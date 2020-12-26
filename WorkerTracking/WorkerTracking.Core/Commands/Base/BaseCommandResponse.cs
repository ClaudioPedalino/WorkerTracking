namespace WorkerTracking.Core.Commands.Base
{
    public class BaseCommandResponse
    {
        public BaseCommandResponse(string commandResponse)
        {
            CommandResponse = commandResponse;
        }

        public string CommandResponse { get; set; }
    }
}
