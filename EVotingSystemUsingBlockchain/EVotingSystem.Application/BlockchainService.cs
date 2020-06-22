using EVotingSystem.Application.Model;
using EVotingSystem.Blockchain;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BlockchainService
    {
        private static readonly List<(TransactionModel, string hash)> transactionModels = new List<(TransactionModel, string)>();
        public BlockchainService()
        {

        }

        public string GetCandidates()
        {
            var candidates = DbContext.GetAllCandidates();
            List<string> candidatesName= new List<string>();
            foreach (var item in candidates)
            {
                candidatesName.Add(item.Name);
            }

            return JsonConvert.SerializeObject(candidatesName);
        }

        public string ReceiveBlock(CreateBlockModel model)
        {
            BlockService blockService = new BlockService();
            List<(TransactionModel, string)> verifiedTransactions = new List<(TransactionModel, string)>();
            var transactions = model.Transactions;
            var transService = new TransactionService();
            for (int i = 0; i < transactions.Count; i++)
            {
                if (transactions[i].Type == "Ballot")
                {
                    TransactionBallotModel trans = (TransactionBallotModel)transactions[i];
                    var processed = transService.ReceiveTransactionBallot(new CreateBallotTransactionModel
                    {
                        FromAddress = trans.FromAddress,
                        Signature = trans.Signature,
                        Timestamp = trans.Timestamp,
                        ToAddress = trans.ToAddress,
                        Type = trans.Type,
                        EndDate = trans.EndDate,
                        BallotName = trans.BallotName,
                        Candidates = trans.Candidates,
                    });

                    if (processed.Item1 == null)
                    {
                        return null;
                    }
                    else
                    {
                        verifiedTransactions.Add((transactions[i], processed.Item2));
                    }
                }
                else
                {
                    TransactionVoteModel trans = (TransactionVoteModel)transactions[i];
                    var voteProcessed =  transService.ReceiveTransactionVote(new CreateTransactionModel
                    {
                        Details = trans.Details,
                        FromAddress = trans.FromAddress,
                        Signature = trans.Signature,
                        Timestamp = trans.Timestamp,
                        ToAddress = trans.ToAddress,
                        Type = trans.Type,
                        Vote = trans.Vote
                    });

                    if (voteProcessed.Item1 == null)
                    {
                        return null;
                    }
                    else
                    {
                        verifiedTransactions.Add((transactions[i], voteProcessed.Item2));
                    }
                }
            }

            return blockService.ReceiveBlock(model, verifiedTransactions);
        }

        public string ProposeBlock((byte[], byte[]) keyPair)
        {
            BlockService blockService = new BlockService();
            return blockService.CreateBlock(transactionModels, keyPair);
        }

        public void ReceiveTransactionVote(CreateTransactionModel transaction)
        {
            TransactionService transactionService = new TransactionService();
            var transactionVerified = transactionService.ReceiveTransactionVote(transaction);

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
