using KeyPairServices;
using Models;
using System;
using System.Collections.Generic;

namespace Wallet.PrivateApplication
{
    public class TransactionInstitutionService
    {
        public string CreateBallotTransaction((byte[], byte[]) keyPair, List<string> candidates, string ballotName, DateTime endDate)
        {
            var model = new CreateBallotTransactionModelWithoutSignature
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

            var modelWithSignature = new CreateBallotTransactionModel(model, signature);

            return modelWithSignature.Serialize();
        }
    }
}
