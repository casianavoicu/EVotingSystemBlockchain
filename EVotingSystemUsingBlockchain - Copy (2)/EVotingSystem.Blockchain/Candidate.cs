using SQLite;
using System;

namespace EVotingSystem.Blockchain
{
    public class Candidate
    {
        [PrimaryKey, AutoIncrement]
        public int CandidateId { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Ballot { get; set; }

        [NotNull]
        public int AccountId { get; set; }

        public int Votes { get; set; }
    }
}
