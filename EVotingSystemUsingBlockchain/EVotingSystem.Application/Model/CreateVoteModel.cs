namespace EVotingSystem.Application.Model
{
    public class CreateVoteModel
    {
        public string AccountAddress { get; set; }

        public string PrivateKey { get; set; }

        public string ElectionAddress { get; set; }

        public string CandidateAddress { get; set; }
    }
}
