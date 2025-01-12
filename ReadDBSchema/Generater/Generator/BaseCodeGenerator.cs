using System.ComponentModel;
using System.Text;
using ReadDBSchema;
using System.IO;
using System.Text;
using System.IO;

namespace Generater.Generator
{
    public abstract class BaseCodeGenerator
    {
        protected string SolutionBasePath => AppDomain.CurrentDomain.BaseDirectory;

        // Template Method
        public void GenerateCode(TableSchema tableSchema, bool overwrite = false)
        {
            string className = tableSchema.GetTableName();
            string code = GenerateClassCode(tableSchema);
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, $"{className}.cs");

            // Check if the file already exists before generating code
            if (File.Exists(filePath) && !overwrite)
            {
                Console.WriteLine($"[INFO] Skipping generation. File already exists: {filePath}");
                return;
            }

            SaveGeneratedCode(filePath, className, code);
        }

        // Abstract methods to be implemented by subclasses
        protected abstract string GenerateClassCode(TableSchema tableSchema);
        protected abstract string GetFolderPath();

        // Common method to save generated code
        protected void SaveGeneratedCode(string filePath, string className, string code)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, code);
            Console.WriteLine($"[SUCCESS] File generated: {filePath}");
        }
    }
}
