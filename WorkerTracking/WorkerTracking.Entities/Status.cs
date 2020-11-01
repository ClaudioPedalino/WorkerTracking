namespace WorkerTracking.Entities
{
    public class Status
    {
        public Status(string name)
        {
            Name = name;
        }

        public Status(int statusId, string name)
        {
            StatusId = statusId;
            Name = name;
        }

        public int StatusId { get; private set; }
        public string Name { get; private set; }
    }
}