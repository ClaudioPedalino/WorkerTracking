namespace WorkerTracking.Core.Identity
{
    public class LoggedRequest
    {
        private string _userId;

        public string GetUser() => _userId;
        public void SetUser(string userId) => _userId = userId;
    }
}
