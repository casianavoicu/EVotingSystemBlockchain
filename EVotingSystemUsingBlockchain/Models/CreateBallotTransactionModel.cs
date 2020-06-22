using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models
{
    public class CreateBallotTransactionModelWithoutSignature
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime EndDate { get; set; }

        public string Type { get; set; }

        public List<string> Candidates { get; set; } = new List<string>();

        public string BallotName { get; set; }

        public CreateBallotTransactionModelWithoutSignature()
        {

        }

        public CreateBallotTransactionModelWithoutSignature(CreateBallotTransactionModel ballotTransactionModel)
        {
            FromAddress = ballotTransactionModel.FromAddress;
            ToAddress = ballotTransactionModel.ToAddress;
            Timestamp = ballotTransactionModel.Timestamp;
            EndDate = ballotTransactionModel.EndDate;
            Type = ballotTransactionModel.Type;
            Candidates = ballotTransactionModel.Candidates;
            BallotName = ballotTransactionModel.BallotName;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class CreateBallotTransactionModel : CreateBallotTransactionModelWithoutSignature
    {
        public string Signature { get; set; }

        public CreateBallotTransactionModel()
        {

        }

        public CreateBallotTransactionModel(CreateBallotTransactionModelWithoutSignature ballotTransactionModelWithoutSignature, string signature)
        {
            FromAddress = ballotTransactionModelWithoutSignature.FromAddress;
            ToAddress = ballotTransactionModelWithoutSignature.ToAddress;
            Timestamp = ballotTransactionModelWithoutSignature.Timestamp;
            EndDate = ballotTransactionModelWithoutSignature.EndDate;
            Type = ballotTransactionModelWithoutSignature.Type;
            Candidates = ballotTransactionModelWithoutSignature.Candidates;
            BallotName = ballotTransactionModelWithoutSignature.BallotName;
            Signature = signature;
        }
    }
}
