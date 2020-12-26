using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Api.Auth;
using WorkerTracking.Api.Common;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Api.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(Routes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailureResponse
                {
                    ErrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage))
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailureResponse
                {
                    ErrorMessages = authResponse.ErrorMessages
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(Routes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailureResponse
                {
                    ErrorMessages = authResponse.ErrorMessages
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
    }
}
