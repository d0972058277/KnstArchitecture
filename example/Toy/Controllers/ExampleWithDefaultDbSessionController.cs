using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Toy.Repos.Example;

namespace Toy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleWithDefaultDbSessionController : ControllerBase
    {
        public readonly IMySqlUnitOfWork UnitOfWork;

        public ExampleWithDefaultDbSessionController(IMySqlUnitOfWork mySqlUnitOfWork)
        {
            UnitOfWork = mySqlUnitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ExampleContextModels.Example>>> GetByRepo()
        {
            var result = await UnitOfWork.Use<IExampleRepo>().GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Models.ExampleContextModels.Example>>> GetByRepo(int id)
        {
            var result = await UnitOfWork.Use<IExampleRepo>().GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Models.ExampleContextModels.Example>> PostByRepoEFCore(Models.ExampleContextModels.Example example)
        {
            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IExampleRepo>().PostAsync(example);
            UnitOfWork.SaveChanges();
            return Ok(example);
        }

        [HttpPost("Dapper")]
        public async Task<ActionResult<Models.ExampleContextModels.Example>> PostByRepoDapper(Models.ExampleContextModels.Example example)
        {
            UnitOfWork.BeginTransaction();
            var id = await UnitOfWork.Use<IExampleRepo>().PostByDapperAsync(example);
            var result = await UnitOfWork.Use<IExampleRepo>().GetAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.ExampleContextModels.Example example)
        {
            if (id != example.Id) return BadRequest();
            var entity = await UnitOfWork.Use<IExampleRepo>().GetAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IExampleRepo>().PutAsync(example);
            UnitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.ExampleContextModels.Example>> Delete(int id)
        {
            if ((await UnitOfWork.Use<IExampleRepo>().GetAsync(id)) == null) return NotFound();
            var entity = await UnitOfWork.Use<IExampleRepo>().GetAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IExampleRepo>().DeleteAsync(entity);
            UnitOfWork.SaveChanges();
            return NoContent();
        }
    }
}