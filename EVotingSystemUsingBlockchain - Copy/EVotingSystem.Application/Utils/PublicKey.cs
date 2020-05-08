using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application.Utils
{
    public class PublicKey
    {
        public string StringExponent;
        public string StringModulus;
        public byte[] Exponent;
        public byte[] Modulus;

        public PublicKey()
        {

        }
        public PublicKey(RSAParameters RSAParameters)
        {
            StringExponent = Convert.ToBase64String(RSAParameters.Exponent);
            StringModulus = Convert.ToBase64String(RSAParameters.Modulus);
        }


        public PublicKey(string keyString)
        {
            keyString = Encoding.UTF8.GetString(Convert.FromBase64String(keyString));

            var publicKey = JsonConvert.DeserializeObject<PublicKey>(keyString);

            Exponent = Convert.FromBase64String(publicKey.StringExponent);
            Modulus = Convert.FromBase64String(publicKey.StringModulus);
        }

        public override string ToString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
        }
    }
}
