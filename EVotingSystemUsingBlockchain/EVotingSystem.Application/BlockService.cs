using EVotingSystem.Application.Model;

namespace EVotingSystem.Application
{
    public class BlockService
    {
        public BlockService()
        {

        }

        public BlockModel<CreateTransactionVoteModel> GenesisBlock()
        {
            //BlockModel<CreateTransactionVoteModel> genesisBlock = new BlockModel<CreateTransactionVoteModel>()
            //{
            //    BlockHeader = new CreateBlockModel
            //    {
            //        BlockIndex = 0,
            //        TimeStamp = DateTime.Now,
            //        PreviousHash = null,
            //        //Hash = HashExtention.ComputeBlockHash256()
            //    }
            //};
            return null;
        }

        public BlockModel<CreateTransactionVoteModel> CreateBlock(CreateTransactionVoteModel transaction)
        {
            return null;
        }
    }
}
