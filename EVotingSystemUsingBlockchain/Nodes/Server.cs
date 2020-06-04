using EVotingSystem.Application;
using EVotingSystem.Blockchain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Nodes
{
    public class Server
    {
        public List<int> Ports = new List<int>()
        {
           13000,
           13001,
           13002,
           13003,
           13004
        };

        TcpListener server = null;
        int currentPort = 0;
        public static List<string> Transactions = new List<string>();
        public static List<string> TransactionsAccount = new List<string>();

        public Server(string ip, int port)
        {
            currentPort = port;
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            StartListener();
            if (Ports.Contains(currentPort))
            {
                Ports.RemoveAt(Ports.IndexOf(currentPort));
            }
        }

        public void StartListener()
        {
            try
            {
                BlockchainService blockchainService = new BlockchainService();
               if (!blockchainService.VerifyIfGenesisBlockExists())
                {
                   blockchainService.GenesisBlock();

                }
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Stop();
            }
        }

        public void HandleDeivce(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = String.Empty;
            string data = null;
            Byte[] bytes = new Byte[1024];
            int i;
            try
            {
                BlockchainService blockchainService = new BlockchainService();
                //sync

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    if (data.StartsWith("Transaction"))
                    {
                        //Console.WriteLine("{1}: Received: {0}", data);
                        string str = "Transaction Registered";
                        Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                        stream.Write(reply, 0, reply.Length);
                        Console.WriteLine("Sent: {0}", str);
                        //process the transaction and create a block

                        foreach (var item in Ports)
                        {
                            new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true;
                            Client.Connect("127.0.0.1", data, 5, item);
                        }).Start();
                        }

                        Console.WriteLine("Message sent to peers");
                    }
                    else if (data.StartsWith("Account"))
                    {
                        int temp = data.IndexOf("Account") + 7;
                        string transaction = data.Substring(temp);
                        string str = "Account Registered";
                        Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                        stream.Write(reply, 0, reply.Length);
                        Console.WriteLine("Sent: {0}", str);
                        TransactionsAccount.Add(transaction);
                        var result = blockchainService.ReceiveTransactionWithNewAccount(TransactionsAccount);
                        foreach (var item in Ports)
                        {
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Client.Connect("127.0.0.1", result, 7, item);
                            }).Start();
                        }

                        Console.WriteLine("Message sent to peers");
                    }
                    else if (data.StartsWith("Peers"))
                    {
                        Console.WriteLine("ReceivedBlock: {0}", data);
                    }

                    //else if (data.StartsWith("Block"))
                    //{
                    //    int temp = data.IndexOf("Block") + 5;
                    //    string transaction = data.Substring(temp);
                    //    string str = "Block Registered";
                    //    Byte[] reply = Encoding.ASCII.GetBytes(str);
                    //    Console.WriteLine("ReceivedBlock: {0}", data);
                    //    stream.Write(reply, 0, reply.Length);
                    //    Console.WriteLine("Sent: {0}", str);
                    //    BlockchainService blockchainService = new BlockchainService();
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }


        }
    }
}
