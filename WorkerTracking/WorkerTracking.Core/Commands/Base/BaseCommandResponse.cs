namespace WorkerTracking.Core.Commands.Base
{
    public class BaseCommandResponse
    {
        public BaseCommandResponse(InfoMessage infoMessage)
        {
            InfoMessage = infoMessage;
        }

        public BaseCommandResponse(string commandResponse)
        {
            CommandResponse = commandResponse;
        }

        public string CommandResponse { get; set; }
        public InfoMessage InfoMessage { get; set; }
    }

    public class InfoMessage
    {
        public InfoMessage(string message, bool success = false)
        {
            Message = message;
            Success = success;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
