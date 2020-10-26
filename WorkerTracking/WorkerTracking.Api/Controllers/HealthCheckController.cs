using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkerTracking.Api.Common;

namespace WorkerTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet(Routes.Live)]
        public async Task<ActionResult> GetLiveAsync()
            => await CallHealthCheck("live");

        [HttpGet(Routes.Ready)]
        public async Task<ActionResult> GetReadyAsync()
            => await CallHealthCheck("ready");

        [HttpGet(Routes.UI)]
        public async Task<ActionResult> GetUIDataAsync()
            => await CallHealthCheck("ui");


        private async Task<ActionResult> CallHealthCheck(string param)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://localhost:44351/api/health/{param}";

                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return Ok(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
