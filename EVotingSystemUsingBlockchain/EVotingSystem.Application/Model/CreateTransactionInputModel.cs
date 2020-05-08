using Newtonsoft.Json;

namespace EVotingSystem.Application.Model
{
    public class CreateTransactionInputModel
    {
        public string FromAddress { get; set; }

        public int ToAddress { get; set; }

        public int Vote { get; set; }

        public string HashedData { get; set; }

        public string Signature { get; set; }

        public CreateTransactionInputModel Deserialize(string content)
        {
            var deserializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            var result = JsonConvert.DeserializeObject<CreateTransactionInputModel>(content, deserializeSettings);

            this.ToAddress = result.ToAddress;
            this.FromAddress = result.FromAddress;
            this.HashedData = result.HashedData;
            this.Signature = result.Signature;
            this.Vote = result.Vote;

            return this;
        }
    }
}
