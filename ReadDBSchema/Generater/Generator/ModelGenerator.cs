using System.Net.NetworkInformation;
using System.Text;
using ReadDBSchema;

namespace Generater.Generator
{
    public class ModelGenerator : BaseCodeGenerator
    {
        private readonly IDatabaseTypeMapper _converter;

        public ModelGenerator(IDatabaseTypeMapper converter)
        {
            _converter = converter;
        }

        // Generate model class code
        protected override string GenerateClassCode(TableSchema tableSchema)
        {
            var classBuilder = new StringBuilder();
            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using GenericForm;");
            classBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
            classBuilder.AppendLine();
            classBuilder.AppendLine($"namespace GenericForm.DBContext");
            classBuilder.AppendLine("{"); 
            classBuilder.AppendLine($"public class {tableSchema.GetTableName()} : IBaseModel");
            classBuilder.AppendLine("{");

            foreach (var column in tableSchema.GetColumns())
            {
                if (column.IsPrimaryKey())
                {
                    classBuilder.AppendLine("[Key]");
                }
                Type csharpType = _converter.MapToCSharp(column.GetDataType());
                string nullableSuffix = column.IsNullable() && csharpType.IsValueType ? "?" : string.Empty;
                classBuilder.AppendLine($"    public {csharpType.Name}{nullableSuffix} {column.GetColumnName()} {{ get; set; }}");
            }

            classBuilder.AppendLine("}");
            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        // Specify the folder path for saving model classes
        protected override string GetFolderPath()
        {
            return Path.Combine(SolutionBasePath, "..", "..", "..", "..", "GenericForm", "Models");
        }
    }
}
