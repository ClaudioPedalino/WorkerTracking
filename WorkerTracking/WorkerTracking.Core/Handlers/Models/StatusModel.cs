namespace WorkerTracking.Core.Handlers.Models
{
    public class StatusModel
    {
        public StatusModel(int statusId, string statusName)
        {
            StatusId = statusId;
            StatusName = statusName;
        }

        public StatusModel(int statusId, int totalWorkers)
        {
            StatusId = statusId;
            TotalWorkers = totalWorkers;
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int TotalWorkers { get; set; }
    }
}
