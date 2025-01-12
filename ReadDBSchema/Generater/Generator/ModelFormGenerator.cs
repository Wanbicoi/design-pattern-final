using System.Text;
using Generater.Generator;
using ReadDBSchema;

namespace Generater.Generator
{
    public class ModelFormGenerator : BaseCodeGenerator
    {
        // Generate ModelForm class code
        protected override string GenerateClassCode(TableSchema tableSchema)
        {
            var className = tableSchema.GetTableName();
            var classBuilder = new StringBuilder();

            classBuilder.AppendLine("using GenericForm.DBContext;");
            classBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            classBuilder.AppendLine("using System.ComponentModel;");
            classBuilder.AppendLine();
            classBuilder.AppendLine($"namespace GenericForm.ModelForms");
            classBuilder.AppendLine("{");
            classBuilder.AppendLine($"    public partial class {className} : BaseModel.List<DBContext.{className}>");
            classBuilder.AppendLine("    {");
            classBuilder.AppendLine($"        public {className}(BaseApplicationDbContext<DBContext.{className}> dbContext) : base(dbContext)");
            classBuilder.AppendLine("        {");
            classBuilder.AppendLine("            InitializeComponent();");
            classBuilder.AppendLine("        }");
            classBuilder.AppendLine();
            classBuilder.AppendLine("        private IContainer components = null;");
            classBuilder.AppendLine();
            classBuilder.AppendLine("        protected override void Dispose(bool disposing)");
            classBuilder.AppendLine("        {");
            classBuilder.AppendLine("            if (disposing && (components != null))");
            classBuilder.AppendLine("            {");
            classBuilder.AppendLine("                components.Dispose();");
            classBuilder.AppendLine("            }");
            classBuilder.AppendLine("            base.Dispose(disposing);");
            classBuilder.AppendLine("        }");
            classBuilder.AppendLine();
            classBuilder.AppendLine("        private void InitializeComponent()");
            classBuilder.AppendLine("        {");
            classBuilder.AppendLine("            this.components = new Container();");
            classBuilder.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;");
            classBuilder.AppendLine($"            this.Text = \"{className}\";");
            classBuilder.AppendLine("        }");
            classBuilder.AppendLine("    }");
            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        // Specify the folder path for saving ModelForms classes
        protected override string GetFolderPath()
        {
            return Path.Combine(SolutionBasePath, "..", "..", "..", "..", "GenericForm", "ModelForms");
        }
    }
}
