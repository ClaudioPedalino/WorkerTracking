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
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public RoleController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet(Routes.Get_All_Roles)]
        public async Task<ActionResult<RoleModel>> GetAllRoleAsync()
        {
            try
            {
                var response = await _mediator.Send(new GetAllRolesQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Roles} with message: {ex.Message}");
                return null;
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPost(Routes.Create_Role)]
        public async Task<ActionResult> CreateRoleAysnc([FromBody] CreateRoleCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Role} with message: {ex.Message}");
                return null;
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpDelete(Routes.Delete_Role)]
        public async Task<ActionResult> DeleteRoleAysnc([FromBody] DeleteRoleCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Role} with message: {ex.Message}");
                return null;
            }
        }

    }
}
