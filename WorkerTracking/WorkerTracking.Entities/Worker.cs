using System;

namespace WorkerTracking.Entities
{
    public class Worker
    {
        public Worker(string firstName, string lastName, string email, DateTime birthday, string photoUrl, int statusId, int roleId, DateTime lastModificationTime)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
            PhotoUrl = photoUrl;
            SetStatusId(statusId);
            RoleId = roleId;
            SetLastModificationTime(lastModificationTime);
        }


        public Guid WorkerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime Birthday { get; private set; }
        public string PhotoUrl { get; private set; }


        private int statusId;
        public int GetStatusId() => statusId;
        public void SetStatusId(int value) => statusId = value;

        public int RoleId { get; private set; }

        private DateTime lastModificationTime;
        public DateTime GetLastModificationTime() => lastModificationTime;
        public void SetLastModificationTime(DateTime value) => lastModificationTime = value;

        public bool IsActive { get; private set; }

        public virtual Status Status { get; private set; }
        public virtual Role Role { get; private set; }


    }
}
