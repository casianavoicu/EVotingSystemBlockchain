using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EVotingSystem.Blockchain
{
    public static class DbContext
    {
        private const string file_name = "Blockchain.sqlite";

        public static SQLiteConnection connection;

        static DbContext()
        {
            connection = new SQLiteConnection(file_name);
            CreateDB(connection);
        }

        private static void CreateDB(SQLiteConnection connection)
        {
            connection.CreateTable<Block>();
            connection.CreateTable<Transaction>();
            connection.CreateTable<Account>();
        }

        public static void InsertTransaction(Transaction transaction)
        {
            connection.Insert(transaction);
        }

        public static void InsertAccount(Account account)
        {
            connection.Insert(account);
        }

        public static void InsertBlock(Block block)
        {
            connection.Insert(block);
        }

        public static int GetAccountBalance(string publicKey)
        {
            return connection.Table<Account>().
                FirstOrDefault(p => p.AccountAddress == publicKey).Balance;
        }

        public static Account GetAccount(string publicKey)
        {
            return connection.Table<Account>().
                FirstOrDefault(p => p.AccountAddress == publicKey);
        }

        public static IEnumerable<Transaction> GetTransactionFromAddress(string publicKey)
        {
            return connection.Table<Transaction>().
                Where(row => row.FromAddress == publicKey);
        }

        public static IEnumerable<Transaction> GetTransactionToAddress(string publicKey)
        {
            return connection.Table<Transaction>().
                Where(row => row.ToAddress == publicKey);
        }

        public static void UpdateBalance(Account account)
        {
            Account result = connection.Table<Account>().
                Where(row => row.AccountAddress == account.AccountAddress).FirstOrDefault();
            result.Balance = account.Balance;
            connection.Update(result);
        }

        public static Block PreviousBlock()
        {
            return connection.Table<Block>().OrderByDescending(s => s.BlockIndex)
                .FirstOrDefault();
        }

        public static int BlockId(int index)
        {
            return connection.Table<Block>().
                FirstOrDefault(i => i.BlockIndex == index).BlockId;
        }

        public static Block GenesisBlock()
        {
            return connection.Table<Block>().
                FirstOrDefault(e => e.BlockIndex == 0);
        }

        public static string GetHash()
        {
            var accounts = connection.Table<Account>().ToList();
            var blocks = connection.Table<Block>().ToList();
            var transactions = connection.Table<Transaction>().ToList();

            var accountsJson = Newtonsoft.Json.JsonConvert.SerializeObject(accounts);
            var blocksJson = Newtonsoft.Json.JsonConvert.SerializeObject(blocks);
            var transactionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(transactions);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(accountsJson);
            stringBuilder.Append(blocksJson);
            stringBuilder.Append(transactionsJson);

            stringBuilder.Append(nameof(Account));
            stringBuilder.Append(nameof(Block));
            stringBuilder.Append(nameof(Transaction));

            stringBuilder.Append(file_name);

            return stringBuilder.ToString();
        }
    }
}
