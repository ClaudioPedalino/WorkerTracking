using System.Threading.Tasks;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Api.Auth
{
    public interface IIdentityService
    {
        Task<bool> VerifyExistingEmailAsync(string email);
        Task<AuthenticationResult> RegisterAsync(UserRegistrationCommand request);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
