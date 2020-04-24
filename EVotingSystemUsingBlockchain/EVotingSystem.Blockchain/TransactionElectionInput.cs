using SQLite;
using System;

namespace EVotingSystem.Blockchain
{
    public class TransactionElectionInput
    {
        [PrimaryKey, AutoIncrement]
        public int CandidateId { get; set; }

        [NotNull]
        public string Address { get; set; }

        [NotNull]
        public string ElectionName { get; set; }

        [NotNull]
        public DateTime StartDate { get; set; }

        [NotNull]
        public DateTime EndDate { get; set; }

        [NotNull]
        public bool Started { get; set; }

        [NotNull]
        public string Signature { get; set; }

        [NotNull]
        public int TransactionType { get; set; }
    }
}
