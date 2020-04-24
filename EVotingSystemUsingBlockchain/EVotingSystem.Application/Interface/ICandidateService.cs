using EVotingSystem.Application.Model;

namespace EVotingSystem.Application.Interface
{
    public interface ICandidateService
    {
        CreateTransactionInputModel<CreateCandidateModel> AddCandidateTransaction(CreateCandidateModel candidateModel);
    }
}
