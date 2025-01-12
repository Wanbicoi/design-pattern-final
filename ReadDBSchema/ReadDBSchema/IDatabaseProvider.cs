using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ReadDBSchema
{
    public interface IDatabaseProvider
    {
        DatabaseSchema GetDatabaseSchema(string connectionString);
        bool CheckConnection(string connectionString);
        string GetConnectionString(string username, string password, string address, string port, string databaseName);
        bool CreateUser(string connectionString, string username, string password, string tableName);
        bool LoginAndGetPermission(string connectionString, string username, string password, out string tableName);
    }

    public abstract class DatabaseProviderBase : IDatabaseProvider
    {
        public DatabaseSchema GetDatabaseSchema(string connectionString)
        {
            var databaseName = GetDatabaseName(connectionString);
            var tables = GetTables(connectionString);
            return new DatabaseSchema(databaseName, tables);
        }
        public abstract bool CheckConnection(string connectionString);
        public abstract string GetConnectionString(string username, string password, string address, string port, string databaseName);
        public abstract bool CreateUser(string connectionString, string username, string password, string tableName);
        public abstract bool LoginAndGetPermission(string connectionString, string username, string password, out string tableName);

        protected abstract string GetDatabaseName(string connectionString);
        protected abstract List<TableSchema> GetTables(string connectionString);
    }
}

