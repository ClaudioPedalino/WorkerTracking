using System;
using System.Collections.Generic;

namespace WorkerTracking.Entities
{
    public class Worker
    {
        public Guid WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string PhotoUrl { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsActive { get; set; }

        public virtual Status Status { get; set; }
        public virtual Role Role { get; set; }

        public virtual List<WorkersByTeam> WorkersByTeamId { get; set; }

    }
}
