using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class CreateTeamCommand : IRequest<string>
    {
        public string TeamName { get; set; }
    }
}
