using EVotingSystem.Application.Model;

namespace EVotingSystem.Application
{
    public class BlockService
    {
        public BlockService()
        {

        }

        public CreateBlockModel GenesisBlock()
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

        public CreateBlockModel CreateBlock(CreateTransactionModel transaction)
        {
            //create blockModel
            //hash block
            //insert into db
            return null;
        }
    }
}
