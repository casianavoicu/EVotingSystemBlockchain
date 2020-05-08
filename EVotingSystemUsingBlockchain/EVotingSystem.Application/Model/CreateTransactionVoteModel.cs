using Newtonsoft.Json;
using System;

namespace EVotingSystem.Application.Model
{
    public class CreateTransactionVoteModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public string HashedData { get; set; }

        public CreateTransactionVoteModel Deserialize(string content)
        {

            var result = JsonConvert.DeserializeObject<CreateTransactionVoteModel>(content);

            return result;
        }

    }
}
