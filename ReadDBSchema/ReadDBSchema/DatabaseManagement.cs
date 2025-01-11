using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDBSchema
{

    public static class DatabaseType
    {
        public static List<string> GetDatabaseTypes()
        {
            return new List<string> {"MySQL", "PostgreSQL", "SQL Server"};
        }
    }

    public class DatabaseManagement
    {
        private string ConnectionString;
        private IDatabaseProvider Provider;
        private string DatabaseType;

        public DatabaseManagement(string username, string password, string address, string port, string databaseName, string databaseType)
        {
            this.Provider = DatabaseProviderFactory.GetDatabaseProvider(databaseType);
            this.ConnectionString = this.Provider.GetConnectionString(username, password, address, port, databaseName);
            this.DatabaseType = databaseType;
        }

        public bool CheckConnection()
        {
            return this.Provider.CheckConnection(this.ConnectionString);
        }

        public string GetConnectionString()
        {
            return this.ConnectionString;
        }

        public DatabaseSchema GetDatabaseSchema()
        {
            return this.Provider.GetDatabaseSchema(this.ConnectionString);
        }

        public string GetDatabaseType()
        {
            return this.DatabaseType;
        }
    }
}
