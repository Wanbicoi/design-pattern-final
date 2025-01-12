﻿using System;
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
                case "MySQL":
                    return new MySqlProviderFactory().GetDatabaseProvider();
                default:
                    throw new NotImplementedException();
            }
        }

        public static IDatabaseTypeMapper? GetDataTypeMapper(string databaseType)
        {
            switch (databaseType)
            {
                case "PostgreSQL":
                    return new PostgreSqlTypeToCSharpMapper();
                case "MySQL":
                    return new MySqlTypeToCSharpMaperr();
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


    internal class MySqlProviderFactory : DatabaseProviderFactory
    {
        public IDatabaseProvider GetDatabaseProvider()
        {
            return new MySqlProvider();
        }
    }
}
