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
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public StatusController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Routes.Get_All_Status)]
        public async Task<ActionResult<StatusModel>> GetAllStatusAsync([FromQuery] GetAllStatusQuery request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Status} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Routes.Create_Status)]
        public async Task<ActionResult> CreateStatusAysnc([FromBody] CreateStatusCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Status} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete(Routes.Delete_Status)]
        public async Task<ActionResult> DeleteStatusAysnc([FromQuery] DeleteStatusCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Status} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
