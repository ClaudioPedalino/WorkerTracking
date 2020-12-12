using MediatR;

namespace WorkerTracking.Core.Commands
{
    public class CreateTeamCommand : IRequest<string>
    {
        public string TeamName { get; set; }
    }
}
