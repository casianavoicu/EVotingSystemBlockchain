using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebSocketSharp;

namespace Peer2Peer
{
    class Example
    {
        public static List<string> peers = new List<string>();

        public static List<WebSocket> webSockets = new List<WebSocket>();
        static void Main(string[] args)
        {
            Console.WriteLine("Insert your PORT number:");

            var Port = Convert.ToInt32(Console.ReadLine());

            var httpClient = new HttpClient();

            var stringContent = new StringContent(JsonConvert.SerializeObject(Port.ToString()), Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync("http://localhost:25833/Node", stringContent).Result;

            Console.WriteLine("=========================");
            Console.WriteLine("1. Connect to a server");

            int selection = 0;
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("There you go");
                        var content = response.Content.ReadAsStringAsync().Result;

                        peers = JsonConvert.DeserializeObject<List<string>>(content);
                        foreach (var item in peers)
                        {
                            var a = item.Split("/");
                            var b = a[1];
                            WebSocket ws = new WebSocket($"ws://127.0.0.1:{b}");
                            webSockets.Add(ws);
                        }
                        foreach (var item in webSockets)
                        {
                            if (item.Url.ToString().Contains($"ws://127.0.0.1:{Port.ToString()}"))
                            {
                                Console.WriteLine("Hi");
                            }
                            else
                            {
                                try
                                {
                                    item.Connect();
                                    item.Send("Hi Server");
                                    Console.WriteLine("Hi server");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                        break;
                }
                Console.WriteLine("Please select an action");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }
        }
    }
}
