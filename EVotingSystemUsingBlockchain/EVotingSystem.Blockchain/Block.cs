using SQLite;
using System;

namespace EVotingSystem.Blockchain
{
    public class Block
    {
        [PrimaryKey, AutoIncrement]
        public int BlockId { get; set; }

        [NotNull]
        public int BlockIndex { get; set; }

        [NotNull]
        public DateTime TimeStamp { get; set; }

        [NotNull]
        public byte[] PreviousHash { get; set; }

        [NotNull]
        public int TransactionId { get; set; }

    }
}
