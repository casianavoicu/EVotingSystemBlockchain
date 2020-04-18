namespace EVotingSystem.Application.Model
{
    public class CreateVoteTransactionModel
    {
        public string VoterAddress { get; set; }

        public string ElectionAddress { get; set; }

        public string CandidateAddress { get; set; }
    }
}
