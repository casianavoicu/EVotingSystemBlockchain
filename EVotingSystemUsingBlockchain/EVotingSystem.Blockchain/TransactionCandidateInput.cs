using SQLite;

namespace EVotingSystem.Blockchain
{
    public class TransactionCandidateInput
    {
        [PrimaryKey, AutoIncrement]
        public int CandidateId { get; set; }

        [NotNull]
        public string Address { get; set; }

        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string LastName { get; set; }

        [NotNull]
        public string Details { get; set; }

        [NotNull]
        public string ElectionAddress { get; set; }

        [NotNull]
        public string Signature { get; set; }

        [NotNull]
        public int TransactionType { get; set; }
    }
}
