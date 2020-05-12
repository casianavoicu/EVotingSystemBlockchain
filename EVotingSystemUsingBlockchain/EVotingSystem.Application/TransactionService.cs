using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;

namespace EVotingSystem.Application
{
    public class TransactionService
    {
        private readonly string Transaction = null;

        public TransactionService(string Transaction)
        {
            this.Transaction = Transaction;
        }

        public string ReceiveTransactionFromWallet()
        {
            CreateTransactionModel model = new CreateTransactionModel();
            CreateTransactionModel transaction = model.Deserialize(Transaction);

            if (CryptoUtils.ValidateTransaction(transaction.FromAddress, transaction.HashedData, transaction.Signature))
            {
                BlockService block = new BlockService();
                block.CreateBlock(transaction);
                return "Verified";
            }
            else
                return null;
            //save into local db
            //verify if transaction is type=1 =>increase balance
        }

    }
}
