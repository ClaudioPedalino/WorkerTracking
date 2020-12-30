using System.Linq;
using System.Security.Claims;

namespace WorkerTracking.Api.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.FirstOrDefault(i => i.Type == "id").Value;

    }
}
