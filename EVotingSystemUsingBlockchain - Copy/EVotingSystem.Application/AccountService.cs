using EVotingSystem.Application.Model;
using EVotingSystem.Blockchain;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class AccountService
    {
        public AccountModel CreateAccount(string voterPublicKey)
        {
            Account account = new Account
            {
                PublicKey = voterPublicKey,
                Balance = 0
            };
            DbContext.InsertAccount(account);

            return new AccountModel
            {
                PublicKey = account.PublicKey,
                Balance = account.Balance
            };
        }

        public int CheckBalance(string voterPublicKey)
        {
            int balance = DbContext.GetAccountBalance(voterPublicKey);
            return balance;
        }

        public AccountModel VerifyAccount(string voterPublicKey)
        {
            var voter = DbContext.GetAccount(voterPublicKey);
            AccountModel model;
            if (voter == null)
            {
                var account = CreateAccount(voterPublicKey);
                model = new AccountModel
                {
                    Balance = account.Balance,
                    PublicKey = account.PublicKey
                };

                return model;
            }

            return new AccountModel
            {
                Balance = voter.Balance,
                PublicKey = voter.PublicKey
            };
        }

        public IEnumerable<string> GetAccountSentTransactions(string voterPublicKey)
        {
            IEnumerable<Transaction> transactions = DbContext.GetTransactionFromAddress(voterPublicKey);
            if (transactions == null)
                return null;

            List<string> result = new List<string>();
            foreach (var item in transactions)
            {
                result.Add(item.ToAddress);
            }

            return result;
        }

        public IEnumerable<string> GetAccountReceivedTransactions(string voterPublicKey)
        {

            var transactions = DbContext.GetTransactionToAddress(voterPublicKey);
            if (transactions == null)
                return null;

            List<string> result = new List<string>();
            foreach (var item in transactions)
            {
                result.Add(item.FromAddress);
            }

            return result;
        }

        public void UpdateBalanceAfterVote(AccountModel account)
        {
            DbContext.UpdateBalance(new Account
            {
                Balance = account.Balance--,
                PublicKey = account.PublicKey
            });
        }

        public void UpdateBalanceBeforeVote(AccountModel account)
        {
            Account accountModel = new Account
            {
                PublicKey = account.PublicKey
            };

            accountModel.Balance = account.Balance + 1;
            DbContext.UpdateBalance(accountModel);
        }
    }
}
