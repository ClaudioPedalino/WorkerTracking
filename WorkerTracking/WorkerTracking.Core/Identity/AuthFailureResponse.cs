using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Identity
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
