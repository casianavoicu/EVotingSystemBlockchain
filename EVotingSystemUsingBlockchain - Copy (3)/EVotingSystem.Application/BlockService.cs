using EVotingSystem.Application.Model;
using EVotingSystem.Blockchain;
using KeyPairServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EVotingSystem.Application
{
    public class BlockService
    {
        public void GenesisBlock()
        {
            CreateBlockModelWithoutSignatureAndRootHash genesisBlock = new CreateBlockModelWithoutSignatureAndRootHash
            {
                BlockIndex = 0,
                TimeStamp = new DateTime(2020, 6, 1, 14, 0, 0),
                PreviousHash = null,
                Transactions = null,
            };

            var hash = CryptoService.CreateHash(genesisBlock.Serialize());

            DbContext.InsertBlock(new Block
            {
                BlockIndex = genesisBlock.BlockIndex,
                PreviousHash = genesisBlock.PreviousHash,
                TimeStamp = genesisBlock.TimeStamp,
                Hash = Convert.ToBase64String(hash),
                PublicKey = null,
                Signature = null,
            });

            var databaseState = DbContext.GetHash();
            var stateRootHash = CryptoService.CreateHash(databaseState);
            DbContext.UpdateStateRootHash(genesisBlock.BlockIndex, Convert.ToBase64String(stateRootHash));
        }

        public string CreateBlock(List<(TransactionModel, string)> transaction, (byte[], byte[]) keyPair)
        {
            var previousBlock = DbContext.PreviousBlock();
            var tempList = new List<TransactionModel>();

            for (int i = 0; i < transaction.Count; i++)
            {
                tempList.Add(transaction[i].Item1);
            }

            var model = new CreateBlockModelWithoutSignatureAndRootHash
            {
                BlockIndex = (++previousBlock.BlockIndex),
                PreviousHash = previousBlock.Hash,
                TimeStamp = DateTime.Now,
                Transactions = tempList,
                PublicKey = Convert.ToBase64String(keyPair.Item2)
            };

            var hash = CryptoService.CreateHash(model.Serialize());

            var signature = CryptoService.CreateSignature(hash, keyPair.Item1);

            DbContext.InsertBlock(new Block
            {
                BlockIndex = model.BlockIndex,
                Hash = Convert.ToBase64String(hash),
                PreviousHash = model.PreviousHash,
                PublicKey = model.PublicKey,
                Signature = signature,
                TimeStamp = model.TimeStamp,
            });

            var blockId = DbContext.BlockId(model.BlockIndex);
            var transService = new TransactionService();
            for (int i = 0; i < transaction.Count; i++)
            {
                if (transaction[i].Item1.Type == "Ballot")
                {
                    TransactionBallotModel trans = (TransactionBallotModel)transaction[i].Item1;

                    transService.InsertBallotTransactions((trans, transaction[i].Item2), blockId);
                }
            }
            var databaseState = DbContext.GetHash();
            var stateRootHash = CryptoService.CreateHash(databaseState);
            DbContext.UpdateStateRootHash(model.BlockIndex, Convert.ToBase64String(stateRootHash));
            return (new CreateBlockModel
            {
                BlockIndex = model.BlockIndex,
                PreviousHash = model.PreviousHash,
                PublicKey = model.PublicKey,
                Signature = signature,
                TimeStamp = model.TimeStamp,
                StateRootHash = Convert.ToString(stateRootHash),
                Transactions = tempList,
            }).Serialize();
        }

        //public string ReceiveBlock(string block)
        //{

        //}

        public void GetGenesisBlock()
        {
            var result = DbContext.GenesisBlock();
            if (result == null)
                GenesisBlock();
        }

    }
}
