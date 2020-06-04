﻿using System;
using System.Net.Sockets;
using System.Text;

namespace Nodes
{
    public static class Client
    {
        public static void Connect(String server, String message, int messageType, Int32 port)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                Byte[] data = new Byte[256];
                switch (messageType)
                {
                    case 1:
                        Console.WriteLine("Pending: {0}", "Transaction");
                        break;
                    case 2:
                        Console.WriteLine("Wait: {0}", "Checking your balance");
                        break;
                    case 3:
                        Console.WriteLine("Wait: {0}", "Checking your transactions");
                        break;
                    case 4:
                        Console.WriteLine("Wait: {0}", "Checking your transactions");
                        break;
                    case 5:
                        Console.WriteLine("Wait: {0}", "Register Account");
                        break;
                    case 6:
                        Console.WriteLine("Broadcasting: {0}", "Blocks");
                        break;
                    case 7:
                        Console.WriteLine("Broadcasting: {0}", "AccountBlocks");
                        break;
                }
                    data = Encoding.ASCII.GetBytes(message);
                    //data = Convert.FromBase64String(message);
                    stream.Write(data, 0, data.Length);
                    data = new Byte[256];
                    String response = String.Empty;
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Your response: {0}", response);
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
    }
}
