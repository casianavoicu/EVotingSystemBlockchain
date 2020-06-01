using EVotingSystem.Application.Model;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application.Utils
{
    public static class CryptoUtils
    {
        public static bool ValidateTransaction(string fromAddress, string data, string signature, out string hashedData)
        {
            var sign = Convert.FromBase64String(fromAddress).ToHex();
            var eth = new EthECKey(sign.HexToByteArray(), false);
            var hashed = ComputeHash(data);
            hashedData = Convert.ToBase64String(hashed);
            return eth.Verify(hashed, EthECDSASignature.FromDER(Convert.FromBase64String(signature)));
        }

        public static byte[] ComputeHash(string data)
        {
            HMACSHA256 sha = new HMACSHA256();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        public static byte[] ComputeHashBlock(CreateHashBlockModel model)
        {
            HMACSHA256 sha = new HMACSHA256();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model.Serialize())));
        }

        public static byte[] ComputeStateRootHashBlock(CreateBlockStateRootHashModel model)
        {
            HMACSHA256 sha = new HMACSHA256();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model.Serialize())));
        }
    }
}
