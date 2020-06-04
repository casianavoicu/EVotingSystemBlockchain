using EVotingSystem.Application.Model;
using EVotingSystem.Blockchain;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class AccountService
    {
        public AccountModel CreateAccount(string voterPublicKey, string address)
        {
            Account account = new Account
            {
                AccountAddress = address,
                PublicKey = voterPublicKey,
                Balance = 0
            };
            DbContext.InsertAccount(account);

            return new AccountModel
            {
                AccountAddress = account.AccountAddress,
                PublicKey = account.PublicKey,
                Balance = account.Balance
            };
        }

        public int CheckBalance(string voterPublicKey)
        {
            int balance = DbContext.GetAccountBalance(voterPublicKey);
            return balance;
        }

        public AccountModel VerifyIfVoterExists(string voterPublicKey)
        {
            var voter = DbContext.GetAccount(voterPublicKey);

            if (voter == null)
                return null;

            AccountModel account = new AccountModel
            {
                AccountAddress = voter.AccountAddress,
                Balance = voter.Balance,
                PublicKey = voter.PublicKey
            };

            return account;
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
                AccountAddress = account.AccountAddress,
                Balance = account.Balance--,
                PublicKey = account.PublicKey
            });
        }

        public void UpdateBalanceBeforeVote(AccountModel account)
        {
            Account accountModel = new Account
            {
                AccountAddress = account.AccountAddress,
                PublicKey = account.PublicKey
            };
            accountModel.Balance = account.Balance + 1;
            DbContext.UpdateBalance(accountModel);
        }
    }
}
