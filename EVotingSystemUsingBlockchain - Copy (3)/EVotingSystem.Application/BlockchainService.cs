using EVotingSystem.Application.Model;
using EVotingSystem.Blockchain;
using Models;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BlockchainService
    {
        private static readonly List<(TransactionModel, string hash)> transactionModels = new List<(TransactionModel, string)>();
        public BlockchainService()
        {

        }

        public string ReceiveBlock()
        {
           // if(isNewAccount)

            return null;
        }

        public string ProposeBlock((byte[], byte[]) keyPair)
        {
            BlockService blockService = new BlockService();
            return blockService.CreateBlock(transactionModels, keyPair); 
        }

        public void ReceiveTransactionVote(CreateTransactionModel transaction)
        {
            TransactionService transactionService = new TransactionService();
            var transactionVerified = transactionService.ReceiveTransactionAccount(transaction);

            if (transactionVerified.Item1 != null)
            {
                transactionModels.Add(((new TransactionVoteModel
                {
                    Type = transactionVerified.Item1.Type,
                    Signature = transactionVerified.Item1.Signature,
                    FromAddress = transactionVerified.Item1.FromAddress,
                    ToAddress = transactionVerified.Item1.ToAddress,
                    Timestamp = transactionVerified.Item1.Timestamp,
                    Details = transactionVerified.Item1.Details,
                    Vote = transactionVerified.Item1.Vote,
                }), transactionVerified.Item2));
            }
        }

        public void ReceiveTransactionBallot(CreateBallotTransactionModel transaction)
        {
            TransactionService transactionService = new TransactionService();
            var transactionVerified = transactionService.ReceiveTransactionBallot(transaction);

            if (transactionVerified.Item1 != null)
            {
                transactionModels.Add(((new TransactionBallotModel
                {
                    BallotName = transactionVerified.Item1.BallotName,
                    Candidates = transactionVerified.Item1.Candidates,
                    Type = transactionVerified.Item1.Type,
                    EndDate = transactionVerified.Item1.EndDate,
                    Signature = transactionVerified.Item1.Signature,
                    FromAddress = transactionVerified.Item1.FromAddress,
                    ToAddress = transactionVerified.Item1.ToAddress,
                    Timestamp = transactionVerified.Item1.Timestamp,
                }), transactionVerified.Item2));
            }

        }

        public int CheckBalance(string publicKey)
        {
            AccountService accountService = new AccountService();
            return accountService.CheckBalance(publicKey);
        }

        public string CheckTransactionFromAddress(string publicKey)
        {
            AccountService accountService = new AccountService();
            var result = accountService.GetAccountSentTransactions(publicKey);
            if (result == null)
                return null;
            return result.ToString();
        }

        public string CheckTransactionToAddress(string publicKey)
        {
            AccountService accountService = new AccountService();
            var result = accountService.GetAccountReceivedTransactions(publicKey);
            if (result == null)
                return null;
            return result.ToString();
        }

    }

}
