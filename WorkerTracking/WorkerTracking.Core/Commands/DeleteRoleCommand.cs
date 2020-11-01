using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class DeleteRoleCommand : IRequest<string>
    {
        public int RoleId { get; set; }
    }
}
