using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Peer2Peer
{
    public class NodeServer : WebSocketBehavior
    {
        private WebSocketServer _wss;
        public void Start()
        {
            _wss = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            _wss.AddWebSocketService<NodeServer>("/Wallet");
            _wss.Start();
            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("Hi Client");
            }

        }
    }
}
