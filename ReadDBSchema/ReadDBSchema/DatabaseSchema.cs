using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDBSchema
{
    interface IDatabaseSchema
    {
        void AddTable(TableSchema table);
        List<TableSchema> GetTables();
        TableSchema GetTable(string tableName);
        int GetTableCount();
    }
    public class DatabaseSchema : IDatabaseSchema
    {

        public string DatabaseName { get; set; }
        public List<TableSchema> TableSchemas;

        public DatabaseSchema(string databaseName, List<TableSchema> tableSchemas)
        {
            DatabaseName = databaseName;
            TableSchemas = tableSchemas;
               
        }

        public void AddTable(TableSchema table)
        {
            TableSchemas.Add(table);
        }

        public TableSchema GetTable(string tableName)
        {
            TableSchema result = null;
            foreach (TableSchema t in TableSchemas)
            {
                if (t.GetTableName() == tableName)
                {
                    result = t;
                    break;
                }
            }
            if (result == null)
            {
                throw new Exception($"Table {tableName} not found");
            }
            return result;
        }

        public List<TableSchema> GetTables()
        {
            return TableSchemas;
        }

        public int GetTableCount()
        {
            return TableSchemas.Count;
        }

        public void RemoveTable(TableSchema table)
        {
            if (TableSchemas.Contains(table))
            {
                TableSchemas.Remove(table);
            }
            else
            {
                throw new Exception($"Table {table.GetTableName()} not found in the database.");
            }
        }
    }
}
