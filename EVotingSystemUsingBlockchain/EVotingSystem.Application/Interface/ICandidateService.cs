using EVotingSystem.Application.Model;

namespace EVotingSystem.Application
{
    public interface ICandidateService
    {
        TransactionOutputModel<CreateCandidateModel> AddCandidateTransaction(CreateCandidateModel candidateModel);

    }
}
