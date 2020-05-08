namespace EVotingSystem.Application.Model
{
    public class CreateVoteModel
    {
        public string AccountAddress { get; set; }

        public string PrivateKey { get; set; }

        public CreateVoteOption Vote { get; set; }
    }
}
