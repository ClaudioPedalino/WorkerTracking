using System.ComponentModel.DataAnnotations;

namespace WorkerTracking.Core.Identity
{
    public class UserRegistrationCommand
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AdminKey { get; set; }
    }
}
