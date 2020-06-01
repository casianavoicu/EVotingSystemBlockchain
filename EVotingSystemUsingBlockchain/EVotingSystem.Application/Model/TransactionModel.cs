using Newtonsoft.Json;
using System;

namespace EVotingSystem.Application.Model
{
    public class TransactionModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public int Type { get; set; }

        public TransactionModel Deserialize(string content)
        {
            var result = JsonConvert.DeserializeObject<TransactionModel>(content);

            return result;
        }
    }
}
