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

        public string PreviousHash { get; set; }

        public string Hash { get; set; }

        public string StateRootHash { get; set; }
    }
}
