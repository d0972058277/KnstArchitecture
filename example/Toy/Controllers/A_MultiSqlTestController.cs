using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Toy.Controllers
{
    public interface ITestRepo : IMultiSqlRepo { }
    public class TestRepo : MultiSqlRepo, ITestRepo
    {
        public TestRepo(IMultiSqlUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public interface IOldRepo : ISqlRepo { }
    public class OldRepo : SqlRepo, IOldRepo
    {
        public OldRepo(ISqlUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class A_MultiSqlTestController : ControllerBase
    {
        public IServiceProvider ServiceProvider;
        public IMultiSqlUnitOfWork UnitOfWork;

        public A_MultiSqlTestController(IServiceProvider serviceProvider, IMultiSqlUnitOfWork unitOfWork)
        {
            ServiceProvider = serviceProvider;
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Get()
        {
            var r = ServiceProvider.GetService<ITestRepo>();
            var multi = UnitOfWork.CreateDbSession();
            UnitOfWork.Use<ITestRepo>(multi);
            UnitOfWork.Use<IOldRepo>(multi.First());
            await Task.CompletedTask;
            return Ok(true);
        }
    }
}