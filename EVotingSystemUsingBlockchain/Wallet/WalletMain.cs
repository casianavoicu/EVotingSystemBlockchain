using EVotingSystem.Application;
using System;
using System.IO;

namespace Wallet
{
    class WalletMain
    {
        static void Main(string[] args)
        {
            string password = null;
            int selector = 0;
            //byte[] privateKey = null;
            //byte[] publicKey = null;
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
                Console.WriteLine("You have enter a password in order to register:");
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
                GenerateKeys.CreateKeyPair(password);
                Console.WriteLine("You have been registered into our system");
                Console.WriteLine("Choose another actions:");
            }

            while (selector != 5)
            {
                switch (selector)
                {
                    case 1:
                        Console.WriteLine("Receiver:");
                        var receiver = Console.ReadLine();
                        Transaction vote = new Transaction();
                        var a = vote.CreateNewTransaction(receiver, keyPair);
                        TransactionService blockchainService = new TransactionService(a);
                        blockchainService.ReceiveVoteTransactionFromWallet();
                        break;
                    case 2:
                        //Check the balance;
                        break;
                    case 3:
                        //send transactions to peers(request);
                        //Create Transaction
                        //View all address
                        Console.WriteLine("Sender:");
                        break;
                    case 4:
                        //request transactions
                        break;
                }

                selector = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
