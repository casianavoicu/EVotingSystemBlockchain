using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Peer2Peer
{
    public class NodeServer : WebSocketBehavior
    {
        private static WebSocketServer _wss;

        public void Start(int port)
        {
            _wss = new WebSocketServer($"ws://127.0.0.1:{port}");
            _wss.AddWebSocketService<NodeServer>("/Wallet");

            _wss.KeepClean = false;
            _wss.Start();
            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            EmitOnPing = true;
            Console.WriteLine(e.Data);
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("Hi Client");
            }
            else if (e.Data.StartsWith("Transaction"))
            {
                Console.WriteLine(e.Data);
                Wallet.ReceiveTransaction(e.Data);
            }
        }

    }
}