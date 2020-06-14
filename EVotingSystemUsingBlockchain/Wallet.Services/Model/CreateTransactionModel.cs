using Newtonsoft.Json;
using System;

namespace Wallet.Services.Model
{
    public class CreateTransactionModel
    {
        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Vote { get; set; }

        public string Signature { get; set; }

        public DateTime Timestamp { get; set; }

        public string Details { get; set; }

        public string Type { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
