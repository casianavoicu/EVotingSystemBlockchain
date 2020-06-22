using EVotingSystem.Blockchain;
using System.Collections.Generic;

namespace EVotingSystem.Application
{
    public class BallotService
    {
        public void AddCandidates(List<string> candidates, string name, string toAddress)
        {
            var account = new Account()
            {
                PublicKey = toAddress,
                Balance = 0
            };

            DbContext.InsertAccount(account);

            var insertedAccount = DbContext.GetAccount(toAddress);

            foreach (var candidateName in candidates)
            {
                var candidate = new Candidate()
                {
                    AccountId = insertedAccount.AccountId,
                    Ballot = name,
                    Name = candidateName
                };

                DbContext.InsertCandidate(candidate);
            }
        }
    }
}
