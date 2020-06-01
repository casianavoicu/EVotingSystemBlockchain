using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EVotingSystem.Application.Model
{
    public class CreateHashBlockModel
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PreviousHash { get; set; }

        public List<TransactionModel> Transactions { get; set; }

        public string Serialize()
        {
            var serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            var model = new CreateHashBlockModel
            {
               BlockIndex = BlockIndex,
               PreviousHash = PreviousHash,
               TimeStamp = TimeStamp,
               Transactions = Transactions
            };

            return JsonConvert.SerializeObject(model, serializeSettings);
        }
    }
}
