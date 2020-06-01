using Newtonsoft.Json;
using System;

namespace Wallet.Model
{
    public class CreateTransactionModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public int Type { get; set; }

        public string Serialize()
        {
            var serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            var model = new CreateTransactionModel
            {
                FromAddress = FromAddress,
                ToAddress = ToAddress,
                Signature = Signature,
                Timestamp = Timestamp,
                Vote = Vote,
                Type = Type
            };

            return JsonConvert.SerializeObject(model, serializeSettings);
        }
    }
}
