using EVotingSystem.Application.Model;

namespace EVotingSystem.Application
{
    public interface ICandidateService
    {
        CreateTransactionInputModel<CreateCandidateModel> AddCandidateTransaction(CreateCandidateModel candidateModel);

    }
}
