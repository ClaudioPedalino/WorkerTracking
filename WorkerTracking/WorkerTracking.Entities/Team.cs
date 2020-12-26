using System;

namespace WorkerTracking.Entities
{
    public class Team
    {
        public Team(string name)
        {
            Name = name;
        }

        public Guid TeamId { get; private set; }
        public string Name { get; private set; }

    }
}
