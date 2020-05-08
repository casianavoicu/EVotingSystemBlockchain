using System;

namespace EVotingSystem.Application.Model
{
    public class AccountModel
    {
        public string AccountAddress { get; set; }

        public string AccountType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
