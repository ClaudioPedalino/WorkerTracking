using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class DeleteStatusCommand : IRequest<string>
    {
        public int StatusId { get; set; }
    }
}
