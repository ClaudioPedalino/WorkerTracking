using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class DeleteTeamCommand : IRequest<string>
    {
        public Guid TeamId { get; set; }
    }
}
