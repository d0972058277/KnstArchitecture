using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toy.Models.ExampleContextModels;
using Toy.Queries.Example;

namespace Toy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleSelectWithQueryController : ControllerBase
    {
        public readonly IExampleQuery ExampleQuery;

        public ExampleSelectWithQueryController(IExampleQuery exampleQuery)
        {
            ExampleQuery = exampleQuery;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Example>>> ByQuery()
        {
            var result = await ExampleQuery.GetAsync();
            return Ok(result);
        }
    }
}