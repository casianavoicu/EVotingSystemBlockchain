using EVotingSystem.Application.Model;

namespace EVotingSystem.Application
{
    public class BlockService : IBlockService
    {
        public BlockService()
        {

        }

        public BlockModel GenesisBlock()
        {
            BlockModel genesisBlock = new BlockModel()
            {

            };
            return genesisBlock;
        }

    }
}
