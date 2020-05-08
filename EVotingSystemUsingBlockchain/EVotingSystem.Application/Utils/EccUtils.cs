using EVotingSystem.Application.Model;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System;

namespace EVotingSystem.Application.Utils
{
    public static class EccUtils
    {

        public static bool ValidateTransaction(string fromAddress, string hashedData, string signature)
        {
            var sign = Convert.FromBase64String(fromAddress).ToHex();
            var eth = new EthECKey(sign.HexToByteArray(), false);

            return eth.Verify(Convert.FromBase64String(hashedData), EthECDSASignature.FromDER(Convert.FromBase64String(signature)));
        }
    }
}
