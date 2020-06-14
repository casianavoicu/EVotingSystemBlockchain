using Models;
using System;

namespace Wallet.Services
{
    public class TransactionService
    {
        public string CreateNewTransaction(string receiverPublicKey, (byte[], byte[]) keyPair, string vote, string type)
        {
            var model = new CreateTransactionModelWithoutSignature
            {
                FromAddress = Convert.ToBase64String(keyPair.Item2),
                ToAddress = receiverPublicKey,
                Vote = vote,
                Timestamp = DateTime.Now.ToUniversalTime(),
                Details = null,
                Type = type
            };

            var hash = CryptoService.CreateHash(model.Serialize());

            var signature = CryptoService.CreateSignature(hash, keyPair.Item1);

            var modelWithSignature = new CreateTransactionModel(model, signature);

            return modelWithSignature.Serialize();
        }
    }
}
