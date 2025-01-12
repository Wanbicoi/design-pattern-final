using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReadDBSchema;

namespace Main.Forms
{
    public partial class UserCredentialForm : Form
    {

        private DatabaseManagement DatabaseManagement { get; }

        public UserCredentialForm(DatabaseManagement databaseManagement)
        {
            InitializeComponent();
            DatabaseManagement = databaseManagement;
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

            if (DatabaseManagement.CreateUser(username, password, tableName))
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
            DatabaseSchema schema = DatabaseManagement.GetDatabaseSchema();
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

        }
    }
}
