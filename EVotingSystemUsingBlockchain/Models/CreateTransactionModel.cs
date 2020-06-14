using Newtonsoft.Json;
using System;

namespace Models
{
    public class CreateTransactionModel : CreateTransactionModelWithoutSignature
    {
        public string Signature { get; set; }

        public CreateTransactionModel()
        {

        }

        public CreateTransactionModel(CreateTransactionModelWithoutSignature createTransactionModelWithoutSignature, string signature)
        {
            Details = createTransactionModelWithoutSignature.Details;
            FromAddress = createTransactionModelWithoutSignature.FromAddress;
            Timestamp = createTransactionModelWithoutSignature.Timestamp;
            ToAddress = createTransactionModelWithoutSignature.ToAddress;
            Type = createTransactionModelWithoutSignature.Type;
            Vote = createTransactionModelWithoutSignature.Vote;
            Signature = signature;
        }
    }

    public class CreateTransactionModelWithoutSignature
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public DateTime Timestamp { get; set; }

        public string Details { get; set; }

        public string Type { get; set; }

        public CreateTransactionModelWithoutSignature()
        {

        }

        public CreateTransactionModelWithoutSignature(CreateTransactionModel createTransactionModel)
        {
            Details = createTransactionModel.Details;
            FromAddress = createTransactionModel.FromAddress;
            Timestamp = createTransactionModel.Timestamp;
            ToAddress = createTransactionModel.ToAddress;
            Type = createTransactionModel.Type;
            Vote = createTransactionModel.Vote;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
