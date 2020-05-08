using EVotingSystem.Application.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application.Utils
{
    public static class HashExtention
    {
        public static byte[] ComputeBlockHash256(CreateBlockHeaderModel createToken)
        {
            var format = createToken.BlockIndex.ToString() + createToken.PreviousHash + createToken.TimeStamp.ToString();

            return EncodeFormat(format);
        }

        public static byte[] CalculateCandidateTransactionHash(CreateTransactionInputModel<CreateCandidateModel> transaction)
        {
            //var format = transaction.Address + transaction. + transaction.Details;

            return null;//EncodeFormat(format);
        }

        private static byte[] EncodeFormat(string format)
        {
            SHA256 sha256 = SHA256.Create();

            var computeHash = Encoding.ASCII.GetBytes(format);

            return sha256.ComputeHash(computeHash);
        }

       
    }
}
