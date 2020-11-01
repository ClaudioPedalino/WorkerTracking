using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Entities
{
    public class Team
    {
        public Team(string name)
        {
            Name = name;
            WorkersByTeamId = new List<WorkersByTeam>();
        }

        public Guid TeamId { get; private set; }
        public string Name { get; private set; }

        public virtual List<WorkersByTeam> WorkersByTeamId { get; set; }
    }
}
