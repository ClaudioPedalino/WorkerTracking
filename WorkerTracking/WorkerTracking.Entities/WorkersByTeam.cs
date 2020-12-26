using System;

namespace WorkerTracking.Entities
{
    public class WorkersByTeam
    {
        public WorkersByTeam(Guid workerId, Guid teamId)
        {
            WorkerId = workerId;
            TeamId = teamId;
        }

        public Guid WorkersByTeamId { get; private set; }
        public Guid WorkerId { get; private set; }
        public Guid TeamId { get; private set; }

        public virtual Team Team { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
