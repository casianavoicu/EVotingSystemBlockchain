using System;
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
                        String final = "Transaction" + message;
                        data = Encoding.ASCII.GetBytes(final);
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("Pending: {0}", "Transaction");
                        break;
                    case 2:
                        data = Encoding.ASCII.GetBytes("Balance");
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("Wait: {0}", "Checking your balance");
                        break;
                    case 3:
                        data = Encoding.ASCII.GetBytes("GetTransaction");
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("Wait: {0}", "Checking your transactions");
                        break;
                    case 4:
                        break;
                    case 5:
                        String broadcastBlock = "Peers" + "haha";
                        data = Encoding.ASCII.GetBytes(broadcastBlock);
                        stream.Write(data, 0, data.Length);
                        //Console.WriteLine("Broadcasting: {0}", "Blocks");
                        break;
                    case 6:
                        break;
                }

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
