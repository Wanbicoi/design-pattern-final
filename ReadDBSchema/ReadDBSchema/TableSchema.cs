using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

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
        //public List<ForeignKeySchema> ForeignKeys;
        public TableSchema(string TableName, List<ColumnSchema> columnSchemas)
        {
            this.tableName = TableName;
            columns = columnSchemas;
            //ForeignKeys = new List<ForeignKeySchema>();
        }

        public TableSchema(string TableName)
        {
            this.tableName = TableName;
            columns = new List<ColumnSchema>();
            //ForeignKeys = new List<ForeignKeySchema>();
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

        //public void addForeignKey(ForeignKeySchema foreignKey)
        //{
        //    ForeignKeys.Add(foreignKey);
        //}

        //public void addForeignKeys(List<ForeignKeySchema> foreignKeys)
        //{
        //    ForeignKeys.AddRange(foreignKeys);
        //}
    }


}
