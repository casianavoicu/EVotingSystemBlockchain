using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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
            connection.CreateTable<Candidate>();
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

        public static void InsertCandidate(Candidate candidate)
        {
            connection.Insert(candidate);
        }

        public static int GetAccountBalance(string publicKey)
        {
            if (GetAccount(publicKey) == null)
                return -1;

            return connection.Table<Account>().
                FirstOrDefault(p => p.PublicKey == publicKey).Balance;
        }

        public static Account GetAccount(string publicKey)
        {
            return connection.Table<Account>().
                FirstOrDefault(p => p.PublicKey == publicKey);
        }

        public static IEnumerable<Candidate> GetVotes()
        {
            return connection.Table<Candidate>().ToList();
        }

        public static IEnumerable<Block> GetAllBlocks()
        {
            return connection.Table<Block>().ToList();
        }
        public static IEnumerable<Account> GetAllAccounts()
        {
            return connection.Table<Account>().ToList();
        }

        public static IEnumerable<Transaction> GetAllTransactions()
        {
            return connection.Table<Transaction>().ToList();
        }

        public static IEnumerable<Transaction> GetTransactionFromAddress(string publicKey)
        {
            return connection.Table<Transaction>().
                Where(row => row.FromAddress == publicKey).ToList();
        }

        public static IEnumerable<Transaction> GetTransactionToAddress(string publicKey)
        {
            return connection.Table<Transaction>().
                Where(row => row.ToAddress == publicKey);
        }

        public static (IEnumerable<Candidate>, string acc) GetAllCandidates()
        {
            var transactionBallot = connection.Table<Transaction>()
                .Where(p => p.ToAddress == p.FromAddress
                && p.Details != null).LastOrDefault();
            var test =  DateTime.Parse(transactionBallot.Details).Date;
            if (test < DateTime.Now)
            {
                return (null, null);
            }
            var account = connection.Table<Account>().Where(a => a.PublicKey == transactionBallot.FromAddress).FirstOrDefault();
            return (connection.Table<Candidate>()
                .Where(r => r.AccountId == account.AccountId), account.PublicKey);
        }
        public static void DeleteTransactions(string hash)
        {
            connection.Table<Transaction>(). Where(row => row.HashedData == hash).Delete();
        }

        public static void DeleteBlock(string hash)
        {
            connection.Table<Block>().Where(row => row.Hash == hash).Delete();
        }

        public static void UpdateBalance(Account account)
        {
            Account result = connection.Table<Account>().
                Where(row => row.PublicKey == account.PublicKey).FirstOrDefault();
            result.Balance = account.Balance;
            connection.Update(result);
        }

        public static void UpdateCandidateVote(string name)
        {
            Candidate result = connection.Table<Candidate>().
                Where(row => row.Name == name).FirstOrDefault();
            result.Votes = (++result.Votes);
            connection.Update(result);
        }

        public static void UpdateStateRootHash(int blockIndex, string stateRootHash)
        {
            Block result = connection.Table<Block>()
                .Where(row => row.BlockIndex == blockIndex).FirstOrDefault();
            result.StateRootHash = stateRootHash;
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
            var candidates = connection.Table<Candidate>().ToList();

            var accountsJson = Newtonsoft.Json.JsonConvert.SerializeObject(accounts);
            var blocksJson = Newtonsoft.Json.JsonConvert.SerializeObject(blocks);
            var transactionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(transactions);
            var candidatesJson = Newtonsoft.Json.JsonConvert.SerializeObject(candidates);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(accountsJson);
            stringBuilder.Append(blocksJson);
            stringBuilder.Append(transactionsJson);
            stringBuilder.Append(candidatesJson);

            stringBuilder.Append(nameof(Account));
            stringBuilder.Append(nameof(Block));
            stringBuilder.Append(nameof(Transaction));
            stringBuilder.Append(nameof(Candidate));

            stringBuilder.Append(file_name);

            return stringBuilder.ToString();
        }
    }
}
