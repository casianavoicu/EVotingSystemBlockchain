using Models;
using System;
using System.Collections.Generic;
using Wallet.Services;

namespace Wallet.PrivateApplication
{
    public class TransactionInstitutionService
    {
        public string CreateBallotTransaction((byte[], byte[]) keyPair, List<string> candidates, string ballotName, DateTime endDate)
        {
            var model = new BallotTransactionModelWithoutSignature
            {
                FromAddress = Convert.ToBase64String(keyPair.Item2),
                ToAddress = Convert.ToBase64String(keyPair.Item2),
                BallotName = ballotName,
                Candidates = candidates,
                Timestamp = DateTime.Now.ToUniversalTime(),
                Type = "Ballot",
                EndDate = endDate
            };

            var hash = CryptoService.CreateHash(model.Serialize());

            var signature = CryptoService.CreateSignature(hash, keyPair.Item1);

            var modelWithSignature = new BallotTransactionModel(model, signature);

            return modelWithSignature.Serialize();
        }
    }
}
