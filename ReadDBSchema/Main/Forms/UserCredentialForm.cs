using GenericForm;
using ReadDBSchema;
using Generater.Generator;
namespace Main.Forms
{
    public partial class UserCredentialForm : Form
    {

        private DatabaseManagement databaseManagement { get; }

        public UserCredentialForm(DatabaseManagement databaseManagement)
        {
            InitializeComponent();
            databaseManagement = databaseManagement;
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
                GenerateModel(s, typeMapper);
            }
            GenerateModelFormMapper(dbSchema, typeMapper);

            this.Hide();
            MainWindow mainWindow = new MainWindow(databaseManagement.GetDatabaseType(), databaseManagement.GetConnectionString());
            mainWindow.Show();

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
}
