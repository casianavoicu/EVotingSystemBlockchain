using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using EVotingSystem.Blockchain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BlockService
    {
        public string GenesisBlock()
        {

            CreateHashBlockModel genesisBlock = new CreateHashBlockModel
            {
                BlockIndex = 0,
                TimeStamp = new DateTime(2020, 6, 1, 14, 0, 0),
                PreviousHash = null,
                Transactions = null,
            };

            CreateBlockStateRootHashModel rootHashModel = new CreateBlockStateRootHashModel
            {
                Block = genesisBlock,
                Hash = Convert.ToBase64String(CryptoUtils.ComputeHashBlock(genesisBlock)),
            };

            CreateBlockModel finalBlock = new CreateBlockModel
            {
                Block = genesisBlock,
                StateRootHash = Convert.ToBase64String(CryptoUtils.ComputeStateRootHashBlock(rootHashModel))
            };

            DbContext.InsertBlock(new Block
            {
                BlockIndex = genesisBlock.BlockIndex,
                PreviousHash = genesisBlock.PreviousHash,
                TimeStamp = genesisBlock.TimeStamp,
                StateRootHash = finalBlock.StateRootHash,
                Hash = rootHashModel.Hash
            });


            return JsonConvert.SerializeObject(genesisBlock);
        }

        public string CreateBlock(List<(TransactionModel, string)> transaction)
        {
            var previousBlock = DbContext.PreviousBlock();
            List<TransactionModel> transactionsReceived = new List<TransactionModel>();
            foreach (var item in transaction)
            {
                transactionsReceived.Add(item.Item1);
            }

            CreateHashBlockModel model = new CreateHashBlockModel
            {
                BlockIndex = previousBlock.BlockIndex++,
                TimeStamp = DateTime.Now.ToUniversalTime(),
                PreviousHash = previousBlock.Hash,
                Transactions = transactionsReceived
            };


            CreateBlockStateRootHashModel rootHashModel = new CreateBlockStateRootHashModel
            {
                Block = model,
                Hash = Convert.ToBase64String(CryptoUtils.ComputeHashBlock(model)),
            };

            CreateBlockModel finalBlock = new CreateBlockModel
            {
                Block = model,
                StateRootHash = Convert.ToBase64String(CryptoUtils.ComputeStateRootHashBlock(rootHashModel))
            };

            DbContext.InsertBlock(new Block
            {
                BlockIndex = model.BlockIndex,
                PreviousHash = model.PreviousHash,
                TimeStamp = model.TimeStamp,
                StateRootHash = finalBlock.StateRootHash,
                Hash = rootHashModel.Hash
            });

            var blockId = DbContext.BlockId(model.BlockIndex);

            foreach (var item in transaction)
            {
                Transaction trans = new Transaction
                {
                    BlockId = blockId,
                    FromAddress = item.Item1.FromAddress,
                    ToAddress = item.Item1.ToAddress,
                    Signature = item.Item1.Signature,
                    Timestamp = item.Item1.Timestamp,
                    Type = item.Item1.Type,
                    HashedData = item.Item2,
                    Vote = item.Item1.Vote,
                };
                DbContext.InsertTransaction(trans);
            }

            return finalBlock.Serialize();
        }

        public string ReceiveBlock(string block)
        {
            CreateBlockModel blockModel = new CreateBlockModel();
            CreateBlockModel receivedBlock = blockModel.Deserialize(block);

            receivedBlock.Block = new CreateHashBlockModel
            {
                BlockIndex = receivedBlock.Block.BlockIndex,
                PreviousHash = receivedBlock.Block.PreviousHash,
                Transactions = receivedBlock.Block.Transactions,
                TimeStamp = receivedBlock.Block.TimeStamp,
            };

            CreateBlockStateRootHashModel rootHashModel = new CreateBlockStateRootHashModel
            {
                Block = receivedBlock.Block,
                Hash = Convert.ToBase64String(CryptoUtils.ComputeHashBlock(receivedBlock.Block)),
            };

            CreateBlockModel finalBlock = new CreateBlockModel
            {
                Block = receivedBlock.Block,
                StateRootHash = Convert.ToBase64String(CryptoUtils.ComputeStateRootHashBlock(rootHashModel))
            };

            var previousBlock = DbContext.PreviousBlock();

            if (previousBlock.Hash != receivedBlock.Block.PreviousHash &&
                finalBlock.StateRootHash != receivedBlock.StateRootHash && ///
                (previousBlock.BlockIndex++) != receivedBlock.Block.BlockIndex)
            {
                return "Invalid";
            }
            else
            {
                DbContext.InsertBlock(new Block
                {
                    BlockIndex = receivedBlock.Block.BlockIndex,
                    PreviousHash = receivedBlock.Block.PreviousHash,
                    TimeStamp = receivedBlock.Block.TimeStamp,
                    StateRootHash = finalBlock.StateRootHash,
                    Hash = rootHashModel.Hash
                });

                //insert transaction
            }
            return "Success";
        }

        public CreateBlockModel GetGenesisBlock()
        {
            var result = DbContext.GenesisBlock();
            if (result != null)
                return new CreateBlockModel
                {
                    Block = new CreateHashBlockModel
                    {
                        BlockIndex =result.BlockIndex,
                        PreviousHash = result.PreviousHash,
                        TimeStamp = result.TimeStamp,
                        Transactions = null
                    }
                };

            return null;
        }

    }
}
