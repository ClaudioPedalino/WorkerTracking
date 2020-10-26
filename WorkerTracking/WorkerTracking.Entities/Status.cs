namespace WorkerTracking.Entities
{
    public class Status
    {
        public Status()
        {
        }

        public Status(int statusId, string name)
        {
            StatusId = statusId;
            Name = name;
        }

        public int StatusId { get; set; }
        public string Name { get; set; }
    }
}