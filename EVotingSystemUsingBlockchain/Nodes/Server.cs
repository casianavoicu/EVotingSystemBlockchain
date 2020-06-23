using EVotingSystem.Application;
using EVotingSystem.Application.Model;
using KeyPairServices;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nodes
{
    public class Server
    {
        private readonly (byte[], byte[]) keyPair = (null, null);
        private static readonly List<int> Ports = new List<int>
        {
           13000,
           13001
        };

        readonly TcpListener server = null;
        readonly int currentPort = 0;
        public static List<string> Transactions = new List<string>();
        public static List<string> TransactionsAccount = new List<string>();

        private readonly BlockchainService blockchainService = new BlockchainService();

        public Server(string ip, int port)
        {
            string password;

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
                //adaugarea cheilor in db
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

            StartTimer(port, Ports.Count);

            currentPort = port;
            Ports.Remove(currentPort);

            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            StartListener();
        }

        public void StartListener()
        {
            try
            {
                BlockService block = new BlockService();
                block.GetGenesisBlock();

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
            Byte[] bytes = new Byte[60000];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    var requestObject = GetRequestType(data);

                    if (data.StartsWith("Balance"))
                    {
                        var result = blockchainService.CheckBalance(data.Substring(7));
                        Byte[] reply = new Byte[64];
                        if (result == -1)
                        {
                            reply = Encoding.ASCII.GetBytes("You are not registered into our system yet");
                        }
                        else
                        {
                            reply = Encoding.ASCII.GetBytes(Convert.ToString(result));
                        }

                        stream.Write(reply, 0, reply.Length);
                    }
                    else if (data.StartsWith("Ballot"))
                    {
                        var candidates = blockchainService.GetCandidates();
                        var reply = Encoding.ASCII.GetBytes(candidates);
                        stream.Write(reply, 0, reply.Length);
                    }
                    else if (data.StartsWith("HistoryF"))
                    {
                        blockchainService.CheckBalance(data.Substring(8));
                    }
                    else if (data.StartsWith("HistoryT"))
                    {
                        blockchainService.CheckBalance(data.Substring(8));
                    }
                    else if (data.StartsWith("123"))
                    {

                    }
                    else if (requestObject is CreateTransactionModel createTransactionModel)
                    {
                        blockchainService.ReceiveTransactionVote(createTransactionModel);
                        var reply = Encoding.ASCII.GetBytes("OK");
                        stream.Write(reply, 0, reply.Length);
                    }
                    else if (requestObject is CreateBallotTransactionModel ballotTransactionModel)
                    {
                        blockchainService.ReceiveTransactionBallot(ballotTransactionModel);

                        Console.WriteLine("Message sent to peers");
                    }
                    else if (requestObject is CreateBlockModel blockModel)
                    {
                        if (blockchainService.ReceiveBlock(blockModel) == null)
                        {
                            Console.WriteLine("Invalid Block");
                        }

                        else
                        {
                            Console.WriteLine("Valid Block");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }

        private void SendToPeers()
        {
            if (BlockchainService.transactionModels.Count == 0)
            {
                return;
            }

            Console.WriteLine("Sending to nodes");

            var result = blockchainService.ProposeBlock(keyPair);

            foreach (var port in Ports)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Client.Connect("127.0.0.1", result, 6, port);
                }).Start();
            }

            BlockchainService.transactionModels.Clear();
        }

        private void StartTimer(int port, int numberOfNodes)
        {
            var order = (port % numberOfNodes) + 1;

            var secondsPerNode = 60 / numberOfNodes;

            var secondWhenToSend = secondsPerNode * order;

            var now = DateTime.UtcNow;
            var nowSecond = DateTime.UtcNow.Second;

            var timeToSleep = (int)(now - now.AddSeconds(nowSecond)).TotalSeconds + secondWhenToSend;

            timeToSleep = (timeToSleep + 60) % 60;

            Console.WriteLine(timeToSleep);

            Task.Run(() =>
            {
                Thread.Sleep(timeToSleep * 1000);

                Console.WriteLine(DateTime.Now.Second);

                var timer = new Timer(new TimerCallback(Tick));

                timer.Change(0, 60000);
            });
        }

        private void Tick(object? state)
        {
            SendToPeers();
        }

        private object GetRequestType(string data)
        {
            try
            {
                var ballot = JsonConvert.DeserializeObject<CreateBallotTransactionModel>(data);
                var block = JsonConvert.DeserializeObject<CreateBlockModel>(data);

                if (block.StateRootHash != null)
                {
                    CreateBlockModel model = new CreateBlockModel();
                    return model.Deserialize(data);
                }
                else if (ballot.Type != "Ballot")
                {
                    return JsonConvert.DeserializeObject<CreateTransactionModel>(data);
                }

                else if (ballot.Type == "Ballot")
                {
                    return ballot;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
