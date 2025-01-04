using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbforGeneration.Database
{
    public interface IDatabaseProvider
    {
        void Insert(string tableName, Dictionary<string, object> values);
        void Update(string tableName, Dictionary<string, object> values, Dictionary<string, object> conditions);
        void Delete(string tableName, Dictionary<string, object> conditions);
        DataTable Read(string tableName, Dictionary<string, object> conditions);
        List<TableSchema> GetTables();
        bool checkConnection();
        void SetConnectionString(string connectionString);
        string getConnectionString(string username, string password, string address, string port, string databaseName);


    }
}
