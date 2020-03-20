using System;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockModel
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] Hash { get; set; }

        public byte[] PreviousHash { get; set; }

        public string Data { get; set; }//is going to be transaction

        public int Difficulty { get; set; }
    }
}
