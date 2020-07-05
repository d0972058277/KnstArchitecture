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
    public class ExampleWithCreateDbSessionController : ControllerBase
    {
        public readonly IMySqlUnitOfWork UnitOfWork;

        public ExampleWithCreateDbSessionController(IMySqlUnitOfWork mySqlUnitOfWork)
        {
            UnitOfWork = mySqlUnitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Example>>> GetByRepo()
        {
            var session = UnitOfWork.CreateDbSession();
            var result = await UnitOfWork.Use<IExampleRepo>(session).GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Example>>> GetByRepo(int id)
        {
            var session = UnitOfWork.CreateDbSession();
            var result = await UnitOfWork.Use<IExampleRepo>(session).GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Example>> PostByRepoEFCore(Example example)
        {
            var session = UnitOfWork.CreateDbSession();
            // 內部程式碼 session.BeginTransaction();
            UnitOfWork.BeginTransaction(session);
            await UnitOfWork.Use<IExampleRepo>(session).PostAsync(example);
            // 內部程式碼 session.SaveChanges();
            UnitOfWork.SaveChanges(session);
            return Ok(example);
        }

        [HttpPost("Dapper")]
        public async Task<ActionResult<Example>> PostByRepoDapper(Example example)
        {
            var session = UnitOfWork.CreateDbSession();
            // 內部程式碼 session.BeginTransaction();
            UnitOfWork.BeginTransaction(session);
            var id = await UnitOfWork.Use<IExampleRepo>(session).PostByDapperAsync(example);
            var result = await UnitOfWork.Use<IExampleRepo>(session).GetAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.ExampleContextModels.Example example)
        {
            var session = UnitOfWork.CreateDbSession();
            if (id != example.Id) return BadRequest();
            var entity = await UnitOfWork.Use<IExampleRepo>(session).GetAsync(id);
            if (entity == null) return NotFound();

            // 內部程式碼 session.BeginTransaction();
            UnitOfWork.BeginTransaction(session);
            await UnitOfWork.Use<IExampleRepo>(session).PutAsync(example);
            // 內部程式碼 session.SaveChanges();
            UnitOfWork.SaveChanges(session);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.ExampleContextModels.Example>> Delete(int id)
        {
            var session = UnitOfWork.CreateDbSession();
            if ((await UnitOfWork.Use<IExampleRepo>(session).GetAsync(id)) == null) return NotFound();
            var entity = await UnitOfWork.Use<IExampleRepo>(session).GetAsync(id);
            if (entity == null) return NotFound();

            // 內部程式碼 session.BeginTransaction();
            UnitOfWork.BeginTransaction(session);
            await UnitOfWork.Use<IExampleRepo>(session).DeleteAsync(entity);
            // 內部程式碼 session.SaveChanges();
            UnitOfWork.SaveChanges(session);
            return NoContent();
        }
    }
}