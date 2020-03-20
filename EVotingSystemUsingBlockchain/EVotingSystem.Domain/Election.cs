using System;

namespace EVotingSystem.Domain
{
    public class Election
    {
        public int ElectionId { get; set; }

        public int TypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
