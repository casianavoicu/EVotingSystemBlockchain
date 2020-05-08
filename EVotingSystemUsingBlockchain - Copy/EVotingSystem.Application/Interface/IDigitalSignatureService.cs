namespace EVotingSystem.Application.Interface
{
    public interface IDigitalSignatureService
    {
        void AssignKey();

        byte[] SignData(byte[] hashOfDataToSign);

        bool VerifySiganture(byte[] hashOfData, byte[] Signature);
    }
}
