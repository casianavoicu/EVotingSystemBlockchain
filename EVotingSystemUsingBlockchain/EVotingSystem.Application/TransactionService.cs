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

        public CreateTransactionModel ReceiveTransactionFromWallet()
        {
            CreateTransactionModel model = new CreateTransactionModel();
            CreateTransactionModel transaction = model.Deserialize(Transaction);

            if (EccUtils.ValidateTransaction(transaction.FromAddress, transaction.HashedData, transaction.Signature))
                return transaction;
            else
                return null;
            //save into local db
            //verify if transaction is type=1 =>increase balance
        }

    }
}
