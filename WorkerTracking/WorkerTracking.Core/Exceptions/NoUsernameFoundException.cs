using WorkerTracking.Core.Exceptions.Base;

namespace WorkerTracking.Core.Exceptions
{
    public class NoUsernameFoundException : CustomApplicationException
    {
        public NoUsernameFoundException()
            : base("No username register for that email") { }
    }
}
