﻿using SQLite;

namespace EVotingSystem.Blockchain
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int AccountId { get; set; }

        [NotNull]
        public string AccountAddress { get; set; }

        [NotNull]
        public string PublicKey { get; set; }
    }
}