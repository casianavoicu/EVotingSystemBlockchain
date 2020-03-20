using EVotingSystem.Application.Model;

namespace EVotingSystem.Application
{
    public interface IBlockService
    {
        byte[] CalculateHash(CreateToken createToken);

        //BlockModel MineBlock()
    }
}
