using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbforGeneration.Database
{
    public class DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider(string databaseType)
        {
            switch (databaseType)
            {
                case "PostgreSql":
                    return new PostgreSqlProviderFactory().GetDatabaseProvider(databaseType);
                case "MsSql":
                    return new MsSqlProviderFactory().GetDatabaseProvider(databaseType);
                case "MySql":
                    return new MySqlProviderFactory().GetDatabaseProvider(databaseType);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    // concrete factory

    internal class PostgreSqlProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider(string databaseType)
        {
            return new PostgreSqlProvider();
        }
    }

    internal class MsSqlProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider(string databaseType)
        {
            return new MsSqlProvider();
        }
    }

    internal class MySqlProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider(string databaseType)
        {
            return new MySqlProvider();
        }
    }
}
