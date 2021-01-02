using System;

namespace WorkerTracking.Entities
{
    public class Worker
    {
        public Worker(string firstName, string lastName, string email, DateTime birthday, string photoUrl, 
            int statusId, int roleId, DateTime lastModificationTime)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
            PhotoUrl = photoUrl;
            StatusId = statusId;
            RoleId = roleId;
            LastModificationTime = lastModificationTime;
            IsActive = true;
        }


        public Guid WorkerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime Birthday { get; private set; }
        public string PhotoUrl { get; private set; }
        public int StatusId { get; set; }
        public int RoleId { get; private set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsActive { get; private set; }

        public virtual Status Status { get; set; }
        public virtual Role Role { get; set; }


    }
}
