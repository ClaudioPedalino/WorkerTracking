using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WorkerTracking.Api.Common;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkerTracking.Api.Controllers
{
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

        [EnableCors("AllowOrigin")]
        [HttpGet(Routes.Get_All_Teams)]
        public async Task<ActionResult<TeamModel>> GetAllTeamAsync()
        {
            try
            {
                var response = await _mediator.Send(new GetAllTeamQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Teams} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPost(Routes.Create_Team)]
        public async Task<ActionResult> CreateTeamAysnc([FromBody] CreateTeamCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Team} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpDelete(Routes.Delete_Team)]
        public async Task<ActionResult> DeleteTeamAysnc([FromBody] DeleteTeamCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
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
