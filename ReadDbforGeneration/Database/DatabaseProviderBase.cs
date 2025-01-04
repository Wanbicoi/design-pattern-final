using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbforGeneration.Database
{
    public abstract class DatabaseProviderBase : IDatabaseProvider
    {
        protected string ConnectionString;

        public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public DatabaseProviderBase()
        {
            ConnectionString = "";
        }

        public abstract void Insert(string tableName, Dictionary<string, object> values);
        public abstract void Update(string tableName, Dictionary<string, object> values, Dictionary<string, object> conditions);
        public abstract void Delete(string tableName, Dictionary<string, object> conditions);
        public abstract DataTable Read(string tableName, Dictionary<string, object> conditions);
        public abstract List<TableSchema> GetTables();
        public abstract bool checkConnection();
        public abstract string getConnectionString(string username, string password, string address, string port, string databaseName);

    }
}
