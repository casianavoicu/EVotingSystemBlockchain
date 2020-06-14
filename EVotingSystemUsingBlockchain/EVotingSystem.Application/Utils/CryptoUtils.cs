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
        public static bool ValidateTransaction(string fromAddress, TransactionModel data, string signature, out string hashedData)
        {
            var sign = Convert.FromBase64String(fromAddress).ToHex();
            var eth = new EthECKey(sign.HexToByteArray(), false);
            var hashed = ComputeHash(data.Vote + data.ToAddress + Convert.ToString(data.Timestamp) + Convert.ToString(data.Type));
            hashedData = Convert.ToBase64String(hashed);
            return eth.Verify(hashed, EthECDSASignature.FromDER(Convert.FromBase64String(signature)));
        }

        public static byte[] ComputeHash(string data)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return sHA256.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        public static byte[] ComputeHashBlock(CreateHashBlockModel model)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return sHA256.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model.Serialize())));
            }
        }

        public static byte[] ComputeStateRootHashBlock(CreateBlockStateRootHashModel model)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return sHA256.ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model.Serialize())));
            }
        }

        public static string CreateSignature(byte[] hashedData, byte[] privateKey)
        {
            EthECKey ethECKey = new EthECKey(privateKey.ToHex());

            return Convert.ToBase64String(ethECKey.Sign(hashedData).ToDER());
        }
    }
}
