using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkerTracking.Core.Identity
{
    public class UserRegistrationCommand
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
