using System;
using System.Data;
using Npgsql;

namespace ReadDBSchema
{
    internal class PostgreSqlProvider : DatabaseProviderBase
    {
        public override bool CheckConnection(string connectionString)
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
        public override string GetConnectionString(string username, string password, string address, string port, string databaseName)
        {
            //return $"Server={address};Port={port};Userid={username};Password={password};Database={databaseName}";
            return "Server=127.0.0.1;Port=5432;Database=master_postgres;User Id=postgres;Password=Str0ngP@ssw0rd!;";

        }

        public override string GetDatabaseName(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return connection.Database;
            }
        }

        public override List<TableSchema> GetTables(string connectionString)
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

        public override bool CreateUser(string connectionString, string username, string password, string tableName)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Create the role (user)
                    string createUserQuery = $"CREATE ROLE {username} WITH LOGIN PASSWORD '{password}';";
                    using (var createUserCommand = new NpgsqlCommand(createUserQuery, connection))
                    {
                        createUserCommand.ExecuteNonQuery();
                    }

                    // Grant privileges to the role on the specified table
                    string grantPrivilegesQuery = $"GRANT SELECT, INSERT, UPDATE, DELETE ON TABLE {tableName} TO {username};";
                    using (var grantPrivilegesCommand = new NpgsqlCommand(grantPrivilegesQuery, connection))
                    {
                        grantPrivilegesCommand.ExecuteNonQuery();
                    }

                    // Commit changes
                    string commitQuery = "COMMIT;";
                    using (var commitCommand = new NpgsqlCommand(commitQuery, connection))
                    {
                        commitCommand.ExecuteNonQuery();
                    }
                }

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                return false; // Indicate failure
            }
        }

        public override bool LoginAndGetPermission(string connectionString, string username, string password, out string tableName)
        {
            tableName = string.Empty;

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra tên người dùng và mật khẩu có hợp lệ không
                    string checkUserQuery = $"SELECT 1 FROM pg_roles WHERE rolname = '{username}' AND rolpassword = crypt('{password}', rolpassword);";
                    using (var cmd = new NpgsqlCommand(checkUserQuery, connection))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            return false;
                        }
                    }

                    string getFirstTableQuery = @"
                    SELECT table_name
                    FROM information_schema.role_table_grants
                    WHERE grantee = @username
                    LIMIT 1;
                ";

                    using (var cmd = new NpgsqlCommand(getFirstTableQuery, connection))
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