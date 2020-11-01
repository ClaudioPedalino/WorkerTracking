using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WorkerTracking.Api.Common;
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

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
