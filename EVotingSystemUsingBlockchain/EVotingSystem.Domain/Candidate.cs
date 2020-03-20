using System;
using System.Collections.Generic;
using System.Text;

namespace EVotingSystem.Domain
{
    public class Candidate
    {
        public int CandidateId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentityCard { get; set; }

        public int ElectionId { get; set; }

        public Election Election { get; set; }

        public int? PartyId { get; set; }

        public Party Party { get; set; }

    }
}
