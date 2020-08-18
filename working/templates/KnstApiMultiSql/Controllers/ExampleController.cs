using System.Collections.Generic;
using System.Threading.Tasks;
using KnstApiMultiSql.Models.Test;
using KnstApiMultiSql.Repos.Example;
using KnstApiMultiSql.Services.Example;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace KnstApiMultiSql.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        // Create, Read, Update, Delete
        IMultiSqlUnitOfWork UnitOfWork;
        IExampleService ExampleService;

        public ExampleController(IMultiSqlUnitOfWork unitOfWork, IExampleService exampleService)
        {
            UnitOfWork = unitOfWork;
            ExampleService = exampleService;
        }

        [HttpGet("MultiRepoDefault")]
        public Task<IEnumerable<Models.Test.Example>> MultiRepoDefaultGetAsync()
        {
            return ExampleService.MultiRepoDefaultGetAsync();
        }

        [HttpGet("RepoDefault")]
        public Task<IEnumerable<Models.Test.Example>> RepoDefaultGetAsync()
        {
            return ExampleService.RepoDefaultGetAsync();
        }

        [HttpGet("First")]
        public Task<IEnumerable<Models.Test.Example>> FirstGetAsync()
        {
            return ExampleService.FirstGetAsync();
        }

        [HttpGet("Second")]
        public Task<IEnumerable<Models.Test.Example>> SecondGetAsync()
        {
            return ExampleService.SecondGetAsync();
        }

        [HttpGet("Third")]
        public Task<IEnumerable<Models.Test.Example>> ThirdGetAsync()
        {
            return ExampleService.ThirdGetAsync();
        }
    }
}