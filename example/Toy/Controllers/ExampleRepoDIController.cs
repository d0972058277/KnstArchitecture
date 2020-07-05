using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Toy.Models.ExampleContextModels;
using Toy.Repos.Example;

namespace Toy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleRepoDIController : ControllerBase
    {
        public readonly IExampleRepo ExampleRepo;
        public readonly IMySqlUnitOfWork UnitOfWork;

        public ExampleRepoDIController(IExampleRepo exampleRepo, IMySqlUnitOfWork unitOfWork)
        {
            ExampleRepo = exampleRepo;
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Example>>> GetByRepo()
        {
            var result = await ExampleRepo.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Example>>> GetByRepo(int id)
        {
            var result = await ExampleRepo.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Example>> PostByRepoEFCore(Example example)
        {
            UnitOfWork.BeginTransaction();
            await ExampleRepo.PostAsync(example);
            UnitOfWork.SaveChanges();
            return Ok(example);
        }

        [HttpPost("Dapper")]
        public async Task<ActionResult<Example>> PostByRepoDapper(Example example)
        {
            UnitOfWork.BeginTransaction();
            var id = await ExampleRepo.PostByDapperAsync(example);
            var result = await ExampleRepo.GetAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.ExampleContextModels.Example example)
        {
            if (id != example.Id) return BadRequest();
            var entity = await ExampleRepo.GetAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            await ExampleRepo.PutAsync(example);
            UnitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.ExampleContextModels.Example>> Delete(int id)
        {
            if ((await ExampleRepo.GetAsync(id)) == null) return NotFound();
            var entity = await ExampleRepo.GetAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            await ExampleRepo.DeleteAsync(entity);
            UnitOfWork.SaveChanges();
            return NoContent();
        }
    }
}