using SQLite;
using System.Collections.Generic;

namespace EVotingSystem.Blockchain
{
    public static class DbContext
    {
        public static SQLiteConnection connection;

        static DbContext()
        {
            connection = new SQLiteConnection("Blockchain.sqlite");
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
    }
}
