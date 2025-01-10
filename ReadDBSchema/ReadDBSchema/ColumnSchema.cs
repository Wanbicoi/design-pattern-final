using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDBSchema
{
    public interface IColumnSchema
    {
        string GetColumnName();

    }
    public class ColumnSchema : IColumnSchema
    {
        private string columnName { get; set; }
        private string dataType { get; set; }
        private bool isNullable { get; set; }
        private bool isPrimaryKey { get; set; }

        public ColumnSchema(string columnName, string dataType, bool isNullable, bool isPrimaryKey, bool isForeignKey)
        {
            this.columnName = columnName;
            this.dataType = dataType;
            this.isNullable = isNullable;
            this.isPrimaryKey = isPrimaryKey;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Column: {columnName}");
            sb.AppendLine($"Data Type: {dataType}");
            sb.AppendLine($"Nullable: {isNullable}");
            sb.AppendLine($"Primary Key: {isPrimaryKey}");
            return sb.ToString();
        }

        public string GetColumnName()
        {
            return columnName;
        }

    }
}
