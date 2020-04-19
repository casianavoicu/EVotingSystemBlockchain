namespace EVotingSystem.Application.Model
{
    public class CreateTransactionVoteModel
    {
        public CreateVoteModel VoteModel { get; set; }

        public string Signature { get; set; }
    }
}
