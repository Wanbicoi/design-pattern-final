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
    public partial class AuthenticationForm : Form
    {
        private DatabaseManagement DatabaseManagement;
        public AuthenticationForm(DatabaseManagement databaseManagement)
        {
            DatabaseManagement = databaseManagement;
            InitializeComponent();
        }

        private void AccessTableButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a username and password");
                return;
            }

            if (Validator.IsValidUsername(username))
            {
                MessageBox.Show("Invalid username");
                return;
            }

            if (Validator.IsValidPassword(password))
            {
                MessageBox.Show("Invalid password");
                return;
            }

            string tableName;
            bool loginSuccess = DatabaseManagement.LoginAndGetPermission(username, password, out tableName);
            if (loginSuccess)
            {
                MessageBox.Show("Login successful. You have access to table: " + tableName);
                // TODO: Open CRUD form with access to table

                return;
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.");
            }
        }
    }
}
