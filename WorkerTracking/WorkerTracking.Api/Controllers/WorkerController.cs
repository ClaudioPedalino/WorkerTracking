using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WorkerTracking.Api.Common;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkerTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public WorkerController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Routes.GetAll)]
        public async Task<ActionResult<WorkerModel>> GetAllWorkersAsync([FromQuery] GetAllWorkerersQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return Ok(new PagedResponse<WorkerModel>(
                    data: response.Item1,
                    pageNumber: request.PageNumber,
                    totalResults: response.Item2,
                    pageSize: request.PageSize));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.GetAll} with message: {ex.Message}");
                return null;
            }

        }

        [HttpGet(Routes.GetById)]
        public async Task<WorkerModel> GetWorkerByIdAsync([FromQuery] GetWorkerByIdQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.GetById} with message: {ex.Message}");
                return null;
            }
        }

        [HttpPatch(Routes.UpdateStatus)]
        public async Task<string> UpdateWorkerStatusAsync([FromBody] UpdateWorkerStatusCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.UpdateStatus} with message: {ex.Message}");
                return null;
            }
        }


        // POST api/<WorkerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<WorkerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<WorkerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
