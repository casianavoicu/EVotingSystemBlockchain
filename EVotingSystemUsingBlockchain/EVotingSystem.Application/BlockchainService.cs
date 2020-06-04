using EVotingSystem.Application.Model;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BlockchainService
    {
        public BlockchainService()
        {
        }

        public string ReceiveBlock(bool isNewAccount)
        {
           // if(isNewAccount)

            return null;
        }
        public string SendGenesis()
        {
            return null;
        }

        public string SyncronizePeer()
        {
            //
            return null;
        }

        public bool VerifyIfGenesisBlockExists()
        {
            BlockService blockService = new BlockService();
            if (blockService.GetGenesisBlock()!=null)
                return true;
            return false;
        }

        public string GenesisBlock()
        {
            BlockService blockService = new BlockService();

            return blockService.GenesisBlock();
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

        public string ReceiveTransactionWithNewAccount(List<string> transaction, bool fromWallet)
        {
            List<(TransactionModel, string)> verifiedTransactions = new List<(TransactionModel, string)>();
            foreach (var item in transaction)
            {
                TransactionService transactionService = new TransactionService(item);
                var transactionVerified = transactionService.ReceiveTransactionAccount();

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
