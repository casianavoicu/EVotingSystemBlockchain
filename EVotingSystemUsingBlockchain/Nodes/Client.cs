using System;
using System.Net.Sockets;
using System.Text;

namespace Nodes
{
    public static class Client
    {
        public static string responseFinal;
        public static void Connect(String server, String message, int messageType, Int32 port)
        {
            try
            {
                using (var tcpClient = new TcpClient(server, port))
                {
                    using (var stream = tcpClient.GetStream())
                    {
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
                            case 8:
                                Console.WriteLine("");
                                break;
                        }

                        var data = Encoding.ASCII.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        String response = String.Empty;
                        data = new Byte[128];
                        var bytes = stream.Read(data, 0, data.Length);
                        response = Encoding.ASCII.GetString(data, 0, bytes);
                        responseFinal = response;
                        //Console.WriteLine("Your response: {0}", response);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();

        }
    }
}
