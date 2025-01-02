using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ReadDbforGeneration.Database
{
    internal class SqlServerProvider : DatabaseProviderBase
    {
        public override bool checkConnection()
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
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
            using (var connection = new SqlConnection(this.ConnectionString))
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
            return $"Server={address},{port};Database={databaseName};User Id={username};Password={password};";
        }

        public override List<TableSchema> GetTables()
        {
            var tables = new List<TableSchema>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                // Query to get table names
                var tableQuery = @"
                    SELECT TABLE_NAME
                    FROM INFORMATION_SCHEMA.TABLES
                    WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = @databaseName";

                using (var tableCommand = new SqlCommand(tableQuery, connection))
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
                                SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
                                FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_NAME = @tableName";

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
                                        var isPrimaryKey = false;
                                        var isForeignKey = false;

                                        // Check if the column is a primary key
                                        var pkQuery = @"
                                            SELECT COLUMN_NAME
                                            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                                            WHERE TABLE_NAME = @tableName AND CONSTRAINT_NAME LIKE 'PK_%' AND COLUMN_NAME = @columnName";

                                        using (var pkCommand = new SqlCommand(pkQuery, connection))
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
                                            SELECT COLUMN_NAME
                                            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                                            WHERE TABLE_NAME = @tableName AND CONSTRAINT_NAME LIKE 'FK_%' AND COLUMN_NAME = @columnName";

                                        using (var fkCommand = new SqlCommand(fkQuery, connection))
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
            }

            return tables;
        }


        public override void Insert(string tableName, Dictionary<string, object> values)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
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
            using (var connection = new SqlConnection(this.ConnectionString))
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

                    using (var adapter = new SqlDataAdapter(command))
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
            using (var connection = new SqlConnection(this.ConnectionString))
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
