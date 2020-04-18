using System;

namespace EVotingSystem.Application.Model
{
    public class BlockModel
    {
        public int BlockNumber { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] BlockHash { get; set; }

        public byte[] PreviousBlockHash { get; set; }

        public TransactionVoteModel Transaction { get; set; }

        public int Difficulty { get; set; }
    }
}
