using WorkerTracking.Core.Exceptions.Base;

namespace WorkerTracking.Core.Exceptions
{
    public class UserDoesNotExistException : CustomApplicationException
    {
        public UserDoesNotExistException()
            : base("User does not exist") { }
    }
}
