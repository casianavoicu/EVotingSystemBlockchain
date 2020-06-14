using EVotingSystem.Application;
using EVotingSystem.Blockchain;
using Models;
using Newtonsoft.Json;
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

        readonly TcpListener server = null;
        readonly int currentPort = 0;
        public static List<string> Transactions = new List<string>();
        public static List<string> TransactionsAccount = new List<string>();

        private BlockchainService blockchainService = new BlockchainService();

        public Server(string ip, int port)
        {
            currentPort = port;
            Ports.Remove(currentPort);

            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();

            //StartTimer(port);
            StartListener();
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
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDevice));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Stop();
            }
        }

        public void HandleDevice(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = String.Empty;
            string data = null;
            Byte[] bytes = new Byte[1024];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    var transaction = GetTransaction(data);

                    if (transaction is CreateTransactionModel createTransactionModel)
                    {
                        blockchainService.ReceiveTransactionAccount(createTransactionModel);

                        string str = "Transaction Registered";
                        Byte[] reply = Encoding.ASCII.GetBytes(str);
                        stream.Write(reply, 0, reply.Length);
                        Console.WriteLine("Sent: {0}", str);
                        //process the transaction and create a block

                        foreach (var port in Ports)
                        {
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Client.Connect("127.0.0.1", data, 5, port);
                            }).Start();
                        }

                        Console.WriteLine("Message sent to peers");
                    }
                    else if (transaction is BallotTransactionModel ballotTransactionModel)
                    {
                        blockchainService.ReceiveTransactionBallot(ballotTransactionModel);

                        string str = "Transaction Registered";
                        Byte[] reply = Encoding.ASCII.GetBytes(str);
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }

        private void StartTimer(int port)
        {
            var order = port % 5;

            var second = DateTime.Now.Second;

            var whenToStart = DateTime.Now.AddSeconds(60 - second);

            var timeToWait = whenToStart - DateTime.Now;

            Thread.Sleep(timeToWait);

            var timer = new Timer(new TimerCallback(Tick));

            var index = port * 12;

            timer.Change(index, 60);
        }

        private void Tick(object? state)
        {

        }

        private object GetTransaction(string data)
        {
            var ballot = JsonConvert.DeserializeObject<BallotTransactionModel>(data);

            if (ballot.EndDate == null)
            {
                return JsonConvert.DeserializeObject<CreateTransactionModel>(data);
            }
            else
            {
                return ballot;
            }
        }
    }
}
