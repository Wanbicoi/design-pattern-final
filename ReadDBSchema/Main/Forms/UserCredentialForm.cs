using GenericForm;
using ReadDBSchema;
using Generater.Generator;
using System.Diagnostics;
namespace Main.Forms
{
    public partial class UserCredentialForm : Form
    {

        private DatabaseManagement databaseManagement { get; }

        public UserCredentialForm(string connectionString, string databaseType)
        {
            InitializeComponent();
            databaseManagement = new DatabaseManagement(connectionString, databaseType);
        }

        private void CreateUser_Click(object sender, EventArgs e)
        {

            // get username, password
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (username == null || password == null)
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (!Validator.IsValidUsername(username))
            {
                MessageBox.Show("Invalid username");
                return;
            }

            if (!Validator.IsValidPassword(password))
            {
                MessageBox.Show("Invalid password");
                return;
            }

            string tableName = TableComboBox.SelectedItem.ToString();

            if (databaseManagement.CreateUser(username, password, tableName))
            {
                MessageBox.Show("User created successfully");
            }
            else
            {
                MessageBox.Show("User creation failed");
                return;
            }

            // after create user, empty username + password
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
        }

        private void UserCredentialForm_Load(object sender, EventArgs e)
        {
            DatabaseSchema schema = databaseManagement.GetDatabaseSchema();
            // get tables
            List<TableSchema> tables = schema.GetTables();
            List<string> tableNames = new List<string>();

            foreach (TableSchema table in tables)
            {
                tableNames.Add(table.GetTableName());
            }

            TableComboBox.DataSource = tableNames;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            DatabaseSchema dbSchema = databaseManagement.GetDatabaseSchema();
            IDatabaseTypeMapper typeMapper = databaseManagement.GetDatabaseTypeMapper();
            List<TableSchema> tableSchemas = dbSchema.GetTables();
            
            foreach (TableSchema s in tableSchemas)
            {
                GenerateModel(s, typeMapper); // class generation
            }
            GenerateModelFormMapper(dbSchema, typeMapper); 

            this.Hide();

            string connectionString = databaseManagement.GetConnectionString();
            string databaseType = databaseManagement.GetDatabaseType();

            AuthenticationForm authenticationForm = new AuthenticationForm(connectionString, databaseType);
            authenticationForm.Show();

            //MainWindow mainWindow = new MainWindow(connectionString, databaseType);
            //mainWindow.Show();

        }

        private void GenerateModel(TableSchema tableSchema, IDatabaseTypeMapper databaseTypeMapper)
        {
            // Generate the model class
            var modelGenerator = new ModelGenerator(databaseTypeMapper);
            modelGenerator.GenerateCode(tableSchema);

            // Generate the ModelForm class
            var modelFormGenerator = new ModelFormGenerator();
            modelFormGenerator.GenerateCode(tableSchema);

        }

        private void GenerateModelFormMapper(DatabaseSchema databaseSchema, IDatabaseTypeMapper databaseTypeMapper)
        {
            // Generate the ModelFormMapper class after generating Model and ModelForm
            var modelFormMapperGenerator = new ModelFormMapperGenerator();
            modelFormMapperGenerator.GenerateCode(databaseSchema); // Generate ModelFormMapper
        }

    }


    public static class ProjectGenerator
    {
        public static void CreateProject(string projectName, string destinationPath, string sourceLibraryPath)
        {
            // Ensure the destination path exists
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            // Create the project using dotnet CLI
            RunCommand("dotnet", $"new winforms -n {projectName} -o {destinationPath}");

            // Copy the GenerateForm library to the new project
            string targetLibraryPath = Path.Combine(destinationPath, projectName, "GenerateForm");
            CopyDirectory(sourceLibraryPath, targetLibraryPath);

            // Add the library reference to the project file
            string projectFilePath = Path.Combine(destinationPath, projectName, $"{projectName}.csproj");
            AddLibraryReference(projectFilePath, targetLibraryPath);
        }

        private static void RunCommand(string command, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Command '{command} {arguments}' failed with exit code {process.ExitCode}");
            }
        }

        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(sourceDir))
            {
                throw new DirectoryNotFoundException($"Source directory not found: {sourceDir}");
            }

            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                var destDirectory = Path.Combine(destinationDir, Path.GetFileName(directory));
                CopyDirectory(directory, destDirectory);
            }
        }

        private static void AddLibraryReference(string projectFilePath, string libraryPath)
        {
            var projectFileContent = File.ReadAllText(projectFilePath);
            var reference = $@"
  <ItemGroup>
    <Compile Include=""{libraryPath}\**\*.cs"" />
  </ItemGroup>";

            var insertIndex = projectFileContent.IndexOf("</Project>");
            projectFileContent = projectFileContent.Insert(insertIndex, reference);
            File.WriteAllText(projectFilePath, projectFileContent);
        }

        public static void SaveDatabaseConfig(string filePath, string connectionString, string databaseType)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(connectionString);
                writer.WriteLine(databaseType);
            }
        }

        public static (string connectionString, string databaseType) LoadDatabaseConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file at {filePath} was not found.");
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string connectionString = reader.ReadLine();
                string databaseType = reader.ReadLine();
                return (connectionString, databaseType);
            }
        }
    }
}
