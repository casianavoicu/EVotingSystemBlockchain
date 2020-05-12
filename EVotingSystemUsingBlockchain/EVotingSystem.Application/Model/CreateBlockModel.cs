using Newtonsoft.Json;
using System;

namespace EVotingSystem.Application.Model
{
    public class CreateBlockModel
    {
        public int BlockIndex { get; set; }

        public DateTime TimeStamp { get; set; }

        public byte[] PreviousHash { get; set; }

        public string Transaction { get; set; }

        public byte[] BlockHash { get; set; }

        public CreateBlockModel Deserialize(string content)
        {

            var result = JsonConvert.DeserializeObject<CreateBlockModel>(content);

            return result;
        }
    }
}
