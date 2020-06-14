using System;
using System.Text;
using Wallet.Services.Model;

namespace Wallet.Services
{
    public class TransactionService
    {
        public string CreateNewTransaction(string receiverPublicKey, (byte[], byte[]) keyPair, string vote)
        {
            CreateTransactionModel model = new CreateTransactionModel
            {
                FromAddress = Convert.ToBase64String(keyPair.Item2),
                ToAddress = receiverPublicKey,
                Vote = vote,
                Timestamp = DateTime.Now.ToUniversalTime(),
                Details = null,
                Type = "Vote"
            };

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(model.Vote);
            stringBuilder.Append(model.ToAddress);
            stringBuilder.Append(Convert.ToString(model.Timestamp));

            var hash = CryptoService.CreateHash(stringBuilder.ToString());

            model.Signature = CryptoService.CreateSignature(hash, keyPair.Item1);

            return model.Serialize();
        }
    }
}
