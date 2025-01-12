using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ReadDBSchema
{
    internal class SqlServerProvider : IDatabaseProvider
    {
        public bool CheckConnection(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }


        public string GetConnectionString(string username, string password, string address, string port, string databaseName)
        {

            //commented by Chan to test without docker

            //string _username = "SA";
            //string _password = "Str0ngP@ssw0rd!";
            //string _address = "localhost";
            //string _port = "1433";
            //string _databaseName = "master";

            //// Create an instance of SqlConnectionStringBuilder
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            //// Set individual properties based on the input parameters
            //builder.DataSource = $"{_address},{_port}"; // Server address and port
            //builder.InitialCatalog = _databaseName;   // Database name
            //builder.UserID = _username;               // Username for authentication
            //builder.Password = _password;             // Password for authentication

            //// Return the constructed connection string
            //return builder.ConnectionString;



            return "Data Source=HAICHANNGUYEN\\BI2425;Initial Catalog=winformdb;Integrated Security=True;Trust Server Certificate=True";

        }


        public DatabaseSchema GetDatabaseSchema(string connectionString)
        {
            var databaseName = GetDatabaseName(connectionString);
            var tables = GetTables(connectionString);
            return new DatabaseSchema(databaseName, tables);
        }

        private string GetDatabaseName(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Database;
            }
        }

        private List<TableSchema> GetTables(string connectionString)
        {
            var tables = new List<TableSchema>();
            var tableNames = GetTableNames(connectionString);

            foreach (var tableName in tableNames)
            {
                var tableSchema = GetTableSchema(connectionString, tableName);
                tables.Add(tableSchema);
            }

            return tables;
        }

        private List<string> GetTableNames(string connectionString)
        {
            var tableNames = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableQuery = @"
                        SELECT table_name
                        FROM information_schema.tables
                        WHERE table_schema = 'dbo'";

                using (var tableCommand = new SqlCommand(tableQuery, connection))
                {
                    using (var reader = tableCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return tableNames;
        }

        private TableSchema GetTableSchema(string connectionString, string tableName)
        {
            var tableSchema = new TableSchema(tableName);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var columnQuery = @"
                        SELECT column_name, data_type, is_nullable, column_default
                        FROM information_schema.columns
                        WHERE table_name = @tableName AND table_schema = 'dbo'";

                using (var columnCommand = new SqlCommand(columnQuery, connection))
                {
                    columnCommand.Parameters.AddWithValue("@tableName", tableName);

                    using (var columnReader = columnCommand.ExecuteReader())
                    {
                        while (columnReader.Read())
                        {
                            var columnName = columnReader.GetString(0);
                            var dataType = columnReader.GetString(1);
                            var isNullable = columnReader.GetString(2) == "YES";
                            var columnDefault = columnReader.IsDBNull(3) ? null : columnReader.GetString(3);
                            var isPrimaryKey = false; // SQL Server does not store primary key info in information_schema.columns
                            var isForeignKey = false; // SQL Server does not store foreign key info in information_schema.columns

                            var columnSchema = new ColumnSchema(
                                columnName,
                                dataType,
                                isNullable,
                                isPrimaryKey,
                                isForeignKey
                            );

                            tableSchema.AddColumn(columnSchema);
                        }
                    }
                }
            }

            return tableSchema;
        }
    }
}
