using System;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockModel
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] PreviousHash { get; set; }

        public CreateTransactionModel Transaction { get; set; }

        public byte[] BlockHash { get; set; }

        public int Nonce { get; set; }
    }
}
