using EVotingSystem.Application;
using System.Linq;

namespace Peer2Peer
{
    public class Wallet
    {
        public static void ReceiveTransaction(string data)
        {
            int i = data.IndexOf("Transaction") + 11;
            string transaction = data.Substring(i);
            TransactionService blockchainService = new TransactionService(transaction);

            blockchainService.ReceiveTransactionFromWallet();
           // System.Threading.Thread.Sleep(10000);
           //NodeClient node = new NodeClient();
           // //var _server = new NodeServer();
           // //_server.Start(6003);
           //node.Initialize("ws://127.0.0.1:6003/Wallet");
        }

        public static string CheckBalance(string publicKey)
        {
            return "Balance";
        }

        public static string ViewTransactions(string publicKey)
        {
            return "ViewTransactions";
        }

        public static string ViewCandidates(string publicKey)
        {
            return "Candidates";
        }
    }
}
