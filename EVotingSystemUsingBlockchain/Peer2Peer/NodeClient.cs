using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocketSharp;
namespace Peer2Peer
{
    public class NodeClient
    {
        IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();
        public static List<string> Ports = new List<string>()
        {
            "6001",
            "1002",
        };
        public async Task Initialize(string port)
        {
            await ConnectToNodes(port);
        }

        private async Task ConnectToNodes(string url)
        {
            if (!wsDict.ContainsKey(url))
            {
                var ws = new WebSocket(url);
                ws.OnMessage += (sender, e) =>
                {
                    if (e.Data == "Hi Client")
                    {
                            //Wallet.ReceiveTransaction(e.Data);
                            //foreach (var item in wsDict)
                            //{
                            //    item.Value.Send(data);
                            //}
                            Console.WriteLine("Hi Client");

                    }
                    else if (e.Data == "Transaction registered")
                    {
                        Console.WriteLine("Transaction registered");
                    }
                };
                ws.Connect();
                ws.Send("Hi Server");
                wsDict.Add(url, ws);
            }
        }

        public void SendTransaction(string data)
        {
            var itemToSend = "Transaction" + data;
            foreach (var item in wsDict)
            {
                item.Value.Send(itemToSend);
            }
        }

        public void SendA(string a)
        {
            foreach (var item in wsDict)
            {
                item.Value.Send("Hi");
            }
        }

        public void Send(string a, WebSocket webSocket)
        {
            webSocket.Send(a);
        }
    }
}
