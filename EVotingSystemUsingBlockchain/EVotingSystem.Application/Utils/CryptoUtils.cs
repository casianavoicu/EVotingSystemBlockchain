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
        public static bool ValidateTransaction(string fromAddress, string hashedData, string signature)
        {
            var sign = Convert.FromBase64String(fromAddress).ToHex();
            var eth = new EthECKey(sign.HexToByteArray(), false);

            return eth.Verify(Convert.FromBase64String(hashedData), EthECDSASignature.FromDER(Convert.FromBase64String(signature)));
        }

        public static byte[] ComputeHash(CreateBlockModel blockModel )
        {
            HMACSHA256 sha = new HMACSHA256();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(blockModel)));
        }
    }
}
