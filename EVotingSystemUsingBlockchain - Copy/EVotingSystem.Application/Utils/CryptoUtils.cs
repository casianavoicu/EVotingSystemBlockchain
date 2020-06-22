using KeyPairServices;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;

namespace EVotingSystem.Application.Utils
{
    public static class CryptoUtils
    {
        public static bool ValidateTransaction(string fromAddress, string data, string signature, out string hash)
        {
            var sign = Convert.FromBase64String(fromAddress).ToHex();
            var eth = new EthECKey(sign.HexToByteArray(), false);
            var hashed = CryptoService.CreateHash(data);
            hash = Convert.ToBase64String(hashed);
            return eth.Verify(hashed, EthECDSASignature.FromDER(Convert.FromBase64String(signature)));
        }
    }
}
