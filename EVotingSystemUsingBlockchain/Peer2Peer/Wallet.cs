using EVotingSystem.Application;

namespace Peer2Peer
{
    public class Wallet
    {
        public static void ReceiveTransaction(string trasaction)
        {
            TransactionService blockchainService = new TransactionService(trasaction);

            blockchainService.ReceiveTransactionFromWallet();
        }
    }
}
