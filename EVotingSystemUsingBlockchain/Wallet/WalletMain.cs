﻿using Nodes;
using System;
using System.IO;
using System.Threading;

namespace Wallet
{
    class WalletMain
    {
        static void Main(string[] args)
        {
            string password = null;
            int selector = 0;
            (byte[], byte[]) keyPair = (null, null);

            if(!File.Exists("keys.txt"))
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
            Console.WriteLine("Send Transaction: 1");
            Console.WriteLine("Check Balance: 2");
            Console.WriteLine("View sent transactions: 3");
            Console.WriteLine("View received transactions: 1");

            //randomize
            while (selector != 6)
            {
                Console.WriteLine("Please select an action");
                try
                {
                    selector = Convert.ToInt32(Console.ReadLine());
                    switch (selector)
                    {
                        case 1:
                            Console.WriteLine("Receiver:");
                            string receiver = Console.ReadLine();
                            Console.WriteLine("Port:");
                            int port = Convert.ToInt32(Console.ReadLine());
                            TransactionService vote = new TransactionService();
                            //make it random
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Client.Connect("127.0.0.1", "Transaction" + vote.CreateNewTransaction(receiver, keyPair), 1, port);
                            }).Start();

                            break;
                        case 2:
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Client.Connect("127.0.0.1", "Balance", 1, 13000);
                            }).Start();
                            break;
                        case 3:
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Client.Connect("127.0.0.1", "TPublicKey+key-ul", 1, 13000);
                            }).Start();
                            break;
                        case 4:
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Client.Connect("127.0.0.1", "FPublicKey+key-ul", 1, 13000);
                            }).Start();
                            break;
                        case 5:
                            Console.WriteLine("Account:");
                            string accountPk = Console.ReadLine();
                            Console.WriteLine("Port:");
                            int port2 = Convert.ToInt32(Console.ReadLine());
                            TransactionService accountTransaction = new TransactionService();
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                var trans = accountTransaction.CreateNewTransaction(accountPk, keyPair);
                                Client.Connect("127.0.0.1", "Account" + trans, 5, port2);
                            }).Start();
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
