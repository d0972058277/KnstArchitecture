using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Toy.Repos.MgExample;

namespace Toy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoDbExampleController : ControllerBase
    {
        IMongoUnitOfWork UnitOfWork;

        public MongoDbExampleController(IMongoUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.MongoTest.MgExample>>> Get()
        {
            var result = await UnitOfWork.Use<IMgExampleRepo>().GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.MongoTest.MgExample>> Get(string id)
        {
            var result = await UnitOfWork.Use<IMgExampleRepo>().GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.MongoTest.MgExample example)
        {
            UnitOfWork.BeginTransaction();
            await UnitOfWork.Use<IMgExampleRepo>().PostAsync(example);
            UnitOfWork.Commit();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeleteResult>> PutAsync(string id, Models.MongoTest.MgExample example)
        {
            if (id != example.Id) return BadRequest();
            var entity = await UnitOfWork.Use<IMgExampleRepo>().GetAsync(id);
            if (entity == null) return NotFound();

            UnitOfWork.BeginTransaction();
            var result = await UnitOfWork.Use<IMgExampleRepo>().PutAsync(example);
            UnitOfWork.Commit();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResult>> DeleteAsync(string id)
        {
            UnitOfWork.BeginTransaction();
            var result = await UnitOfWork.Use<IMgExampleRepo>().DeleteAsync(id);
            UnitOfWork.Commit();

            return Ok(result);
        }
    }
}