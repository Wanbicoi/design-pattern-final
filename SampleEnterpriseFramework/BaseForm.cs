using SampleEnterpriseFramework.Repositories;
using System.Data;

namespace SampleEnterpriseFramework
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();

            ReadClients();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ReadClients()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Phone");
            dataTable.Columns.Add("Adress");
            dataTable.Columns.Add("Date");


            var repo = new ClientRepository();
            var clients = repo.GetClients();

            foreach (var client in clients)
            {
                var row = dataTable.NewRow();

                row["ID"] = client.id;
                row["Name"] = client.firstName + client.lastName;
                row["Email"] = client.email;
                row["Phone"] = client.phone;
                row["Adress"] = client.address;
                row["Date"] = client.createdAt;

                dataTable.Rows.Add(row);

            }

            this.clientsTable.DataSource = dataTable;
        }

        private void addBtnClient_Click(object sender, EventArgs e)
        {
            CreateEditForm form = new CreateEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadClients();
            }
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            var val = this.clientsTable.SelectedRows[0].Cells[0].Value.ToString();
            if (val == null || val.Length == 0)
            {
                return;
            }

            int clientId = int.Parse(val);
            var repo = new ClientRepository();
            var client = repo.GetClientById(clientId);

            if (client == null) return;


            CreateEditForm form = new CreateEditForm();
            form.EditClient(client);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadClients();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var val = this.clientsTable.SelectedRows[0].Cells[0].Value.ToString();
            if (val == null || val.Length == 0)
            {
                return;
            }

            int clientId = int.Parse(val);

            DialogResult dialogResult = MessageBox.Show("Are you sure to delete this client?", "Delete Client", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No) return;


            var repo = new ClientRepository();
            repo.DeleteClient(clientId);

            ReadClients();
        }
    }
}
