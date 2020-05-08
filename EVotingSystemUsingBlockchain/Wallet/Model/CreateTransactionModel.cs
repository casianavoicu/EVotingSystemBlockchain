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

        public string HashedData { get; set; }

        public string Serialize()
        {
            var deserializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            var model = new CreateTransactionModel
            {
                FromAddress = FromAddress,
                ToAddress = ToAddress,
                HashedData = HashedData,
                Signature = Signature,
                Timestamp = Timestamp,
                Vote = Vote
            };

            return JsonConvert.SerializeObject(model, deserializeSettings);
        }
    }
}
