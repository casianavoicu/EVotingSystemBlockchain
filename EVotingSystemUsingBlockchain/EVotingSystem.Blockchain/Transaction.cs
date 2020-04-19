namespace EVotingSystem.Blockchain
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string CandidateAddress { get; set; }

        public string Signature { get; set; }
    }
}
