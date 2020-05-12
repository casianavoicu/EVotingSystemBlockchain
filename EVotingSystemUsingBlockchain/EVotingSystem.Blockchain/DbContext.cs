using SQLite;

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
        //public static void InsertAccount(Account account)
        //{
        //    connection.Insert(account);
        //}
        public static void InsertBlock(Block block)
        {
            connection.Insert(block);
        }

        //select transactions,accounts,blocks
        //select balance by public key
        //select all users with type 2 <-candidates

    }
}
