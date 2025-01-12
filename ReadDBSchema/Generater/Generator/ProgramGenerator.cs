using ReadDBSchema;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generater.Generator
{
    public class ProgramGenerator 
    {
        public ProgramGenerator()
        {
        }

        public void GenerateCode(string databaseType, string connectionString, bool overwrite = true)
        {

            string code = GenerateClassCode(databaseType, connectionString);
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, "Program.cs");

            // Check if the file already exists before generating code
            if (File.Exists(filePath) && !overwrite)
            {
                Console.WriteLine($"[INFO] Skipping generation. File already exists: {filePath}");
                return;
            }

            SaveGeneratedCode(filePath, code);
        }

        protected void SaveGeneratedCode(string filePath, string code)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, code);
            Console.WriteLine($"[SUCCESS] File generated: {filePath}");
        }


        // Generate model class code
        protected string GenerateClassCode(string databaseType, string connectionString)
        {
            connectionString  = connectionString.Replace(@"\", @"\\");
            var classBuilder = new StringBuilder();
            classBuilder.AppendLine("using GenericForm.DBContext;");

            classBuilder.AppendLine("namespace GenericForm");
            classBuilder.AppendLine("{");
            classBuilder.AppendLine("internal static class Program");
            classBuilder.AppendLine("{");


            classBuilder.AppendLine("[STAThread]");
            classBuilder.AppendLine("static void Main()");
            classBuilder.AppendLine("{");

            classBuilder.AppendLine("ApplicationConfiguration.Initialize();");

            classBuilder.AppendLine($"Application.Run(new MainWindow(\"{databaseType}\", \"{connectionString}\"));");
            classBuilder.AppendLine("}");
            classBuilder.AppendLine("}");
            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        // Specify the folder path for saving model classes
        protected string GetFolderPath()
        {
            string SolutionBasePath =AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(SolutionBasePath, "..", "..", "..", "..", "GenericForm");
        }
    }
}
