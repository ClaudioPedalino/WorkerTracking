using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WorkerTracking.Api.Auth;
using WorkerTracking.Api.Common;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkerTracking.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public RoleController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Routes.Get_All_Roles)]
        public async Task<ActionResult<RoleModel>> GetAllRoleAsync([FromQuery] GetAllRolesQuery request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Roles} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Routes.Create_Role)]
        public async Task<ActionResult<BaseCommandResponse>> CreateRoleAysnc([FromBody] CreateRoleCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Role} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete(Routes.Delete_Role)]
        public async Task<ActionResult> DeleteRoleAysnc([FromQuery] DeleteRoleCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Role} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
