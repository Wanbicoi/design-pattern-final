using System.ComponentModel;
using System.Text;
using ReadDBSchema;
using System.IO;

namespace Generater
{
    public class Generator
    {
        private ReadDBSchema.IDatabaseTypeMapper _converter;

        public Generator(ReadDBSchema.IDatabaseTypeMapper converter)
        {
            _converter = converter;
        }

        public string GenerateModelCode(TableSchema tableSchema)
        {
            var classBuilder = new StringBuilder();
            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using GenericForm;"); // Namespace of IBaseModel
            classBuilder.AppendLine();
            classBuilder.AppendLine($"public class {tableSchema.GetTableName()} : IBaseModel");
            classBuilder.AppendLine("{");

            foreach (var column in tableSchema.GetColumns())
            {
                Type csharpType = _converter.MapToCSharp(column.GetDataType()); // Adjust to match your DB type
                String nullableSuffix = column.IsNullable() && csharpType.IsValueType ? "?" : string.Empty;
                classBuilder.AppendLine($"    public {csharpType.Name}{nullableSuffix} {column.GetColumnName()} {{ get; set; }}");
            }

            classBuilder.AppendLine("    public int ID { get; set; } // IBaseModel requirement");
            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        public void SaveGeneratedCode(string className, string code)
        {
            string solutionBasePath = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(solutionBasePath, "..", "..", "..", "..", "GenericForm", "Models");

            Directory.CreateDirectory(folderPath);
            File.WriteAllText(Path.Combine(folderPath, $"{className}.cs"), code);
        }
    }
}
