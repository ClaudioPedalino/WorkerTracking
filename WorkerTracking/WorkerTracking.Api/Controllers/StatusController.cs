using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WorkerTracking.Api.Common;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;


namespace WorkerTracking.Api.Controllers
{
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

        [HttpGet(Routes.Get_All_Status)]
        public async Task<ActionResult<StatusModel>> GetAllStatusAsync()
        {
            try
            {
                var response = await _mediator.Send(new GetAllStatusQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Status} with message: {ex.Message}");
                return null;
            }
        }

        //[HttpGet(Routes.Get_Status_By_Id)]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost(Routes.Create_Status)]
        public async Task<ActionResult> CreateStatusAysnc([FromBody] CreateStatusCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Status} with message: {ex.Message}");
                return null;
            }
        }

        [HttpDelete(Routes.Delete_Status)]
        public async Task<ActionResult> DeleteStatusAysnc([FromBody] DeleteStatusCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Status} with message: {ex.Message}");
                return null;
            }
        }
    }
}
