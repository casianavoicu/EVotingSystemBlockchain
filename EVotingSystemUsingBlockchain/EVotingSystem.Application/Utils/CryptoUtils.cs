using KeyPairServices;
using Nethereum.Signer;
using System;

namespace EVotingSystem.Application.Utils
{
    public static class CryptoUtils
    {
        public static bool ValidateSignature(string fromAddress, string data, string signature, out string hash)
        {
            var eth = new EthECKey(Convert.FromBase64String(fromAddress), false);
            var hashed = CryptoService.CreateHash(data);
            hash = Convert.ToBase64String(hashed);
            return eth.Verify(hashed, EthECDSASignature.FromDER(Convert.FromBase64String(signature)));
        }
    }
}
