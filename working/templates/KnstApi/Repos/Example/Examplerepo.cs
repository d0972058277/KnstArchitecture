using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Repos;

namespace KnstApi.Repos.Example
{
    public class Examplerepo : SqlRepo, IExampleRepo
    {
        public async Task DeleteExampleAsync(int id)
        {
            string sql = "DELETE FROM Test.example WHERE id = @id;";
            await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<Models.Test.Example> GetExampleAsync(int id)
        {
            string sql = "SELECT "
                + "id AS Id, "
                + "name AS Name, "
                + "row_datetime AS RowDatetime, "
                + "is_delete AS IsDelete "
                + "FROM Test.example WHERE id = @id;";
            var result = await Connection.QuerySingleAsync<Models.Test.Example>(sql, new { id }, Transaction);
            return result;
        }

        public async Task<int> InsertExampleAsync(Models.Test.Example example)
        {
            string sql = "INSERT INTO Test.example "
                + "(name, row_datetime, is_delete) "
                + "VALUES "
                + "(@Name, @RowDatetime, @IsDelete); "
                + "SELECT LAST_INSERT_ID();";
            var result = await Connection.QuerySingleAsync<int>(sql, example, Transaction);
            return result;
        }

        public async Task UpdateExampleAsync(int id, Models.Test.Example example)
        {
            example.Id = id;
            string sql = "UPDATE Test.example "
                + "SET "
                + "name = @Name, "
                + "row_datetime = @RowDatetime, "
                + "is_delete = @IsDelete "
                + "WHERE id = @Id";
            await Connection.ExecuteAsync(sql, example, Transaction);
        }
    }
}