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
            return new List<string> { "MySQL", "PostgreSQL" };
        }
    }

    public class DatabaseManagement
    {
        private string ConnectionString;
        private IDatabaseProvider Provider;
        private string DatabaseType;
        private IDatabaseTypeMapper DatabaseTypeMapper;

        public DatabaseManagement(string username, string password, string address, string port, string databaseName, string databaseType)
        {
            this.Provider = DatabaseProviderFactory.GetDatabaseProvider(databaseType);
            this.ConnectionString = this.Provider.GetConnectionString(username, password, address, port, databaseName);
            this.DatabaseType = databaseType;
            this.DatabaseTypeMapper = DatabaseProviderFactory.GetDataTypeMapper(databaseType);
        }

        public DatabaseManagement(string conn, string databaseType)
        {
            this.Provider = DatabaseProviderFactory.GetDatabaseProvider(databaseType);
            this.ConnectionString = conn;
            this.DatabaseType = databaseType;
            this.DatabaseTypeMapper = DatabaseProviderFactory.GetDataTypeMapper(databaseType);
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

        public IDatabaseTypeMapper GetDatabaseTypeMapper()
        {
            return this.DatabaseTypeMapper;
        }

        public bool CreateUser(string username, string password, string tableName)
        {
            return this.Provider.CreateUser(this.ConnectionString, username, password, tableName);
        }

        public bool LoginAndGetPermission(string username, string password, out string tableName)
        {
            return this.Provider.LoginAndGetPermission(this.ConnectionString, username, password, out tableName);
        }
    }
}
