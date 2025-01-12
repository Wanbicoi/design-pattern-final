using SampleEnterpriseFramework.Models;
using SampleEnterpriseFramework.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleEnterpriseFramework
{
    public partial class CreateEditForm : Form
    {
        public CreateEditForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }
        private int clientId = 0;
        public void EditClient(Client client)
        {
            this.Text = "Edit Client";
            this.lbTitle.Text = "Edit Client";

            this.lbId.Text = "" + client.id;
            this.tbFirstName.Text = client.firstName;
            this.tbLastName.Text = client.lastName ;
            this.tbEmail.Text = client.email;
            this.tbPhone.Text = client.phone;
            this.tbAddress.Text = client.address;

            this.clientId = client.id;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.id = this.clientId;
            client.firstName = this.tbFirstName.Text;
            client.lastName = this.tbLastName.Text;
            client.email = this.tbEmail.Text;
            client.phone = this.tbPhone.Text;
            client.address = this.tbAddress.Text;

            var repo = new ClientRepository();

            if (client.id == 0)
            {
                repo.CreateClient(client);

            }
            else
            {
                repo.UpdateClient(client);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
