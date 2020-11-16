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

        [HttpGet(Routes.Get_All_Workers)]
        public async Task<ActionResult<WorkerModel>> GetAllWorkersAsync([FromQuery] GetAllWorkersQuery request)
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
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Workers} with message: {ex.Message}");
                return null;
            }

        }

        [HttpGet(Routes.Get_Worker_By_Id)]
        public async Task<WorkerModel> GetWorkerByIdAsync([FromQuery] GetWorkerByIdQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Workers} with message: {ex.Message}");
                return null;
            }
        }

        [HttpPatch(Routes.Update_Worker_Status)]
        public async Task<string> UpdateWorkerStatusAsync([FromBody] UpdateWorkerStatusCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Update_Worker_Status} with message: {ex.Message}");
                return null;
            }
        }

        [HttpPost(Routes.Create_Worker)]
        public async Task<ActionResult> CreateWorkerAysnc([FromBody] CreateWorkerCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Worker} with message: {ex.Message}");
                return null;
            }
        }

    }
}
