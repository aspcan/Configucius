using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Configucius.Core;
using Dapper;

namespace Configucius.Provider.SQL
{
    public class SqlConfigRepository : IConfigRepository
    {
        public IEnumerable<Config> GetValues(string domain, string environment)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Configucius_ConnectionString"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var query = sqlConnection.Query<Config>("select [Key], Value, UpdatedAt from Setting where IsActive = 1 and Domain = @Domain and Environment = @Environment", new { Domain = domain, Environment = environment });

                return query;
            }
        }
    }
}