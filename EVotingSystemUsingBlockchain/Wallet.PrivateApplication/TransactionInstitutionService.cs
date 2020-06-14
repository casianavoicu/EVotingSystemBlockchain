using System;
using System.Collections.Generic;
using Wallet.PrivateApplication.Model;
using Wallet.Services;

namespace Wallet.PrivateApplication
{
    public class TransactionInstitutionService
    {
        public string CreateBallotTransaction((byte[], byte[]) keyPair, List<string> Candidates, DateTime endDate)
        {
            List<CandidateModel> finalCandidates = new List<CandidateModel>();

            foreach (var item in Candidates)
            {
                finalCandidates.Add(new CandidateModel
                {
                    FullName = item,
                    NumberOfVotes = 0
                });
            }

            TransactionBallot ballot = new TransactionBallot
            {
                Candidates = finalCandidates,
                FromAddress = Convert.ToBase64String(keyPair.Item2),
                Timestamp = DateTime.Now.ToUniversalTime(),
                BallotAddress = Convert.ToBase64String(keyPair.Item2),
                Details = Convert.ToString(endDate),
                Type = "Ballot"
            };

            TransactionHashModel hashModel = new TransactionHashModel
            {
                BallotAddress = ballot.BallotAddress,
                Candidates = ballot.Candidates,
                Timestamp = ballot.Timestamp,
                Type = ballot.Type,
                Details = ballot.Details,
            };

            string hashedData = Convert.ToBase64String(CryptoService.CreateHash(hashModel.Serialize()));

            ballot.Signature = CryptoService.CreateSignature(Convert.FromBase64String(hashedData), keyPair.Item1);

            return ballot.Serialize();
        }
    }
}
