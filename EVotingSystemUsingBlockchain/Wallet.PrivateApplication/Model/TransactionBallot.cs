using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Wallet.PrivateApplication.Model
{
    public class TransactionBallot
    {
        public string FromAddress { get; set; }

        public string BallotAddress { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public string Details { get; set; } //End type

        public string Type { get; set; }

        public List<CandidateModel> Candidates { get; set; } = new List<CandidateModel>();

        public string Serialize()
        {
            var model = new TransactionBallot
            {
                FromAddress = FromAddress,
                Signature = Signature,
                Timestamp = Timestamp,
                Candidates = Candidates,
                BallotAddress = BallotAddress,
                Details = Details,
                Type = Type
            };

            return JsonConvert.SerializeObject(model);
        }
    }
}
