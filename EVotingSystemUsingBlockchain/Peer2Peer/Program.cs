using System;

namespace Peer2Peer
{
    public class Program
    {
        public static int Port;
        private static NodeServer _server;
        private static readonly NodeClient Client = new NodeClient();
        private static string _name = "Unknown";
        public static void Main(string[] args)
        {
            Port = Convert.ToInt32("6001");
            if (args.Length >= 1)
                Port = int.Parse(args[0]);
            if (args.Length >= 2)
                _name = args[1];

            if (Port > 0)
            {
                _server = new NodeServer();
                _server.Start();
            }
            if (_name != "Unkown")
            {
                Console.WriteLine($"Current user is {_name}");
            }

            Console.WriteLine("1. Connect to a server");
            Console.WriteLine("2. Add a transaction");
            Console.WriteLine("3. Display Blockchain");
            Console.WriteLine("4. Exit");

            int selection = 0;
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Please enter the server URL");
                        string serverUrl = Console.ReadLine();
                        Client.Initialize($"{serverUrl}/Wallet");
                        break;
                    case 2:
                        Console.WriteLine("Please enter the receiver name");
                        string receiverName = Console.ReadLine();
                        Console.WriteLine("Please enter the amount");
                        string amount = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Blockchain");
                        break;

                }

                Console.WriteLine("Please select an action");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }
        }
    }

}
