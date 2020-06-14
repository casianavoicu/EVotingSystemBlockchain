using EVotingSystem.Application.Model;
using EVotingSystem.Blockchain;
using Models;
using System;
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
                AccountAddress = toAddress,
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
