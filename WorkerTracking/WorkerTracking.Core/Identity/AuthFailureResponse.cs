using System.Collections.Generic;

namespace WorkerTracking.Core.Identity
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
