using System;

namespace WorkerTracking.Core.Handlers.Models
{
    public class TeamModel
    {

        public TeamModel(Guid teamId, string name)
        {
            TeamId = teamId;
            Name = name;
        }

        public TeamModel(Guid teamId, int totalWorkers)
        {
            TeamId = teamId;
            TotalWorkers = totalWorkers;
        }

        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public int TotalWorkers { get; set; }
    }
}