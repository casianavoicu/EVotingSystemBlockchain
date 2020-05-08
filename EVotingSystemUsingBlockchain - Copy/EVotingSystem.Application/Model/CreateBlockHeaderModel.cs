using System;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockHeaderModel
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] PreviousHash { get; set; }

    }
}
