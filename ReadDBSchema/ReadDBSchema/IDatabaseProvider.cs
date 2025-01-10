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
    }
}
