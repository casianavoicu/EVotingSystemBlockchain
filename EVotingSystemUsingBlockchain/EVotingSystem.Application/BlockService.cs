using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using EVotingSystem.Blockchain;
using Newtonsoft.Json;
using System;

namespace EVotingSystem.Application
{
    public class BlockService
    {
        public BlockService()
        {
        }

        public string GenesisBlock()
        {
            CreateBlockModel genesisBlock = new CreateBlockModel
            {
                BlockIndex = 0,
                TimeStamp = DateTime.Now.ToUniversalTime(),
                PreviousHash = null,
                Transaction = "",
            };

            genesisBlock.BlockHash = CryptoUtils.ComputeHash(genesisBlock);

            //DbContext.InsertBlock(genesisBlock);
            return JsonConvert.SerializeObject(genesisBlock);
        }

        public string CreateBlock(CreateTransactionModel transaction)
        {
            //select last index and previous hash
            byte[] previousHash = { 1};
            int indexBlock = 1;
            string data = JsonConvert.SerializeObject(transaction);

            CreateBlockModel block = new CreateBlockModel
            {
                BlockIndex = indexBlock,
                TimeStamp = DateTime.Now.ToUniversalTime(),
                PreviousHash = previousHash,
                Transaction = data,
            };

            block.BlockHash = CryptoUtils.ComputeHash(block);

            Block dbBlock = new Block
            {
                BlockIndex = block.BlockIndex,
                TimeStamp = block.TimeStamp,
                PreviousHash = block.PreviousHash,
                Transaction = block.Transaction,
            };

            DbContext.InsertBlock(dbBlock);
            return JsonConvert.SerializeObject(block);
        }

        public CreateBlockModel ReceiveBlock(string block)
        {
            CreateBlockModel blockModel = new CreateBlockModel();
            CreateBlockModel createBlock = blockModel.Deserialize(block);

            //verify block
            //insert into db

            return null;
        }
    }
}
