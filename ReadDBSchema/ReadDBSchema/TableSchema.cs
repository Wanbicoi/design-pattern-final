using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDBSchema
{
    public interface ITableSchema
    {
        void AddColumn(ColumnSchema column);
        List<ColumnSchema> GetColumns();
        string GetTableName();
    }
    public class TableSchema : ITableSchema
    {
        private string tableName;
        private List<ColumnSchema> columns;
        public TableSchema(string TableName, List<ColumnSchema> columnSchemas)
        {
            this.tableName = TableName;
            columns = columnSchemas;
        }

        public TableSchema(string TableName)
        {
            this.tableName = TableName;
            columns = new List<ColumnSchema>();
        }

        public void AddColumn(ColumnSchema column)
        {
            columns.Add(column);
        }

        public string GetTableName()
        {
            return tableName;
        }

        public List<ColumnSchema> GetColumns()
        {
            return columns;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {tableName}\n");
            foreach (var column in columns)
            {
                sb.AppendLine(column.ToString());
            }
            return sb.ToString();
        }
    }
}
