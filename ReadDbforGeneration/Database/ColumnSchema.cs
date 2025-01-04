namespace ReadDbforGeneration.Database
{

    public interface IColumnSchema
    {

    }
    public class ColumnSchema : IColumnSchema
    {
        private string columnName { get; set; }
        private string dataType { get; set; }
        private bool isNullable { get; set; }
        private bool isPrimaryKey { get; set; }
        private bool isForeignKey { get; set; }

        public ColumnSchema(string columnName, string dataType, bool isNullable, bool isPrimaryKey, bool isForeignKey)
        {
            this.columnName = columnName;
            this.dataType = dataType;
            this.isNullable = isNullable;
            this.isPrimaryKey = isPrimaryKey;
            this.isForeignKey = isForeignKey;
        }
    }
}