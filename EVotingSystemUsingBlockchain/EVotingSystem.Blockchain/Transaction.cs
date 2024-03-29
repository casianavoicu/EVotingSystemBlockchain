﻿using SQLite;
using System;

namespace EVotingSystem.Blockchain
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }

        [NotNull]
        public string FromAddress { get; set; }

        [NotNull]
        public string ToAddress { get; set; }

        public string Vote { get; set; }

        [NotNull]
        public string Signature { get; set; }

        [NotNull]
        public DateTime Timestamp { get; set; }

        [NotNull]
        public string HashedData { get; set; }

        [NotNull]
        public string Type { get; set; }

        public string Details { get; set; }

        [NotNull]
        public int BlockId { get; set; }
    }
}
