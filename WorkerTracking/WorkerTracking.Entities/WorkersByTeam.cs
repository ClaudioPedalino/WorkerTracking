using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Entities
{
    public class WorkersByTeam
    {
        public Guid WorkersByTeamId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid TeamId { get; set; }

        public Team Team { get; set; }
        public Worker Worker { get; set; }
    }
}
