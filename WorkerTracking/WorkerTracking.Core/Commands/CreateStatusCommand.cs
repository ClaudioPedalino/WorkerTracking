using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class CreateStatusCommand : IRequest<string>
    {
        public string StatusName { get; set; }
    }
}
