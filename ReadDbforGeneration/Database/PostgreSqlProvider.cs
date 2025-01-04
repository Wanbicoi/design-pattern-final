using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ReadDbforGeneration.Database
{
    internal class PostgreSqlProvider : DatabaseProviderBase
    {
        public override bool checkConnection()
        {
            try
            {
                using (var connection = new NpgsqlConnection(this.ConnectionString))
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
            using (var connection = new NpgsqlConnection(this.ConnectionString))
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
            return $"Host={address};Port={port};Username={username};Password={password};Database={databaseName}";
        }

        public override List<TableSchema> GetTables()
        {
            var tables = new List<TableSchema>();

            using (var connection = new NpgsqlConnection(this.ConnectionString))
            {
                connection.Open();

                // Query to get table names
                var tableQuery = @"
                    SELECT table_name
                    FROM information_schema.tables
                    WHERE table_schema = 'public' AND table_type = 'BASE TABLE'";

                using (var tableCommand = new NpgsqlCommand(tableQuery, connection))
                using (var reader = tableCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tableName = reader.GetString(0);
                        var tableSchema = new TableSchema(tableName);

                        // Query to get column details for each table
                        var columnQuery = @"
                            SELECT column_name, data_type, is_nullable, column_default
                            FROM information_schema.columns
                            WHERE table_name = @tableName";

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
                                    var isPrimaryKey = false;
                                    var isForeignKey = false;

                                    // Check if the column is a primary key
                                    var pkQuery = @"
                                        SELECT kcu.column_name
                                        FROM information_schema.table_constraints tc
                                        JOIN information_schema.key_column_usage kcu
                                        ON tc.constraint_name = kcu.constraint_name
                                        WHERE tc.table_name = @tableName AND tc.constraint_type = 'PRIMARY KEY' AND kcu.column_name = @columnName";

                                    using (var pkCommand = new NpgsqlCommand(pkQuery, connection))
                                    {
                                        pkCommand.Parameters.AddWithValue("@tableName", tableName);
                                        pkCommand.Parameters.AddWithValue("@columnName", columnName);

                                        using (var pkReader = pkCommand.ExecuteReader())
                                        {
                                            if (pkReader.Read())
                                            {
                                                isPrimaryKey = true;
                                            }
                                        }
                                    }

                                    // Check if the column is a foreign key
                                    var fkQuery = @"
                                        SELECT kcu.column_name
                                        FROM information_schema.table_constraints tc
                                        JOIN information_schema.key_column_usage kcu
                                        ON tc.constraint_name = kcu.constraint_name
                                        WHERE tc.table_name = @tableName AND tc.constraint_type = 'FOREIGN KEY' AND kcu.column_name = @columnName";

                                    using (var fkCommand = new NpgsqlCommand(fkQuery, connection))
                                    {
                                        fkCommand.Parameters.AddWithValue("@tableName", tableName);
                                        fkCommand.Parameters.AddWithValue("@columnName", columnName);

                                        using (var fkReader = fkCommand.ExecuteReader())
                                        {
                                            if (fkReader.Read())
                                            {
                                                isForeignKey = true;
                                            }
                                        }
                                    }
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

            return tables;
        }

        public override void Insert(string tableName, Dictionary<string, object> values)
        {
            using (var connection = new NpgsqlConnection(this.ConnectionString))
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
            using (var connection = new NpgsqlConnection(this.ConnectionString))
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

                    using (var adapter = new NpgsqlDataAdapter(command))
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
            using (var connection = new NpgsqlConnection(this.ConnectionString))
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
