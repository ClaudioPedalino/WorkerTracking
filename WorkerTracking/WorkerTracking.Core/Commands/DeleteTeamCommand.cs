using MediatR;
using System;

namespace WorkerTracking.Core.Commands
{
    public class DeleteTeamCommand : IRequest<string>
    {
        public Guid TeamId { get; set; }
    }
}
