﻿using System;
using System.Security.Cryptography;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;

namespace Wallet
{
    public static class CryptoService
    {
        public static byte[] CreateHash(string data)
        {
            //HMACSHA256 sha = new HMACSHA256();
            //return sha.ComputeHash(Encoding.UTF8.GetBytes(data));
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

        public static string CalculateTransactionHash(string vote, string address, DateTime dateTime, int type)
        {
            var data = vote + address + Convert.ToString(dateTime) + Convert.ToString(type);
            return Convert.ToBase64String(CreateHash(data));
        }
    }
}
