using System;
using System.IO;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;


namespace Generater.Builder { 
public class ProjectBuilder
{
    public  void Main()
    {
        var root = ProjectRootElement.Create();

        // Build configuration
        var group = root.AddPropertyGroup();
        group.AddProperty("Configuration", "Release");
        group.AddProperty("Platform", "x64");

        // Add references
        AddItems(root, "Reference",
            "System",
            "Microsoft.EntityFrameworkCore.Sqlite",
            "Microsoft.EntityFrameworkCore.SqlServer",
            "Npgsql.EntityFrameworkCore.PostgreSQL");

        // Dynamically add source files from folders
        string[] folders = { "BaseModel", "DataAccess", "Fields", "IoCContainer", "Models", "ModelForms" };
        foreach (var folder in folders)
        {
            AddFilesFromFolder(root, @$"..\GenericForm\{folder}");
        }

        // Define build target
        var target = root.AddTarget("Build");
        var task = target.AddTask("Csc");
        task.SetParameter("Sources", "@(Compile)");
        task.SetParameter("OutputAssembly", "GenericForm.dll");

        // Save and build
        root.Save("DynamicGenericForm.csproj");
        BuildProject("DynamicGenericForm.csproj");
    }

    private static void AddItems(ProjectRootElement elem, string groupName, params string[] items)
    {
        var group = elem.AddItemGroup();
        foreach (var item in items)
        {
            group.AddItem(groupName, item);
        }
    }

    private static void AddFilesFromFolder(ProjectRootElement elem, string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            var group = elem.AddItemGroup();
            foreach (var file in Directory.GetFiles(folderPath, "*.cs"))
            {
                group.AddItem("Compile", file);
            }
        }
    }

    private static void BuildProject(string projectPath)
    {
        var buildRequest = new BuildRequestData(projectPath, null, null, new[] { "Build" }, null);
        var buildParameters = new BuildParameters(ProjectCollection.GlobalProjectCollection)
        {
            Loggers = new[] { new Microsoft.Build.Logging.ConsoleLogger() }
        };

        var result = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);
        Console.WriteLine(result.OverallResult == BuildResultCode.Success ? "Build succeeded!" : "Build failed.");
    }
}
}
