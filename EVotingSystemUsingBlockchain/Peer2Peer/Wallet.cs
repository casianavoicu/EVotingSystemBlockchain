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
