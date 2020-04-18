using EVotingSystem.Application.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application.Utils
{
    public static class HashExtention
    {
        public static byte[] ComputeBlockHash256(CreateTokenModel createToken)
        {
            var format = createToken.BlockIndex.ToString() + createToken.PreviousHash + createToken.TimeStamp.ToString() + createToken.Data;

            return EncodeFormat(format);
        }

        public static byte[] CalculateCandidateTransactionHash(TransactionOutputModel<CreateCandidateModel> transaction)
        {
            //var format = transaction.Address + transaction. + transaction.Details;

            return 0;//EncodeFormat(format);
        }

        private static byte[] EncodeFormat(string format)
        {
            SHA256 sha256 = SHA256.Create();

            var computeHash = Encoding.ASCII.GetBytes(format);

            return sha256.ComputeHash(computeHash);
        }
    }
}
