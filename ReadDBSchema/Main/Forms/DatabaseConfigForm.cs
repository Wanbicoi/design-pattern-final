using Main.Forms;
using ReadDBSchema;

namespace SimpleEnterprisesFramework

{
    public partial class DatabaseConfigForm : Form
    {
        public DatabaseConfigForm()
        {
            InitializeComponent();
        }

        private void DatabaseConfigForm_Load(object sender, EventArgs e)
        {
            DatabaseTypeComboBox.DataSource = DatabaseType.GetDatabaseTypes();
        }

        private void Username_Click(object sender, EventArgs e)
        {

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void Port_Click(object sender, EventArgs e)
        {

        }

        private void ConnectionString_Click(object sender, EventArgs e)
        {

        }

        private void GenerateCode_Click(object sender, EventArgs e)
        {

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string address = AddressTextBox.Text;
            string port = PortTextBox.Text;
            string databaseName = DatabaseNameTextBox.Text;
            string databaseType = DatabaseTypeComboBox.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(databaseType))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            DatabaseManagement databaseManagement = new DatabaseManagement(username, password, address, port, databaseName, databaseType);
            
            this.Hide();
            var checkConnectionForm = new DatabaseConnection(this, username, password, address, port, databaseName, databaseType, databaseManagement);
            checkConnectionForm.Show();
        }

        private void PortNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
