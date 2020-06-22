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

    }

    public class TransactionBallotModel : TransactionModel
    {
        public TransactionBallotModel()
        {

        }

        //public TransactionBallotModel(TransactionModel transaction)
        //{
        //    FromAddress = transaction.FromAddress;
        //    ToAddress = transaction.ToAddress;
        //    Timestamp = transaction.Timestamp;
        //    EndDate = this.EndDate;
        //    Type = transaction.Type;
        //    Signature = transaction.Signature;
        //    Candidates = this.Candidates;
        //    BallotName = this.BallotName;
        //}

        public DateTime EndDate { get; set; }

        public List<string> Candidates { get; set; } = new List<string>();

        public string BallotName { get; set; }
    }

    public class TransactionVoteModel : TransactionModel
    {
        public TransactionVoteModel()
        {

        }

        public TransactionVoteModel(TransactionModel transaction, string vote, string details)
        {
            Details = details;
            FromAddress = transaction.FromAddress;
            Timestamp = transaction.Timestamp;
            ToAddress = transaction.ToAddress;
            Type = transaction.Type;
            Vote =vote;
            Signature = transaction.Signature;
        }


        public string Vote { get; set; }

        public string Details { get; set; }
    }
}
