using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using EVotingSystem.Blockchain;
using Models;
using System;

namespace EVotingSystem.Application
{
    public class TransactionService
    {
        public (CreateTransactionModel, string) ReceiveTransactionVote(CreateTransactionModel transaction)
        {
            AccountService accountService = new AccountService();

            var transactionWithoutSignature = new CreateTransactionModelWithoutSignature(transaction);
            var serializedTransaction = transactionWithoutSignature.Serialize();
            if (CryptoUtils.ValidateSignature(transaction.FromAddress, serializedTransaction, transaction.Signature, out string hash))
            {
                var acc = accountService.VerifyAccount(transaction.ToAddress);

                if (transaction.Type == "Vote")
                {

                    accountService.UpdateBalanceBeforeVote(acc);
                }
                else
                {
                    accountService.UpdateBalanceAfterVote(acc);
                }

                return (transaction, hash);
            }

            return (null, null);
        }

        public (CreateBallotTransactionModel, string) ReceiveTransactionBallot(CreateBallotTransactionModel transactionBallot)
        {
            AccountService accountService = new AccountService();
            var transactionWithoutSignature = new CreateBallotTransactionModelWithoutSignature(transactionBallot);
            var serializedTransaction = transactionWithoutSignature.Serialize();
            if (CryptoUtils.ValidateSignature(transactionBallot.FromAddress, serializedTransaction, transactionBallot.Signature, out string hash))
            {
                accountService.VerifyAccount(transactionBallot.ToAddress);
                return (transactionBallot, hash);
            }

            return (null, null);
        }

        public void InsertBallotTransactions((TransactionBallotModel, string) finalTransaction, int BlockId)
        {
            DbContext.InsertTransaction(new Transaction
            {
                BlockId = BlockId,
                FromAddress = finalTransaction.Item1.FromAddress,
                HashedData = finalTransaction.Item2,
                Signature = finalTransaction.Item1.Signature,
                Timestamp = finalTransaction.Item1.Timestamp,
                ToAddress = finalTransaction.Item1.ToAddress,
                Type = finalTransaction.Item1.Type,
                Details = Convert.ToString(finalTransaction.Item1.EndDate),
                Vote = null
            });

            var candidates = finalTransaction.Item1.Candidates;
            var ballotName = finalTransaction.Item1.BallotName;
            var insertedAccount = DbContext.GetAccount(finalTransaction.Item1.FromAddress);
            foreach (var candidateName in candidates)
            {
                var candidate = new Candidate()
                {
                   Ballot = ballotName,
                   Votes = 0,
                   Name = candidateName,
                   AccountId = insertedAccount.AccountId
                };

                DbContext.InsertCandidate(candidate);
            }
        }

        public void InsertVoteTransactions((TransactionVoteModel, string) finalTransaction, int BlockId)
        {
            DbContext.InsertTransaction(new Transaction
            {
                BlockId = BlockId,
                FromAddress = finalTransaction.Item1.FromAddress,
                HashedData = finalTransaction.Item2,
                Signature = finalTransaction.Item1.Signature,
                Timestamp = finalTransaction.Item1.Timestamp,
                ToAddress = finalTransaction.Item1.ToAddress,
                Type = finalTransaction.Item1.Type,
                Details = finalTransaction.Item1.Details,
                Vote = finalTransaction.Item1.Vote
            });

            var candidates = DbContext.GetAllCandidates();
            foreach (var item in candidates)
            {
                if (item.Name == finalTransaction.Item1.Vote)
                {
                    DbContext.UpdateCandidateVote(item.Name);
                    return;
                }
            }
        }
    }
}
