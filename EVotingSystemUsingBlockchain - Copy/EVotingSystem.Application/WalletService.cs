using EVotingSystem.Application.Interface;

namespace EVotingSystem.Application
{
    public class WalletService : IWalletService
    {
        private readonly IDigitalSignatureService digitalSignatureService;

        public WalletService(IDigitalSignatureService digitalSignatureService)
        {
            this.digitalSignatureService = digitalSignatureService;
        }

        //create wallet
    }
}
