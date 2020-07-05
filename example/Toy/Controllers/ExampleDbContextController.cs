using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toy.Models.ExampleContextModels;
using Toy.Models.UserContextModels;
using Toy.Repos.Example;

namespace Toy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleDbContextController : ControllerBase
    {
        public readonly ExampleContext ExampleContext;
        public readonly UserContext UserContext;
        public readonly IMySqlUnitOfWork UnitOfWork;

        public ExampleDbContextController(ExampleContext exampleContext, UserContext userContext, IMySqlUnitOfWork unitOfWork)
        {
            ExampleContext = exampleContext;
            UserContext = userContext;
            UnitOfWork = unitOfWork;
        }

        [HttpGet("CheckMultiContext")]
        public void CheckMultiContext()
        {
            UnitOfWork.BeginTransaction();
            var example = ExampleContext.Example.Add(new Example
            {
                Name = "CheckMultiContext",
                    RowDatetime = DateTime.Now
            });
            var user = UserContext.User.Add(new User
            {
                Name = "CheckMultiContext",
                    Password = "CheckMultiContext",
                    RowDatetime = DateTime.Now
            });
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
        }

        [HttpGet("CheckSameContext")]
        public bool CheckIsSameContext()
        {
            var result = ExampleContext == UnitOfWork.GetCtx<ExampleContext>();
            return result;
        }

        [HttpGet("CheckDifferentContext")]
        public bool CheckDifferentContext()
        {
            var result = ExampleContext != UnitOfWork.CreateDbSession().GetCtx<ExampleContext>();
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Example>>> GetByRepo()
        {
            var result = await ExampleContext.Example.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Example>>> GetByRepo(int id)
        {
            var result = await ExampleContext.Example.FindAsync(id);
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
            await ExampleContext.Example.AddAsync(example);
            UnitOfWork.SaveChanges();
            return Ok(example);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.ExampleContextModels.Example example)
        {
            if (id != example.Id) return BadRequest();
            var entity = await ExampleContext.Example.FindAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            ExampleContext.Example.Update(example);
            UnitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.ExampleContextModels.Example>> Delete(int id)
        {
            if ((await ExampleContext.Example.FindAsync(id)) == null) return NotFound();
            var entity = await ExampleContext.Example.FindAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            ExampleContext.Example.Remove(entity);
            UnitOfWork.SaveChanges();
            return NoContent();
        }
    }
}