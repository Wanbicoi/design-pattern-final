using ReadDBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericForm.ModelForms;

namespace Generater.Generator
{
    public class ModelFormMapperGenerator
    {
        // Original GenerateCode method for TableSchema
        public void GenerateCode(DatabaseSchema databaseSchema)
        {
            string code = GenerateClassCode(databaseSchema);
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, "ModelFormMapper.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, code);
        }

        // Overloaded GenerateCode method for DatabaseSchema
        public string GenerateClassCode(DatabaseSchema databaseSchema, bool overwrite = false)
        {
            var classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using System.Collections.Generic;");
            classBuilder.AppendLine("using GenericForm.DBContext;");
            classBuilder.AppendLine("namespace GenericForm.ModelForms");
            classBuilder.AppendLine("{");
            classBuilder.AppendLine("    public static class ModelFormMapper");
            classBuilder.AppendLine("    {");

            classBuilder.AppendLine("        public static readonly Dictionary<string, Type> FormTypes = new Dictionary<string, Type>()");
            classBuilder.AppendLine("        {");

            foreach (var table in databaseSchema.GetTables())
            {
                string tableName = table.GetTableName();
                string formType = $"{tableName}";
                classBuilder.AppendLine($"            {{ \"{tableName}\", typeof(ModelForms.{formType}) }},");
            }
            classBuilder.AppendLine("        };");

            classBuilder.AppendLine();
            classBuilder.AppendLine("        public static readonly Dictionary<Type, Type> ModelFormMapping = new Dictionary<Type, Type>");
            classBuilder.AppendLine("        {");

            foreach (var table in databaseSchema.GetTables())
            {
                string tableName = table.GetTableName();
                classBuilder.AppendLine($"            {{ typeof(DBContext.{tableName}), typeof(ModelForms.{tableName}) }},");
            }
            classBuilder.AppendLine("        };");

            classBuilder.AppendLine("    }");
            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        // Specify the folder path for saving ModelFormMapper class
        protected string GetFolderPath()
        {
            string SolutionBasePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(SolutionBasePath, "..", "..", "..", "..", "GenericForm", "ModelForms");
        }


    }
}
