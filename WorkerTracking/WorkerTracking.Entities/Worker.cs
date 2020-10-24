﻿using System;

namespace WorkerTracking.Entities
{
    public class Worker
    {
        public Guid WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }

        public virtual Status Status { get; set; }
        public virtual Role Role { get; set; }
        public WorkersByTeam WorkersByTeamId { get; set; }

    }
}