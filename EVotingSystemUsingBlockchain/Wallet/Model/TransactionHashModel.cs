using System;

namespace Wallet.Model
{
    public class TransactionHashModel
    {
        public string Address { get; set; }

        public string Vote { get; set; }

        public DateTime Time { get; set; }

        public int Type { get; set; }
    }
}
