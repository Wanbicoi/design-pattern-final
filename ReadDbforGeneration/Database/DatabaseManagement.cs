using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbforGeneration.Database
{
    public class DatabaseManagement
    {
        private static DatabaseManagement instance;
        private static readonly object LockObj = new object();
        private string ConnectionString { get; set; }
        private IDatabaseProvider Provider { get; set; }

        private DatabaseManagement() { 
        }

        public static DatabaseManagement Instance
        {
            get
            {
                lock (LockObj)
                {
                    if (instance == null)
                    {
                        instance = new DatabaseManagement();
                    }
                    return instance;
                }
            }
        }

        public void Initialize(string username, string password, string address, string port, string databaseName, string databaseType)
        {

            Provider = DatabaseProviderFactory.GetDatabaseProvider(databaseType);
            this.ConnectionString = Provider.getConnectionString(username, password, address, port, databaseName,  databaseType);

        }


        public void Insert(string tableName, Dictionary<string, object> values)
        {
            Provider.Insert(tableName, values, ConnectionString);
        }

        public void Update(string table, Dictionary<string, object> data, Dictionary<string, object> conditions)
        {
            // Implement logic for update operation
        }

        public void Delete(string table, Dictionary<string, object> conditions)
        {
            // Implement logic for delete operation
        }

        public DataTable Read(string table, Dictionary<string, object> conditions)
        {
            // Implement logic for read operation
        }

    }
}
