namespace EVotingSystem.Application.Model
{
    public class TransactionOutputModel<T>
    {
        public string Address { get; set; }

        public T Transaction { get; set; }

        public string Signature { get; set; }
    }
}
