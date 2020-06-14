using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;

namespace Wallet.Services
{
    public static class CryptoService
    {
        public static byte[] CreateHash(string data)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return sHA256.ComputeHash(Encoding.UTF8.GetBytes(data));
            }

        }
        public static string CreateSignature(byte[] hashedData, byte[] privateKey)
        {
            EthECKey ethECKey = new EthECKey(privateKey.ToHex());

            return Convert.ToBase64String(ethECKey.Sign(hashedData).ToDER());
        }

    }
}
