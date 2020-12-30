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
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;


namespace WorkerTracking.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public TeamController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Routes.Get_All_Teams)]
        public async Task<ActionResult<TeamModel>> GetAllTeamAsync([FromQuery] GetAllTeamQuery request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Teams} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Routes.Create_Team)]
        public async Task<ActionResult> CreateTeamAysnc([FromBody] CreateTeamCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Team} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete(Routes.Delete_Team)]
        public async Task<ActionResult> DeleteTeamAysnc([FromQuery] DeleteTeamCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Team} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
