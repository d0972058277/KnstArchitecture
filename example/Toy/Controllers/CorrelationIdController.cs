using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Toy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorrelationIdController : ControllerBase
    {
        public IHttpClientFactory HttpClientFactory;
        public ICorrelationContextAccessor CorrelationContextAccessor;
        public ILogger<CorrelationIdController> Logger;

        public CorrelationIdController(IHttpClientFactory httpClientFactory, ICorrelationContextAccessor correlationContextAccessor, ILogger<CorrelationIdController> logger)
        {
            HttpClientFactory = httpClientFactory;
            CorrelationContextAccessor = correlationContextAccessor;
            Logger = logger;
        }

        [HttpGet("Nest2")]
        public ActionResult<string> GetCorrelationIdNest2()
        {
            Logger.LogInformation("{CorrelationId}", CorrelationContextAccessor.CorrelationContext.CorrelationId);
            var result = $"Nest2 CorrelationId {CorrelationContextAccessor.CorrelationContext.CorrelationId}";
            return Ok(result);
        }

        [HttpGet("Nest1")]
        public async Task<ActionResult<string>> GetCorrelationIdNest1()
        {
            Logger.LogInformation("{CorrelationId}", CorrelationContextAccessor.CorrelationContext.CorrelationId);
            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5000/api/CorrelationId/Nest2");
            var nest2 = await response.Content.ReadAsStringAsync();
            var result = $"Nest1 CorrelationId {CorrelationContextAccessor.CorrelationContext.CorrelationId}\n{nest2}";
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetCorrelationIdNest()
        {
            Logger.LogInformation("{CorrelationId}", CorrelationContextAccessor.CorrelationContext.CorrelationId);
            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5000/api/CorrelationId/Nest1");
            var nest1 = await response.Content.ReadAsStringAsync();
            var result = $"CorrelationId {CorrelationContextAccessor.CorrelationContext.CorrelationId}\n{nest1}";
            return Ok(result);
        }
    }
}