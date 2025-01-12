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
using SimpleEnterprisesFramework;

namespace Main.Forms
{
    public partial class DatabaseConnection : Form
    {
        private DatabaseConfigForm ConfigForm { get; }
        DatabaseManagement databaseManagement;
        public DatabaseConnection(DatabaseConfigForm configForm, DatabaseManagement databaseManagement)
        {
            InitializeComponent();
            ConfigForm = configForm;
            this.databaseManagement = databaseManagement;

            ConnectionStringTextBox.Text = databaseManagement.GetConnectionString();
        }

        private void CheckConnection_Click(object sender, EventArgs e)
        {
            if (databaseManagement.CheckConnection())
            {
                MessageBox.Show("Connection successful");
                NextButton.Visible = true;
                
                // debug
                DatabaseSchema schema = databaseManagement.GetDatabaseSchema();
                List<TableSchema> tableSchemas = schema.GetTables();

                string tmp = "";
                foreach (TableSchema table in tableSchemas)
                {
                    tmp += table.ToString();
                }

                MessageBox.Show(tmp);
            }
            else
            {
                MessageBox.Show("Connection failed");
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfigForm.Show();
        }

        private void NextButton_ClickHandler(object sender, EventArgs e)
        {
            UserCredentialForm userCredentialForm = new UserCredentialForm(this.databaseManagement);

            this.Hide();
            userCredentialForm.Show();
        }
    }
}
