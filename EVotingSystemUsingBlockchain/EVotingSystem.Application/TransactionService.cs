using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using EVotingSystem.Blockchain;
using Models;

namespace EVotingSystem.Application
{
    public class TransactionService
    {
        public CreateTransactionModel ReceiveTransactionAccount(CreateTransactionModel transaction)
        {
            AccountService accountService = new AccountService();
            AccountModel account;

            var transactionWithoutSignature = new CreateTransactionModelWithoutSignature(transaction);
            var serializedTransaction = transactionWithoutSignature.Serialize();

            if (CryptoUtils.ValidateTransaction(transaction.FromAddress, serializedTransaction, transaction.Signature))
            {
                var acc = accountService.VerifyAccount(transaction.ToAddress);
                if (transaction.Type == "Account")
                {
                    if (acc != null)
                    {
                        account = acc;
                    }
                    else
                    {
                        account = accountService.CreateAccount(transaction.ToAddress, transaction.ToAddress);
                    }

                    accountService.UpdateBalanceBeforeVote(account);
                }
                else
                {
                    accountService.UpdateBalanceAfterVote(acc);
                }

                return transaction;
            }

            return null;
        }

        public BallotTransactionModel ReceiveTransactionBallot(BallotTransactionModel transactionBallot)
        {
            var ballotService = new BallotService();

            var transactionWithoutSignature = new BallotTransactionModelWithoutSignature(transactionBallot);
            var serializedTransaction = transactionWithoutSignature.Serialize();

            if (CryptoUtils.ValidateTransaction(transactionBallot.FromAddress, serializedTransaction, transactionBallot.Signature))
            {
                var candidates = transactionBallot.Candidates;
                var ballotName = transactionBallot.BallotName;

                ballotService.AddCandidates(candidates, ballotName, transactionBallot.ToAddress);
            }

            return null;
        }

        public void ReceiveTransactionFromBlock(string data)
        {
            /*TransactionModel model = new TransactionModel();
            TransactionModel deserializedTransaction = model.Deserialize(transaction);

            if (CryptoUtils.ValidateTransaction(deserializedTransaction.FromAddress, deserializedTransaction, deserializedTransaction.Signature, out string hash))
            {
                InsertIntoDatabase(deserializedTransaction, hash);
                // AccountService account = new AccountService();
                var accountModel = new AccountModel
                {
                    PublicKey = deserializedTransaction.FromAddress,
                };
            }
            */
        }

        private void InsertIntoDatabase(TransactionModel transaction, string hash)
        {
            DbContext.InsertTransaction(new Transaction
            {
                FromAddress = transaction.FromAddress,
                ToAddress = transaction.ToAddress,
                HashedData = hash,
                Signature = transaction.Signature,
                Timestamp = transaction.Timestamp,
                Type = transaction.Type,
                Vote = transaction.Vote,
            });
        }

        private bool VerifyAccount(string publickKey)
        {
            AccountService account = new AccountService();

            if (account.VerifyAccount(publickKey) == null)
            {
                return false;
            }

            if (account.CheckBalance(publickKey) == 0)
            {
                return false;
            }

            return true;
        }
    }
}
