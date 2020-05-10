﻿using System;

namespace Wallet.Model
{
    public class TransactionHash
    {
        public string Candidate { get; set; }

        public string Vote { get; set; }

        public DateTime Time { get; set; }

        public int Type { get; set; }
    }
}
