using System;

namespace EVotingSystem.Application.Model
{
    public class BlockModel<T>
    {
        public CreateBlockHeaderModel BlockHeader { get; set; }

        public T Transaction { get; set; }

        public byte[] BlockHash { get; set; }

        public int Difficulty { get; set; }
    }
}
