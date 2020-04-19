using System;

namespace EVotingSystem.Application.Model
{
    public class BlockModel
    {
        public CreateBlockHeaderModel BlockHeader { get; set; }

        public CreateTransactionVoteModel Transaction { get; set; }

        public byte[] BlockHash { get; set; }

        public int Difficulty { get; set; }
    }
}
