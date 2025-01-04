using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ReadDbforGeneration.Database
{
    internal class MySqlProvider : DatabaseProviderBase
    {
        public override bool checkConnection()
        {
            try
            {
                using (var connection = new MySqlConnection(this.ConnectionString))
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

        public override void Delete(string tableName, Dictionary<string, object> conditions)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var whereClause = string.Join(" AND ", conditions.Keys.Select(k => $"{k} = @cond_{k}"));
                    command.CommandText = $"DELETE FROM {tableName} WHERE {whereClause}";

                    foreach (var kvp in conditions)
                    {
                        command.Parameters.AddWithValue("@cond_" + kvp.Key, kvp.Value ?? DBNull.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public override string getConnectionString(string username, string password, string address, string port, string databaseName)
        {
            return $"Server={address};Port={port};Database={databaseName};User={username};Password={password};";
        }

        public override List<TableSchema> GetTables()
        {
            var tables = new List<TableSchema>();

            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();

                // Query to get table names
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
                            var tableName = reader.GetString(0);
                            var tableSchema = new TableSchema(tableName);

                            // Query to get column details for each table
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

                                        tableSchema.Columns.Add(columnSchema);
                                    }
                                }
                            }

                            tables.Add(tableSchema);
                        }
                    }
                }
            }

            return tables;
        }

        public override void Insert(string tableName, Dictionary<string, object> values)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var columns = string.Join(", ", values.Keys);
                    var parameters = string.Join(", ", values.Keys.Select(k => "@" + k));
                    command.CommandText = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

                    foreach (var kvp in values)
                    {
                        command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public override DataTable Read(string tableName, Dictionary<string, object> conditions)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var whereClause = string.Join(" AND ", conditions.Keys.Select(k => $"{k} = @cond_{k}"));
                    command.CommandText = $"SELECT * FROM {tableName} WHERE {whereClause}";

                    foreach (var kvp in conditions)
                    {
                        command.Parameters.AddWithValue("@cond_" + kvp.Key, kvp.Value ?? DBNull.Value);
                    }

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public override void Update(string tableName, Dictionary<string, object> values, Dictionary<string, object> conditions)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var setClause = string.Join(", ", values.Keys.Select(k => $"{k} = @{k}"));
                    var whereClause = string.Join(" AND ", conditions.Keys.Select(k => $"{k} = @cond_{k}"));
                    command.CommandText = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

                    foreach (var kvp in values)
                    {
                        command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);
                    }

                    foreach (var kvp in conditions)
                    {
                        command.Parameters.AddWithValue("@cond_" + kvp.Key, kvp.Value ?? DBNull.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
