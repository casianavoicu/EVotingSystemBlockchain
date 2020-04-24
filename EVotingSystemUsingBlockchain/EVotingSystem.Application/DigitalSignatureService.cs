using EVotingSystem.Application.Constants;
using EVotingSystem.Application.Interface;
using EVotingSystem.Application.Utils;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application
{
    public class DigitalSignatureService : IDigitalSignatureService
    {
        public  void AssignKey()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            RSAParameters RSAKeyInfo = RSA.ExportParameters(true);

            PublicKey publicKey = new PublicKey(RSAKeyInfo);
            PrivateKey privateKey = new PrivateKey(RSAKeyInfo);
            //current directory!!
            var path = @"D:\Wallet.txt";
            string[] lines = {$"{WalletConstant.privateKeyDescription}", $"{ privateKey }",
                              $"{ WalletConstant.finalPrivateKey }", $"{ WalletConstant.publicKeyDescription }",
                              $"{ publicKey }", $"{ WalletConstant.finalPublickKey }" };
            File.WriteAllLines(path, lines);
            //write public key in dbb!!
            //create account address
        }

        public byte[] SignData(byte[] hashOfDataToSign)
        {
            string pK = "sdfs";

            PrivateKey privateKey = new PrivateKey(pK);

            RSAParameters RSAParameters = new RSAParameters
            {
                P = privateKey.P,
                Q = privateKey.Q,
                D = privateKey.D,
                DP = privateKey.DP,
                DQ = privateKey.DQ,
                InverseQ = privateKey.InverseQ,
            };

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(RSAParameters);

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");

                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public bool VerifySiganture(byte[] hashOfData, byte[] Signature)
        {
            //read from db the public key
            string publick = "asda";
            PublicKey publicKey = new PublicKey(publick);

            RSAParameters RSAParameters = new RSAParameters
            {
                Modulus = publicKey.Modulus,
                Exponent = publicKey.Exponent
            };
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(RSAParameters);

                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");

                return rsaDeformatter.VerifySignature(hashOfData, Signature);
            }
        }
    }
}

