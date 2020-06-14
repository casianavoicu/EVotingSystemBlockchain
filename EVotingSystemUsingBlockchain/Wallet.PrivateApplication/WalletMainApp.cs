using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Wallet.Services;

namespace Wallet.PrivateApplication
{
    class WalletMainApp
    {
        static void Main(string[] args)
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
                Console.WriteLine("You have been registered in the system");
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
            Console.WriteLine("Transaction: 1");
            Console.WriteLine("Create Ballot: 2");
            Console.WriteLine("View final votes: 3");

            TransactionInstitutionService transaction = new TransactionInstitutionService();
            TransactionService accountTransaction = new TransactionService();
            //randomize
            while (selector != 4)
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
                            Console.WriteLine("Port:");
                            int port2 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Amount:");
                            string ammount = Console.ReadLine();
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                var finalTransaction = accountTransaction.CreateNewTransaction(accountPk, keyPair, ammount);
                                Client.Connect("127.0.0.1", finalTransaction, 5, port2);
                            }).Start();
                            break;

                        case 2:
                            Console.WriteLine("Insert the final date yyyy/MM/dd HH:mm:ss");
                            var test = new DateTime(2020, 5, 1, 14, 0, 0);
                            var year = int.Parse(Console.ReadLine());
                            var month = int.Parse(Console.ReadLine());
                            var day = int.Parse(Console.ReadLine());
                            var hour = int.Parse(Console.ReadLine());
                            var minute = int.Parse(Console.ReadLine());
                            var second = int.Parse(Console.ReadLine());

                            Console.WriteLine("Insert all candidates and press x key at the end of the list:");
                            string candidateName;
                            var key = "x";
                            List<string> candidates = new List<string>();

                            while (((candidateName = Console.ReadLine()) != key.ToLower()))
                            {
                                candidates.Add(candidateName);
                            }

                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                var finalTransaction = transaction.CreateBallotTransaction(keyPair, candidates, new DateTime(year, month, day, hour, minute, second));
                                Client.Connect("127.0.0.1", finalTransaction, 5, 13000);
                            }).Start();
                            break;

                        case 3:
                            Console.WriteLine("View final:");
                            //request catre
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

