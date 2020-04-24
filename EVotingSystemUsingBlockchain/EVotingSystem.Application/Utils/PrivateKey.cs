using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application.Utils
{
    public class PrivateKey
    {
        public string stringP;
        public string stringQ;
        public string stringD;
        public string stringDP;
        public string stringDQ;
        public string stringInverseQ;

        public byte[] P;
        public byte[] Q;
        public byte[] D;
        public byte[] DP;
        public byte[] DQ;
        public byte[] InverseQ;

        public PrivateKey()
        {

        }

        public PrivateKey(RSAParameters RSAParameters)
        {
            stringP = Convert.ToBase64String(RSAParameters.P);
            stringQ = Convert.ToBase64String(RSAParameters.Q);
            stringD = Convert.ToBase64String(RSAParameters.D);
            stringDP = Convert.ToBase64String(RSAParameters.DP);
            stringDQ = Convert.ToBase64String(RSAParameters.DQ);
            stringInverseQ = Convert.ToBase64String(RSAParameters.InverseQ);
        }

        public PrivateKey(string keyString)
        {
            keyString = Encoding.UTF8.GetString(Convert.FromBase64String(keyString));

            var privateKey = JsonConvert.DeserializeObject<PrivateKey>(keyString);

            P = Convert.FromBase64String(privateKey.stringP);
            Q = Convert.FromBase64String(privateKey.stringQ);
            D = Convert.FromBase64String(privateKey.stringD);
            DP = Convert.FromBase64String(privateKey.stringDP);
            DQ = Convert.FromBase64String(privateKey.stringDQ);
            InverseQ = Convert.FromBase64String(privateKey.stringInverseQ);
        }

        public override string ToString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
        }
    }
}
