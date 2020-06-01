using Newtonsoft.Json;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockStateRootHashModel
    {
        public CreateHashBlockModel Block { get; set; }

        public string Hash { get; set; }

        public string Serialize()
        {
            var serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };

            var model = new CreateBlockStateRootHashModel
            {
                Block= new CreateHashBlockModel
                {
                    BlockIndex = Block.BlockIndex,
                    PreviousHash = Block.PreviousHash,
                    TimeStamp = Block.TimeStamp,
                    Transactions = Block.Transactions
                },
                Hash = Hash
            };

            return JsonConvert.SerializeObject(model, serializeSettings);
        }
    }
}
