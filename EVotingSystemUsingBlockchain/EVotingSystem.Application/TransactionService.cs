using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;

namespace EVotingSystem.Application
{
    public class TransactionService
    {
        private readonly string Transaction = null;

        public TransactionService(string Transaction)
        {
            this.Transaction = Transaction;
        }

        public CreateTransactionVoteModel ReceiveVoteTransactionFromWallet()
        {
            CreateTransactionVoteModel model = new CreateTransactionVoteModel();
            CreateTransactionVoteModel transaction = model.Deserialize(Transaction);

            if (EccUtils.ValidateTransaction(model.FromAddress, model.HashedData, model.Signature))
                return transaction;
            else
                return null;

        }

        public CreateTransactionInputModel ReceiveInputTransactionFromWallet()
        {
            CreateTransactionInputModel model = new CreateTransactionInputModel();
            CreateTransactionInputModel transaction = model.Deserialize(Transaction);

            if (EccUtils.ValidateTransaction(model.FromAddress, model.HashedData, model.Signature))
                return transaction;
            else
                return null;

        }

    }
}
