using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using WorkerTracking.Api.Auth;
using WorkerTracking.Api.Common;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;

namespace WorkerTracking.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize]
        //[AllowAnonymous]
        //[EnableCors("WorkerApiCors")]
        [HttpGet(Routes.Get_All_Workers)]
        public async Task<ActionResult<WorkerModel>> GetAllWorkersAsync([FromQuery] GetAllWorkersQuery request)
        {
            try
            {
                request.SetUser(User.GetUserId());
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
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet(Routes.Get_Worker_My_Info)]
        public async Task<ActionResult<WorkerModel>> GetWorkerByIdAsync([FromQuery] GetWorkerMyInfo request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Get_All_Workers} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch(Routes.Update_Worker_Status)]
        public async Task<ActionResult> UpdateWorkerStatusAsync([FromBody] UpdateWorkerStatusCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Update_Worker_Status} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Routes.Create_Worker)]
        public async Task<ActionResult> CreateWorkerAysnc([FromBody] CreateWorkerCommand request)
        {
            try
            {
                request.SetUser(User.GetUserId());
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Operation failed into controller {Routes.Create_Worker} with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
