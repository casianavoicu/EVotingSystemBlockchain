using SQLite;

namespace EVotingSystem.Blockchain
{
    public static class DbContext
    {
        public static SQLiteConnection connection;

        static DbContext()
        {
            connection = new SQLiteConnection("blockchain");
            CreateDB(connection);
        }

        private static void CreateDB(SQLiteConnection connection)
        {
            connection.CreateTable<Block>();
            connection.CreateTable<TransactionCandidateInput>();
            connection.CreateTable<TransactionElectionInput>();
            connection.CreateTable<TransactionVoteInput>();
            connection.CreateTable<Account>();
        }

        public static void InsertCandidate(TransactionCandidateInput candidateInput)
        {
            connection.Insert(candidateInput);
        }
    }
}
