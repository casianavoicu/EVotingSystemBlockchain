namespace EVotingSystem.Application.Model
{
    public class CreateTransactionVoteModel
    {
        public AccountModel AccountModel { get; set; }

        public CreateVoteOption Vote { get; set; }

        public string Signature { get; set; }
    }
}
