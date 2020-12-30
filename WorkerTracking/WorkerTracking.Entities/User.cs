using Microsoft.AspNetCore.Identity;

namespace WorkerTracking.Entities
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
