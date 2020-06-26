using System;

namespace EVotingSystem.UI.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public string HashedData { get; set; }

        public string Type { get; set; }

        public string Details { get; set; }

        public int BlockId { get; set; }
    }
}
