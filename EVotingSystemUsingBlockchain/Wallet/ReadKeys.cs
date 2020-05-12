using System;
using System.IO;

namespace Wallet
{
    public static class ReadKeys
    {
        public static (byte[], byte[]) ReadKeysFromFile(string password)
        {
            StreamReader streamReader = new StreamReader("keys.txt");
            var privateKey = Convert.FromBase64String(EncryptionHelper.Decrypt(streamReader.ReadLine(), password));
            var publicKey = Convert.FromBase64String(EncryptionHelper.Decrypt(streamReader.ReadLine(), password));
            return (privateKey, publicKey);
        }
    }
}
