using System.Threading;

namespace Nodes
{
    class Program
    {
        public static int Port;
        static void Main(string[] args)
        {
            Port = 13001;
            if (args.Length >= 1)
            {
                Port = int.Parse(args[0]);
            }

            Thread t = new Thread(delegate ()
            {
                Server myserver = new Server("127.0.0.1", Port);
            });
            t.Start();
        }
    }
}
