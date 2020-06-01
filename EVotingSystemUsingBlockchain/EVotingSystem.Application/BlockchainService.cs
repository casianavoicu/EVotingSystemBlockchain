using EVotingSystem.Application.Model;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BlockchainService
    {
        private readonly string Transaction = null;

        public BlockchainService(string Transaction)
        {
            this.Transaction = Transaction;
        }

        public string ReceiveBlock()
        {
            return null;
        }

        public string ReceiveTransaction(List<string> transaction)
        {
            List <(TransactionModel,string)> verifiedTransactions= new List<(TransactionModel,string)>();
            foreach (var item in transaction)
            {
                TransactionService transactionService = new TransactionService(item);
                var transactionVerified = transactionService.ReceiveTransactionFromWallet();

                if (transactionVerified != (null, null))
                {
                    verifiedTransactions.Add((transactionVerified.Item1, transactionVerified.Item2));
                }
            }

            var blockService = new BlockService();

            return blockService.CreateBlock(verifiedTransactions);
        }
    }

}
