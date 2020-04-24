using System;

namespace EVotingSystem.Application.Model
{
    public class CreateElectionModel
    {
        public string ElectionName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Started { get; set; }

    }
}
