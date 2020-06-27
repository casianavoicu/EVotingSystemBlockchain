using KeyPairServices;
using Newtonsoft.Json;
using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Wallet.Services;

namespace Wallet.PrivateApplication
{
    class WalletMainApp
    {
        private const int numberOfPorts = 2;

        public static int Port
        {
            get
            {
                var timePerPort = 60 / numberOfPorts;

                return 13000 + (DateTime.Now.Second / timePerPort);
            }
        }
        public static int PortRandom
        {
            get
            {
                var ports = Server.Ports;
                ports.Remove(Port);
                Random random = new Random();
                int index = random.Next(ports.Count());
                return Server.Ports.ElementAt(index);
            }
        }

        static void Main()
        {
            string password = null;
            int selector = 0;
            (byte[], byte[]) keyPair = (null, null);


            if (!File.Exists("keys.txt"))
            {
                Console.WriteLine("You have to enter a password in order to register:");
                Console.WriteLine("Enter a password:");
                password = Console.ReadLine();
                Console.WriteLine("Confirm password:");
                string confirm = Console.ReadLine();
                while (password != confirm)
                {
                    Console.WriteLine("Enter a password:");
                    password = Console.ReadLine();
                    Console.WriteLine("Confirm password:");
                    confirm = Console.ReadLine();
                }
                GenerateKeysService.CreateKeyPair(password);
                Console.WriteLine("Your Wallet is ready");
                Console.WriteLine("Choose another action:");
            }

            Console.WriteLine("Enter your password:");
            while (keyPair.Item1 == null)
            {
                password = Console.ReadLine();
                keyPair = ReadKeys.ReadKeysFromFile(password);
                if (keyPair.Item1 == null)
                {
                    Console.WriteLine("Enter a valid password");
                }
            }
            Console.WriteLine("Transaction: 1");
            Console.WriteLine("Create Ballot: 2");
            Console.WriteLine("View final votes: 3");
            Console.WriteLine("View sent transactions: 4");
            Console.WriteLine("View received transactions: 5"); ;

            TransactionInstitutionService transaction = new TransactionInstitutionService();
            TransactionService accountTransaction = new TransactionService();
            while (selector != 6)
            {
                Console.WriteLine("Please select an action");
                try
                {
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:
                            Console.WriteLine("Account:");
                            string accountPk = Console.ReadLine();
                            Console.WriteLine("Amount:");
                            string amount = Console.ReadLine();
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                var finalTransaction = accountTransaction.CreateNewTransaction(accountPk, keyPair, amount, "Account");
                                Client.Connect("127.0.0.1", finalTransaction, 1, Port);
                            }).Start();
                            break;

                        case 2:
                            Console.WriteLine("Enter a file name in order to add candidates");
                            var fileName = Console.ReadLine();

                            var lines =  File.ReadLines(fileName).ToList();

                            var endDate = DateTime.ParseExact(lines[0], "yyyy/MM/dd HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            var ballotName = lines[1];
                            var candidates = lines.Skip(2).ToList();

                            Console.WriteLine("Date and Time");
                            Console.WriteLine(lines[0]);

                            Console.WriteLine("Ballot Name:");
                            Console.WriteLine(lines[1]);

                            Console.WriteLine("Candidates:");
                            foreach (var item in candidates)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine("Your transaction has been registerd");
                            Console.WriteLine();
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                var finalTransaction = transaction.CreateBallotTransaction(keyPair, candidates, ballotName, endDate);
                                Client.Connect("127.0.0.1", finalTransaction, 1, Port);
                            }).Start();
                            break;

                        case 3:
                            Console.WriteLine("View final:");
                            var finalCandidatesSerialized = Client.Connect("127.0.0.1", "FinalVotes" + Convert.ToBase64String(keyPair.Item2), 5, PortRandom);
                            var deserialized = JsonConvert.DeserializeObject<List<string>>(finalCandidatesSerialized);
                            foreach (var item in deserialized)
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        case 4:
                            Console.WriteLine("View history");
                            var response = Client.Connect("127.0.0.1", "HistoryF" + Convert.ToBase64String(keyPair.Item2), 3, PortRandom);
                            Console.WriteLine("Your history :" + response);
                            break;
                        case 5:
                            var responseHist = Client.Connect("127.0.0.1", "HistoryT" + Convert.ToBase64String(keyPair.Item2), 4, PortRandom);
                            Console.WriteLine("Your history :" + responseHist);
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please insert a valid number");
                }
            }
        }
    }
}

