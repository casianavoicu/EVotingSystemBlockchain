namespace EVotingSystem.Application.Model
{
    public class TransactionVoteModel
    {
        public string VoterAddress { get; set; }

        public string CandidateAddress { get; set; }

        public string ElectionAddress { get; set; }

        public string Signature { get; set; }
    }
}
