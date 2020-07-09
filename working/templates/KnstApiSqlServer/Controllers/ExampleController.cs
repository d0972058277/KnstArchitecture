using System.Collections.Generic;
using System.Threading.Tasks;
using KnstApiSqlServer.Models.Test;
using KnstApiSqlServer.Queries.Example;
using KnstApiSqlServer.Repos.Example;
using KnstApiSqlServer.Services.Example;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace KnstApiSqlServer.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        // Create, Read, Update, Delete
        ISqlServerUnitOfWork UnitOfWork;
        // Readonly
        IExampleQuery ExampleQuery;
        IExampleService ExampleService;

        public ExampleController(ISqlServerUnitOfWork unitOfWork, IExampleQuery exampleQuery, IExampleService exampleService)
        {
            UnitOfWork = unitOfWork;
            ExampleQuery = exampleQuery;
            ExampleService = exampleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Example>>> Get()
        {
            var result = await ExampleQuery.GetExamplesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Example>> Get(int id)
        {
            var result = await UnitOfWork.Use<IExampleRepo>().GetExampleAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Example example)
        {
            // Use UnitOfWork Default DbSession
            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IExampleRepo>().InsertExampleAsync(example);
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();

            // Use another DbSession
            // Equal to: UnitOfWork.CreateDbSession().BeginTransaction();
            var session = UnitOfWork.CreateDbSession();
            UnitOfWork.BeginTransaction(session);

            example.Id = 0;
            await UnitOfWork.Use<IExampleRepo>(session).InsertExampleAsync(example);

            // Equal to: session.SaveChanges();
            UnitOfWork.SaveChanges(session);

            // Equal to: session.Rollback();
            UnitOfWork.Rollback(session);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Example example)
        {
            // Use UnitOfWork Default DbSession
            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IExampleRepo>().UpdateExampleAsync(id, example);
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();

            // Use another DbSession
            // Equal to: UnitOfWork.CreateDbSession().BeginTransaction();
            var session = UnitOfWork.CreateDbSession();
            UnitOfWork.BeginTransaction(session);

            await UnitOfWork.Use<IExampleRepo>(session).UpdateExampleAsync(id, example);

            // Equal to: session.SaveChanges();
            UnitOfWork.SaveChanges(session);

            // Equal to: session.Rollback();
            UnitOfWork.Rollback(session);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Use UnitOfWork Default DbSession
            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IExampleRepo>().DeleteExampleAsync(id);
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();

            // Use another DbSession

            // Equal to: UnitOfWork.CreateDbSession().BeginTransaction();
            var session = UnitOfWork.CreateDbSession();
            UnitOfWork.BeginTransaction(session);

            await UnitOfWork.Use<IExampleRepo>(session).DeleteExampleAsync(id);

            // Equal to: session.SaveChanges();
            UnitOfWork.SaveChanges(session);

            // Equal to: session.Rollback();
            UnitOfWork.Rollback(session);

            return NoContent();
        }

        [HttpPost("v2")]
        public async Task<IActionResult> PostV2(Example example)
        {
            // Use UnitOfWork Default DbSession
            UnitOfWork.BeginTransaction();
            var result1 = await ExampleService.InsertThenGetAsync(example);
            UnitOfWork.Commit();

            // Use another DbSession
            // Equal to: UnitOfWork.CreateDbSession().BeginTransaction();
            var session = UnitOfWork.CreateDbSession();
            UnitOfWork.BeginTransaction(session);

            example.Id = 0;
            var result2 = await ExampleService.InsertThenGetAsync(session, example);

            // Equal to: session.Rollback();
            UnitOfWork.Rollback(session);

            return Ok(new [] { result1 });
        }
    }
}