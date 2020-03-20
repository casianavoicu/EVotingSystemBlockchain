using EVotingSystem.Application.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EVotingSystem.Application
{
    public class BlockService : IBlockService
    {
        public BlockService()
        {
        }

        public byte[] CalculateHash(CreateToken createToken)
        {
            SHA256 sha256 = SHA256.Create();

            var format = createToken.BlockIndex.ToString() + createToken.PreviousHash + createToken.TimeStamp.ToString() + createToken.Data;

            Console.WriteLine(format);

            var computeHash = Encoding.ASCII.GetBytes(format);

            return sha256.ComputeHash(computeHash);
        }
    }
}
