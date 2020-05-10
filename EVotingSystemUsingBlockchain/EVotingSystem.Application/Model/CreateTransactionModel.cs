using Newtonsoft.Json;
using System;

namespace EVotingSystem.Application.Model
{
    public class CreateTransactionModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public string HashedData { get; set; }

        public int Type { get; set; }

        public CreateTransactionModel Deserialize(string content)
        {

            var result = JsonConvert.DeserializeObject<CreateTransactionModel>(content);

            return result;
        }
    }
}
