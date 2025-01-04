using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbforGeneration.Database
{

    public interface ITableSchema
    {
        string TableName { get; }
        void AddColumn(ColumnSchema column);
        List<ColumnSchema> Columns { get; }
    }
    public class TableSchema : ITableSchema
    {
        private string tableName;
        private List<ColumnSchema> columns;


        public TableSchema(string tableName)
        {
            this.tableName = tableName;
            columns = new List<ColumnSchema>();
        }
         
        public TableSchema(string TableName, List<ColumnSchema> columnSchemas)
        {
            this.tableName = TableName;
            columns = columnSchemas;
        }

        public void AddColumn(ColumnSchema column)
        {
            columns.Add(column);
        }

        public string TableName
        {
            get { return tableName; }
        }

        public List<ColumnSchema> Columns
        {
            get { return columns; }
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
