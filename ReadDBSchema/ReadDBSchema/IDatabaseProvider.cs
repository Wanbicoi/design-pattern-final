using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract DatabaseSchema GetDatabaseSchema(string connectionString);
        public abstract bool CheckConnection(string connectionString);
        public string GetConnectionString(string username, string password, string address, string port, string databaseName)
        {

            return $"Server={address};Port={port};Database={databaseName};User Id={username};Password={password};";
        }
        public abstract bool CreateUser(string connectionString, string username, string password, string tableName);
        public abstract bool LoginAndGetPermission(string connectionString, string username, string password, out string tableName);
    }
}
