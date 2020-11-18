using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class CreateLocationCommand : IRequest<string>
    {
        public string LocationName { get; set; }
    }
}
