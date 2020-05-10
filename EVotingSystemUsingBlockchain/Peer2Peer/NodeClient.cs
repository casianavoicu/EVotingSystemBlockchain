using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebSocketSharp;
namespace Peer2Peer
{
    public class NodeClient
    {
        IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();
        public static List<string> Ports = new List<string>()
        {
            "1001",
            "1002",
        };
        public void Initialize(string port)
        {
            ConnectToNodes(port);
        }

        private void ConnectToNodes(string url)
        {
            if (!wsDict.ContainsKey(url))
            {
                WebSocket ws = new WebSocket(url);
                ws.OnMessage += (sender, e) =>
                {
                    if (e.Data == "Hi Client")
                    {
                        Console.WriteLine(e.Data);
                    }
                    else
                    {

                    }
                };
                ws.Connect();
                ws.Send("Hi Server");
                wsDict.Add(url, ws);
            }
        }
    }
}
