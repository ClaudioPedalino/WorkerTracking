using System.ComponentModel.DataAnnotations;

namespace WorkerTracking.Core.Identity
{
    public class UserLoginCommand
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
