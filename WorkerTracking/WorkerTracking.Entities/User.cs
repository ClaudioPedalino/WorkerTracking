using Microsoft.AspNetCore.Identity;

namespace WorkerTracking.Entities
{
    public class User : IdentityUser
    {
        public User()
        {

        }
        public User(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public bool IsAdmin { get; private set; }
    }
}
