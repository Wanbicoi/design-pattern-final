using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ReadDBSchema
{
    internal class MySqlProvider : DatabaseProviderBase
    {
        public override bool CheckConnection(string connectionString)
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

        public override string GetConnectionString(string username, string password, string address, string port, string databaseName)
        {
            return $"Server={address};Port={port};Database={databaseName};User Id={username};Password={password};";
            //return "Server=127.0.0.1;Port=3306;Database=master;User Id=root;Password=Str0ngP@ssw0rd!;";
        }

        protected override string GetDatabaseName(string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Database;
            }
        }

        protected override List<TableSchema> GetTables(string connectionString)
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

        public override bool CreateUser(string connectionString, string username, string password, string databaseName)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Tạo user
                    string createUserQuery = $"CREATE USER '{username}'@'%' IDENTIFIED BY '{password}';";
                    using (var createUserCommand = new MySqlCommand(createUserQuery, connection))
                    {
                        createUserCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }


        public override bool LoginAndGetPermission(string connectionString, string username, string password, out string tableName)
        {
            tableName = string.Empty;
            try
            {
                // Create a new connection string with the provided username and password
                var loginConnectionString = new MySqlConnectionStringBuilder(connectionString)
                {
                    UserID = username,
                    Password = password
                }.ConnectionString;

                using (var connection = new MySqlConnection(loginConnectionString))
                {
                    connection.Open();

                    // If the connection is successful, the login credentials are valid
                    string getFirstTableQuery = @"
            SELECT TABLE_NAME
            FROM information_schema.TABLE_PRIVILEGES
            WHERE GRANTEE = CONCAT('\'', @username, '\'@\'%\'' )
            LIMIT 1;";

                    using (var cmd = new MySqlCommand(getFirstTableQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            tableName = result.ToString();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return false;
        }
    }
}
