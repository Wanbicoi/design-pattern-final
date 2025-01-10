using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ReadDBSchema
{
    internal class MySqlProvider : IDatabaseProvider
    {
        public bool CheckConnection(string connectionString)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
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
            //return $"Server={address};Port={port};Database={databaseName};User Id={username};Password={password};";

            return "Server=127.0.0.1;Port=3306;Database=master;User Id=root;Password=Str0ngP@ssw0rd!;";


        }

        public DatabaseSchema GetDatabaseSchema(string connectionString)
        {
            var databaseName = GetDatabaseName(connectionString);
            var tables = GetTables(connectionString);
            return new DatabaseSchema(databaseName, tables);
        }

        private string GetDatabaseName(string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
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

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var tableQuery = @"
                        SELECT table_name
                        FROM information_schema.tables
                        WHERE table_schema = @databaseName";

                using (var tableCommand = new MySqlCommand(tableQuery, connection))
                {
                    tableCommand.Parameters.AddWithValue("@databaseName", connection.Database);

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

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var columnQuery = @"
                        SELECT column_name, data_type, is_nullable, column_key
                        FROM information_schema.columns
                        WHERE table_name = @tableName AND table_schema = @databaseName";

                using (var columnCommand = new MySqlCommand(columnQuery, connection))
                {
                    columnCommand.Parameters.AddWithValue("@tableName", tableName);
                    columnCommand.Parameters.AddWithValue("@databaseName", connection.Database);

                    using (var columnReader = columnCommand.ExecuteReader())
                    {
                        while (columnReader.Read())
                        {
                            var columnName = columnReader.GetString(0);
                            var dataType = columnReader.GetString(1);
                            var isNullable = columnReader.GetString(2) == "YES";
                            var columnKey = columnReader.GetString(3);
                            var isPrimaryKey = columnKey == "PRI";
                            var isForeignKey = columnKey == "MUL";

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
