using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using GenericForm.DBContext;

namespace Generater
{
    public static class RuntimeCompiler
    {
        public static Assembly CompileCode(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var compilation = CSharpCompilation.Create(
                "DynamicModels",
                new[] { syntaxTree },
                new[]
                {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // Core assembly
            MetadataReference.CreateFromFile(typeof(IBaseModel).Assembly.Location), // Your project assembly
                },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                var errors = result.Diagnostics
                    .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
                    .Select(diagnostic => diagnostic.ToString());
                throw new Exception("Compilation failed:\n" + string.Join("\n", errors));
            }

            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }

    }


}
