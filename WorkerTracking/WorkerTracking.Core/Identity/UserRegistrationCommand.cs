using System.ComponentModel.DataAnnotations;

namespace WorkerTracking.Core.Identity
{
    public class UserRegistrationCommand
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        #region AdminKey
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? AdminKey { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        #endregion
    }
}
