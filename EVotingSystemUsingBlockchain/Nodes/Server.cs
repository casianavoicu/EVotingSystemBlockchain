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
            Byte[] bytes = new Byte[256];
            int i;
            try
            {
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
                    else if (data.StartsWith("Peers"))
                    {
                        Console.WriteLine("ReceivedBlock: {0}", data);
                    }
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
