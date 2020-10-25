using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkerTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<WorkerController>
        [HttpGet]
        public async Task<ActionResult<WorkerModel>> GetAllWorkersAsync([FromQuery] GetAllWorkerersQuery request)
        {
            IEnumerable<WorkerModel> response = await _mediator.Send(request);

            return Ok(new PagedResponse<WorkerModel>(
                data: response, 
                pageNumber: request.PageNumber,
                pageSize: request.PageSize));
        }

        // Patch api/<WorkerController>/5
        [HttpPatch("update-status")]
        public async Task<string> UpdateWorkerStatusAsync([FromBody] UpdateWorkerStatusCommand command)
        {
            var response = await _mediator.Send(command);

            return response;
        }


        // GET api/<WorkerController>/5
        //[HttpGet("{id}")]
        //public Worker Get(GetWorkererByIdQuery request)
        //{

        //    return null;
        //}

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
