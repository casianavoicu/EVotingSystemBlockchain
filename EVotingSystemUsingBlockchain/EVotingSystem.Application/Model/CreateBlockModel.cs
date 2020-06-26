using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockModel : CreateBlockModelWithoutSignatureAndRootHash
    {

        public string StateRootHash { get; set; }

        public string Signature { get; set; }

        public CreateBlockModel()
        {
        }

        public CreateBlockModel(CreateBlockModelWithoutSignatureAndRootHash model, string stateRootHash, string signature)
        {
            BlockIndex = model.BlockIndex;
            TimeStamp = model.TimeStamp;
            PreviousHash = model.PreviousHash;
            Transactions = model.Transactions;
            StateRootHash = stateRootHash;
            Signature = signature;
            PublicKey = model.PublicKey;
        }

        public CreateBlockModel Deserialize(string model)
        {
            var jObject = Newtonsoft.Json.Linq.JObject.Parse(model);

            var transactionsJObject = jObject["Transactions"];

            var transactionChildren = transactionsJObject.Children();

            var createBlockModel = JsonConvert.DeserializeObject<CreateBlockModel>(model);

            createBlockModel.Transactions.Clear();

            foreach (var transaction in transactionChildren)
            {
                var baseTransaction = Newtonsoft.Json.JsonConvert.DeserializeObject<TransactionModel>(transaction.ToString());

                if (baseTransaction.Type == "Ballot")
                {
                    var ballotTransaction = JsonConvert.DeserializeObject<TransactionBallotModel>(transaction.ToString());

                    createBlockModel.Transactions.Add(ballotTransaction);
                }
                else
                {
                    var voteTransaction = JsonConvert.DeserializeObject<TransactionVoteModel>(transaction.ToString());

                    createBlockModel.Transactions.Add(voteTransaction);
                }
            }

            return createBlockModel;
        }
    }

    public class CreateBlockModelWithoutSignatureAndRootHash
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PreviousHash { get; set; }

        public string PublicKey { get; set; }

        public List<TransactionModel> Transactions { get; set; }

        public CreateBlockModelWithoutSignatureAndRootHash()
        {

        }

        public CreateBlockModelWithoutSignatureAndRootHash(CreateBlockModel blockModel)
        {
            BlockIndex = blockModel.BlockIndex;
            TimeStamp = blockModel.TimeStamp;
            PreviousHash = blockModel.PreviousHash;
            Transactions = blockModel.Transactions;
            PublicKey = blockModel.PublicKey;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
