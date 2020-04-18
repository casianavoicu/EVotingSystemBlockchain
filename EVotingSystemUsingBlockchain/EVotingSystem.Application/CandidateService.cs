using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;

namespace EVotingSystem.Application
{
    public class CandidateService : ICandidateService
    {
        public TransactionOutputModel<CreateCandidateModel> AddCandidateTransaction(CreateCandidateModel candidateModel)
        {
            TransactionOutputModel<CreateCandidateModel> model = new TransactionOutputModel<CreateCandidateModel>
            {
                Address = "test",
                Transaction = new CreateCandidateModel
                {
                    Details = candidateModel.Details,
                    FirstName = candidateModel.FirstName,
                    LastName = candidateModel.LastName,
                },
                Signature = HashExtention.CalculateTransactionHash()
            };
            throw new System.NotImplementedException();
        }

    }
}
