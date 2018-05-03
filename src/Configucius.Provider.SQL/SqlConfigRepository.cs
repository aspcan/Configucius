using System.Collections.Generic;
using System.Data.SqlClient;
using Configucius.Core;
using Dapper;

namespace Configucius.Provider.SQL
{
    public class SqlConfigRepository : IConfigRepository
    {
        private readonly string _connectionString;

        public SqlConfigRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Config> GetValues(string domain, string environment)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = sqlConnection.Query<Config>("select [Key], Value, UpdatedAt from Setting where IsActive = 1 and Domain = @Domain and Environment = @Environment", new { Domain = domain, Environment = environment });

                return query;
            }
        }
    }
}