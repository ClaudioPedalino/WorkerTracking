using MediatR;
using System;

namespace WorkerTracking.Core.Commands
{
    public class CreateWorkerCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public Guid[] TeamId { get; set; }

    }
}
