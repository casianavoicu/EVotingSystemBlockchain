using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using EVotingSystem.Blockchain;

namespace EVotingSystem.Application
{
    public class TransactionService
    {
        private readonly string transaction = null;

        public TransactionService(string transaction)
        {
            this.transaction = transaction;
        }

        public (TransactionModel, string) ReceiveTransactionAccount()
        {
            TransactionModel model = new TransactionModel();
            TransactionModel deserializedTransaction = model.Deserialize(transaction);
            AccountService accountService = new AccountService();
            AccountModel account = new AccountModel();

            string hash = "";

            if (CryptoUtils.ValidateTransaction(deserializedTransaction.FromAddress, deserializedTransaction, deserializedTransaction.Signature, out hash))
            {
                var acc = accountService.VerifyIfVoterExists(deserializedTransaction.ToAddress);
                if (acc != null)
                {
                    account = acc;
                }
                else
                {
                    account = accountService.CreateAccount(deserializedTransaction.ToAddress, deserializedTransaction.ToAddress);
                }
                accountService.UpdateBalanceBeforeVote(account);

                return (deserializedTransaction, hash);
            }

            return (null, null);
        }

        public (TransactionModel, string) ReceiveTransactionFromWallet()
        {
            TransactionModel model = new TransactionModel();
            TransactionModel deserializedTransaction = model.Deserialize(transaction);
            AccountService accountService = new AccountService();

            if (VerifyVoter(deserializedTransaction.FromAddress))
            {
                string hash = "";

                if (CryptoUtils.ValidateTransaction(deserializedTransaction.FromAddress, deserializedTransaction, deserializedTransaction.Signature, out hash))
                {
                    accountService.UpdateBalanceAfterVote(accountService.
                        VerifyIfVoterExists(deserializedTransaction.FromAddress));
                    return (deserializedTransaction, hash);
                }
            }

            return (null, null);
        }

        public void ReceiveTransactionFromBlock(string data)
        {
            TransactionModel model = new TransactionModel();
            TransactionModel deserializedTransaction = model.Deserialize(transaction);
            string hash = "";

            if (CryptoUtils.ValidateTransaction(deserializedTransaction.FromAddress, deserializedTransaction, deserializedTransaction.Signature, out hash))
            {
                InsertIntoDatabase(deserializedTransaction, hash);
                AccountService account = new AccountService();
                AccountModel accountModel = new AccountModel
                {
                    PublicKey = deserializedTransaction.FromAddress,
                };
            }

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

        private bool VerifyVoter(string publickKey)
        {
            AccountService account = new AccountService();

            if (account.VerifyIfVoterExists(publickKey) == null)
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
