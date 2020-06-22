using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EVotingSystem.Application.Model
{
    public class TransactionModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public string Type { get; set; }

        public TransactionModel Deserialize(string content)
        {
            var result = JsonConvert.DeserializeObject<TransactionModel>(content);

            return result;
        }

        public TransactionModel()
        {

        }

    }

    public class TransactionBallotModel : TransactionModel
    {
        public TransactionBallotModel()
        {

        }

        public DateTime EndDate { get; set; }

        public List<string> Candidates { get; set; } = new List<string>();

        public string BallotName { get; set; }
    }

    public class TransactionVoteModel : TransactionModel
    {
        public TransactionVoteModel()
        {

        }

        public string Vote { get; set; }

        public string Details { get; set; }
    }
}
