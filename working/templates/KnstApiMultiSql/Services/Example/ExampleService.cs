using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnstApiMultiSql.Repos.Example;
using KnstArchitecture.Services;
using KnstArchitecture.UnitOfWorks;

namespace KnstApiMultiSql.Services.Example
{
    public class ExampleService : IExampleService, IService
    {
        public IMultiSqlUnitOfWork UnitOfWork;

        public ExampleService(IMultiSqlUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Models.Test.Example>> MultiRepoDefaultGetAsync()
        {
            // 設定默認使用 SqlDbSession 的過濾條件
            UnitOfWork.GetDefaultDbSession().SetDefaultFilter(sessions => sessions.First());

            // 這邊所使用的 Repo 必須是 MultiSqlRepo 型別
            var repo = UnitOfWork.Use<IExampleMultiRepo>();
            var result = repo.DefaultGetAsync();
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> RepoDefaultGetAsync()
        {
            // 先將想要使用的 SqlDbSession 挑選出來
            var session = UnitOfWork.GetDefaultDbSession().First();

            // 這邊所使用的 Repo 必須是 SqlRepo 型別
            // 讓 SqlRepo 使用已選擇好的 SqlDbSession
            // 這種寫法相對直覺簡單，但是不支援建構子注入(Dose Not Support Constructor Injection)
            var repo = UnitOfWork.Use<IExampleRepo>(session);
            var result = repo.GetAsync();
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> FirstGetAsync()
        {
            // 這邊所使用的 Repo 必須是 MultiSqlRepo 型別
            var repo = UnitOfWork.Use<IExampleMultiRepo>();
            var result = repo.FirstGetAsync();
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> SecondGetAsync()
        {
            // 這邊所使用的 Repo 必須是 MultiSqlRepo 型別
            var repo = UnitOfWork.Use<IExampleMultiRepo>();
            var result = repo.SecondGetAsync();
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> ThirdGetAsync()
        {
            // 這邊所使用的 Repo 必須是 MultiSqlRepo 型別
            var repo = UnitOfWork.Use<IExampleMultiRepo>();
            var result = repo.ThirdGetAsync();
            return result;
        }
    }
}