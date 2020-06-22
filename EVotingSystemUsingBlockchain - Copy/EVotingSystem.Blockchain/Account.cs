using SQLite;
using System;

namespace EVotingSystem.Blockchain
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int AccountId { get; set; }

        [NotNull]
        public string PublicKey { get; set; }

        public int Balance { get; set; }
    }
}
