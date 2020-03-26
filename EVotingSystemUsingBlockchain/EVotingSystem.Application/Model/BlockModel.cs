using System;

namespace EVotingSystem.Application.Model
{
    public class BlockModel
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] Hash { get; set; }

        public byte[] PreviousHash { get; set; }

        public TransactionModel Transaction { get; set; }

        public int Difficulty { get; set; }
    }
}
