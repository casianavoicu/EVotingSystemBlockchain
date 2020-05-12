using Peer2Peer;
using System;
using System.IO;

namespace Wallet
{
    class WalletMain
    {
        private static readonly NodeClient Client = new NodeClient();

        static void Main(string[] args)
        {
            string password = null;
            int selector = 0;
            (byte[], byte[]) keyPair = (null, null);

            if (File.Exists("keys.txt"))
            {
                Console.WriteLine("Enter your password:");
                password = Console.ReadLine();
                keyPair = ReadKeys.ReadKeysFromFile(password);
                Console.WriteLine("Send Transaction: 1");
            }
            else
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

            while (selector != 6)
            {
                Console.WriteLine("Please select an action");
                selector = Convert.ToInt32(Console.ReadLine());
                switch (selector)
                {
                    case 1:
                        Console.WriteLine("Receiver:");
                        string receiver = Console.ReadLine();
                        TransactionService vote = new TransactionService();
                        //make it random
                        string serverUrl = "ws://127.0.0.1:6001/Wallet";
                        Client.Initialize(serverUrl);
                        Client.SendTransaction(vote.CreateNewTransaction(receiver, keyPair));
                        break;
                    case 2:
                        //Check the balance;
                        break;
                    case 3:
                        //View all address
                        Console.WriteLine("Sender:");
                        break;
                    case 4:
                        //view all transactions for you
                        break;
                    case 5:
                        //view all transactions from you
                        break;
                }
            }
        }
    }
}
