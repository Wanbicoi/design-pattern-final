using GenericForm;
using ReadDBSchema;

namespace Main.Forms
{
    public partial class AuthenticationForm : Form
    {
        private string connectionString { get; }
        private string databaseType { get; }
        public AuthenticationForm(string connectionString, string databaseType)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.databaseType = databaseType;
        }

        private void AccessTableButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();

            MessageBox.Show("Username: " + username + " Password: " + password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a username and password");
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

            string tableName;
            
            DatabaseManagement dbManagement = new DatabaseManagement(connectionString, databaseType);

            bool loginSuccess = dbManagement.LoginAndGetPermission(username, password, out tableName);
            if (loginSuccess)
            {
                MessageBox.Show("Login successful");
                MainWindow mainWindow = new MainWindow(connectionString, databaseType);
                mainWindow.Show();
                return;
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.");
            }

           
        }
    }
}
