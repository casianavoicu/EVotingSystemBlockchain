using System;

namespace EVotingSystem.UI.Models
{
    public class BlockViewModel
    {
        public string BlockId { get; set; }

        public string BlockIndex { get; set; }

        public string TimeStamp { get; set; }

        public string PreviousHash { get; set; }

        public string Hash { get; set; }

        public string StateRootHash { get; set; }

        public string PublicKey { get; set; }

        public string Signature { get; set; }
    }
}
