using KeyPairServices;
using Newtonsoft.Json;
using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wallet.Services;

namespace Wallet
{
    class WalletMain
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

            string password;
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
            Console.WriteLine("Vote: 1");
            Console.WriteLine("Check Balance: 2");
            Console.WriteLine("View sent transactions: 3");
            Console.WriteLine("View received transactions: 4");
            Console.WriteLine("Get public key: 5");

            while (selector != 7)
            {
                try
                {
                    Console.WriteLine("Please select an action");
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:

                            TransactionService vote = new TransactionService();
                            var ballotResponse = Client.Connect("127.0.0.1", "Ballot", 8, Port);

                            if (ballotResponse == null)
                            {
                                break;
                            }
                            var candidateList = JsonConvert.DeserializeObject<List<string>>(ballotResponse);
                            var address = candidateList.ElementAt(0);
                            var ballotName = candidateList.ElementAt(1);
                            candidateList.RemoveAt(0);
                            candidateList.RemoveAt(0);
                            var finalCandidates = new Dictionary<int, string>();
                            Console.WriteLine("Candidates:");
                            int ct = 1;
                            for (int i = 0; i < candidateList.Count(); i++)
                            {
                                finalCandidates.Add(ct, candidateList[i]);
                                Console.WriteLine(candidateList[i] + " " + ct);
                                ct++;
                            }

                            var choice = Console.ReadLine();
                            var candidate = finalCandidates.FirstOrDefault(p => p.Key == Convert.ToInt32(choice)).Value;

                            if (candidate == null)
                            {
                                break;
                            }

                            var transactionResponse = Client.Connect("127.0.0.1", vote.CreateNewTransaction(address, keyPair, candidate, "Vote"), 1, Port);
                            Console.WriteLine(transactionResponse);
                            break;
                        case 2:
                            var balance = Client.Connect("127.0.0.1", "Balance" + Convert.ToBase64String(keyPair.Item2), 8, PortRandom);
                            Console.WriteLine("Your balance :" + balance);
                            break;
                        case 3:
                            var response = Client.Connect("127.0.0.1", "HistoryF"+Convert.ToBase64String(keyPair.Item2), 1, PortRandom);
                            Console.WriteLine("Your history :" + response);
                            break;
                        case 4:
                            var history = Client.Connect("127.0.0.1", "HistoryT" + Convert.ToBase64String(keyPair.Item2), 1, PortRandom);
                            Console.WriteLine("Your history :" + history);
                            break;
                        case 5:
                            Console.WriteLine(Convert.ToBase64String(keyPair.Item2));
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

    internal class CandidatesFinal
    {
        public List<string> Candidates{ get; set; }

        public CandidatesFinal Deserialize(string content)
        {
            var result = JsonConvert.DeserializeObject<CandidatesFinal>(content);

            return result;
        }
    }
}
