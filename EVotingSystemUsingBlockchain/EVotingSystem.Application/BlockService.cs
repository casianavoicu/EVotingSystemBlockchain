using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;
using EVotingSystem.Blockchain;
using KeyPairServices;
using System;
using System.Collections.Generic;

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
                else
                {
                    TransactionVoteModel trans = (TransactionVoteModel)transaction[i].Item1;
                    transService.InsertVoteTransactions((trans, transaction[i].Item2), blockId);
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

        public string ReceiveBlock(CreateBlockModel block, List<(TransactionModel, string)> verifiedTransactions)
        {
            var previousBlock = DbContext.PreviousBlock();
            var tempList = new List<TransactionModel>();

            for (int i = 0; i < block.Transactions.Count; i++)
            {
                tempList.Add(block.Transactions[i]);
            }

            var model = new CreateBlockModelWithoutSignatureAndRootHash
            {
                BlockIndex = (++previousBlock.BlockIndex),
                PreviousHash = previousBlock.Hash,
                TimeStamp = DateTime.Now,
                Transactions = tempList,
                PublicKey = block.PublicKey
            };

            var signature = CryptoUtils.ValidateSignature(block.PublicKey, model.Serialize(), block.Signature, out string hash);
            var hashedData = hash;
            if ((previousBlock.Hash == block.PreviousHash)
                || previousBlock.BlockIndex == model.BlockIndex
                || !signature)
            {
                return null;
            }
            else
            {
                DbContext.InsertBlock(new Block
                {
                    BlockIndex = model.BlockIndex,
                    Hash = hashedData,
                    PreviousHash = model.PreviousHash,
                    PublicKey = model.PublicKey,
                    Signature = block.Signature,
                    TimeStamp = model.TimeStamp,
                });

                var blockId = DbContext.BlockId(model.BlockIndex);
                var transService = new TransactionService();
                for (int i = 0; i < verifiedTransactions.Count; i++)
                {
                    if (verifiedTransactions[i].Item1.Type == "Ballot")
                    {
                        TransactionBallotModel trans = (TransactionBallotModel)verifiedTransactions[i].Item1;

                        transService.InsertBallotTransactions((trans, verifiedTransactions[i].Item2), blockId);
                    }
                    else
                    {
                        TransactionVoteModel trans = (TransactionVoteModel)verifiedTransactions[i].Item1;
                        transService.InsertVoteTransactions((trans, verifiedTransactions[i].Item2), blockId);
                    }
                }
                var databaseState = DbContext.GetHash();
                var stateRootHash = Convert.ToBase64String(CryptoService.CreateHash(databaseState));
                if (stateRootHash != block.StateRootHash)
                {
                    for (int i = 0; i < verifiedTransactions.Count; i++)
                    {
                        DbContext.DeleteTransactions(verifiedTransactions[i].Item2);
                    }
                    DbContext.DeleteBlock(hashedData);
                    return null;
                }
                else
                {
                    DbContext.UpdateStateRootHash(model.BlockIndex, stateRootHash);
                }
                return "Success";
            }
        }

        public void GetGenesisBlock()
        {
            var result = DbContext.GenesisBlock();
            if (result == null)
                GenesisBlock();
        }

    }
}
