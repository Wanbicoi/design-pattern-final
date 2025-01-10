using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ReadDBSchema
{
    internal class PostgreSqlProvider : IDatabaseProvider
    {
        public bool CheckConnection(string connectionString)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
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
            // Server=127.0.0.1;Port=5432;Userid=u;Password=p;Protocol=3;SSL=false;Pooling=false;MinPoolSize=1;MaxPoolSize=20;Timeout=15;SslMode=Disable;Database=test"
            //return $"Server={address};Port={port};Userid={username};Password={password};Database={databaseName}";
            return "Server=127.0.0.1;Port=5432;Database=master_postgres;User Id=postgres;Password=Str0ngP@ssw0rd!;";

        }
        public DatabaseSchema GetDatabaseSchema(string connectionString)
        {
            var databaseName = GetDatabaseName(connectionString);
            var tables = GetTables(connectionString);
            return new DatabaseSchema(databaseName, tables);
        }

        private string GetDatabaseName(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
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

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var tableQuery = @"
                        SELECT table_name
                        FROM information_schema.tables
                        WHERE table_schema = 'public'";

                using (var tableCommand = new NpgsqlCommand(tableQuery, connection))
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

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var columnQuery = @"
                        SELECT column_name, data_type, is_nullable, column_default
                        FROM information_schema.columns
                        WHERE table_name = @tableName AND table_schema = 'public'";

                using (var columnCommand = new NpgsqlCommand(columnQuery, connection))
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
                            var isPrimaryKey = columnDefault != null && columnDefault.Contains("nextval");
                            var isForeignKey = false; // PostgreSQL does not store foreign key info in information_schema.columns

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
