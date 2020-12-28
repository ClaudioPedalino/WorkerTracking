using System;
using System.Collections.Generic;

namespace WorkerTracking.Core.Handlers.Models
{
    public class WorkerModel
    {
        public Guid WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string PhotoUrl { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsBirthdayToday { get; set; }
        public List<TeamModel> Teams { get; set; }

    }
}
