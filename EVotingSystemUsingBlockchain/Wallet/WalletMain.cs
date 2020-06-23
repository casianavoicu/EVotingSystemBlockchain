using KeyPairServices;
using Newtonsoft.Json;
using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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
                Console.WriteLine("Choose another actions:");
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
            Console.WriteLine("Please select an action");

            while (selector != 7)
            {
                try
                {
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:

                            TransactionService vote = new TransactionService();

                            Client.Connect("127.0.0.1", "Ballot", 8, GetRandomNodes());

                            if (Client.responseFinal == null)
                            {
                                break;
                            }

                            var candidateList = JsonConvert.DeserializeObject<List<string>>(Client.responseFinal);
                            var finalCandidates = new Dictionary<int, string>();
                            Console.WriteLine("Candidates:");
                            int ct = 0;
                            for (int i = 0; i < candidateList.Count(); i++)
                            {
                                finalCandidates.Add(ct, candidateList[i]);
                                Console.WriteLine(candidateList + " " + ct);
                            }

                            var choice = Console.ReadLine();
                            var candidate = finalCandidates.FirstOrDefault(p => p.Key == Convert.ToInt32(choice)).Value;

                            if (candidate == null)
                            {
                                break;
                            }

                            Client.Connect("127.0.0.1", vote.CreateNewTransaction("asda", keyPair, candidate, "Vote"), 1, GetRandomNodes());
                            break;
                        case 2:
                            Client.Connect("127.0.0.1", "Balance" + Convert.ToBase64String(keyPair.Item2), 8, GetRandomNodes());
                            break;
                        case 3:
                            Client.Connect("127.0.0.1", "TPublicKey+key-ul", 1, GetRandomNodes());
                            break;
                        case 4:
                            Client.Connect("127.0.0.1", "FPublicKey+key-ul", 1, GetRandomNodes());
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
                    Console.WriteLine("Please select an action");
            }
        }

        private static int GetRandomNodes()
        {
            List<int> ports = new List<int>
            {
                13000,
                13001,
            };

            ports.Remove(Port);
            Random random = new Random();
            int index = random.Next(ports.Count);

            return ports[index];
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
