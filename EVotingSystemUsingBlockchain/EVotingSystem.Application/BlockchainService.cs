using EVotingSystem.Application.Model;
using Models;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BlockchainService
    {
        private static readonly List<CreateTransactionModel> verifiedTransactions = new List<CreateTransactionModel>();
        private static readonly List<BallotTransactionModel> verifiedBallotTransactions = new List<BallotTransactionModel>();

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

        public void ReceiveTransactionAccount(CreateTransactionModel transaction)
        {
            TransactionService transactionService = new TransactionService();
            var transactionVerified = transactionService.ReceiveTransactionAccount(transaction);

            if (transactionVerified != null)
            {
                verifiedTransactions.Add(transactionVerified);
            }
        }

        public void ReceiveTransactionBallot(BallotTransactionModel transaction)
        {
            TransactionService transactionService = new TransactionService();
            var transactionVerified = transactionService.ReceiveTransactionBallot(transaction);

            if (transactionVerified != null)
            {
                verifiedBallotTransactions.Add(transactionVerified);
            }
        }
    }

}
