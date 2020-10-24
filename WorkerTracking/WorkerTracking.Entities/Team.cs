using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Entities
{
    public class Team
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }


        public WorkersByTeam WorkersByTeamId { get; set; }
    }
}
