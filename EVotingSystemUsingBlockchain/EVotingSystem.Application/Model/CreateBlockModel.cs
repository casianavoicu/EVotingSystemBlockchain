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
            return JsonConvert.DeserializeObject<CreateBlockModel>(model);
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
