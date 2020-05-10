using System;
using Wallet.Model;

namespace Wallet
{
    public class Transaction
    {
        public string CreateNewTransaction(string receiverPublicKey, (byte[], byte[]) keyPair)
        {
            CreateTransactionModel model = new CreateTransactionModel
            {
                FromAddress = Convert.ToBase64String(keyPair.Item2),
                ToAddress = receiverPublicKey,
                Vote = "test", //zknp
                Timestamp = DateTime.Now.ToUniversalTime(),
                Type = 1
            };
            model.HashedData = CryptoService.CalculateTransactionHash(model.Vote, model.ToAddress, model.Timestamp, model.Type);
            model.Signature = CryptoService.CreateSignature(Convert.FromBase64String(model.HashedData), keyPair.Item1);

            return model.Serialize();
        }
    }
}
