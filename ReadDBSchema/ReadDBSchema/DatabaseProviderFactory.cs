using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDBSchema
{
    public class DatabaseProviderFactory
    {
        public static IDatabaseProvider GetDatabaseProvider(string databaseType)
        {
            switch (databaseType)
            {
                case "PostgreSQL":
                    return new PostgreSqlProviderFactory().GetDatabaseProvider();
                case "SQL Server":
                    return new SqlServerProviderFactory().GetDatabaseProvider();
                case "MySQL":
                    return new MySqlProviderFactory().GetDatabaseProvider();
                default:
                    throw new NotImplementedException();
            }
        }
    }

    internal class PostgreSqlProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider()
        {
            return new PostgreSqlProvider();
        }
    }

    internal class SqlServerProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider()
        {
            return new SqlServerProvider();
        }
    }

    internal class MySqlProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider()
        {
            return new MySqlProvider();
        }
    }
}
