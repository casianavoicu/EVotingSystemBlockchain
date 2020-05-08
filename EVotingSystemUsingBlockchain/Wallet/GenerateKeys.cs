﻿using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;
using System.IO;
using System.Text;

namespace Wallet
{
    public static class GenerateKeys
    {
        public static (string, string) CreateKeyPair(string password)
        {
            EthECKey ethECKey = EthECKey.GenerateKey();
            var privateKey = ethECKey.GetPrivateKeyAsBytes();
            var publicKey = ethECKey.GetPubKey();

            string privateKeyString = Convert.ToBase64String(privateKey);
            string publicKeyString = Convert.ToBase64String(publicKey);
            string encryptedPrivateKey = EncryptionHelper.Encrypt(privateKeyString, password);
            string encryptedPublicKey = EncryptionHelper.Encrypt(publicKeyString, password);

            StreamWriter streamWriter = new StreamWriter("keys.txt");
            streamWriter.WriteLine(encryptedPrivateKey);
            streamWriter.WriteLine(encryptedPublicKey);
            streamWriter.Close();
            return (encryptedPrivateKey, encryptedPublicKey);
        }
    }
}
