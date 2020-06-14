using Newtonsoft.Json;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockModel
    {
        public CreateHashBlockModel Block { get; set; }

        public string StateRootHash { get; set; }

        public string Signature { get; set; }

        public CreateBlockModel Deserialize(string content)
        {
            var result = JsonConvert.DeserializeObject<CreateBlockModel>(content);

            return result;
        }

        public string Serialize()
        {
            var serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            var model = new CreateBlockModel
            {
                Block = new CreateHashBlockModel
                {
                    BlockIndex = Block.BlockIndex,
                    PreviousHash = Block.PreviousHash,
                    TimeStamp = Block.TimeStamp,
                    Transactions = Block.Transactions
                },
               StateRootHash = StateRootHash
            };

            return JsonConvert.SerializeObject(model, serializeSettings);
        }
    }
}
