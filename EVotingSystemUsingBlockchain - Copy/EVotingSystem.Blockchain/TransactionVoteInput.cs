using SQLite;

namespace EVotingSystem.Blockchain
{
    public class TransactionVoteInput
    {
        [PrimaryKey, AutoIncrement]
        public int VoteId { get; set; }

        [NotNull]
        public string VoterAddress { get; set; }

        [NotNull]
        public string CandidateAddress { get; set; }

        [NotNull]
        public string ElectionAddress { get; set; }

        [NotNull]
        public int TransactionType { get; set; }
    }
}
