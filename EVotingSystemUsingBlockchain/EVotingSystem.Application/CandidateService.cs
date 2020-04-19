using EVotingSystem.Application.Model;
using EVotingSystem.Application.Utils;

namespace EVotingSystem.Application
{
    public class CandidateService : ICandidateService
    {
        public CreateTransactionInputModel<CreateCandidateModel> AddCandidateTransaction(CreateCandidateModel candidateModel)
        {
            CreateTransactionInputModel<CreateCandidateModel> model = new CreateTransactionInputModel<CreateCandidateModel>
            {
                Address = "test", //probably RSA
                Transaction = new CreateCandidateModel
                {
                    Details = candidateModel.Details,
                    FirstName = candidateModel.FirstName,
                    LastName = candidateModel.LastName,
                },
                Signature = "test"
            };
            throw new System.NotImplementedException();
        }

    }
}
