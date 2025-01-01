namespace CURD
{
    public partial class BaseModal : UserControl
    {
        public BaseModal()
        {
            InitializeComponent();

            Reload();

            fieldName.SetLabel("Name"); // Change here
            fieldAge.SetLabel("Age"); // Change here
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                if(btnCreate.Text == "Create")
                {
                    var model = new User() // Change here
                    {
                        Name = fieldName.GetValue(), // Change here
                        Age = fieldAge.GetValue() // Change here
                    };
                    db.Add(model);
                }
                else
                {
                    var model = db.Users.FirstOrDefault(u => u.ID == (dgModal.CurrentRow.DataBoundItem as User)!.ID); // Change here
                    model!.Name = fieldName.GetValue(); // Change here
                    model.Age = fieldAge.GetValue(); // Change here
                    db.Update(model);
                }
                db.SaveChanges();
            }
            Reload();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgModal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least 1 item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var db = new ApplicationDbContext())
            {
                foreach (DataGridViewRow row in dgModal.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        var user = row.DataBoundItem as User; // Change here
                        if (user != null)
                        {
                            var userInDb = db.Users.FirstOrDefault(u => u.ID == user.ID); // Change here
                            if (userInDb != null)
                            {
                                db.Users.Remove(userInDb); // Change here
                            }
                        }
                    }
                }

                db.SaveChanges(); // Change here
            }

            Reload(); // Change here
        }

        void Reload()
        {
            using (var db = new ApplicationDbContext()) // Change here
            {
                var users = db.Users.ToList(); // Change here
                dgModal.DataSource = users; // Change here
            }
        }

        private void dgModal_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgModal.CurrentRow;
            if (row == null) return;
            var user = row.DataBoundItem as User; // Change here
            if (user != null)
            {
                fieldName.SetValue(user.Name); // Change here
                fieldAge.SetValue(user.Age); // Change here

                btnCreate.Text = "Update";
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            dgModal.ClearSelection();
            btnCreate.Text = "Create";

            fieldAge.SetValue(0); // Change here
            fieldName.SetValue(""); // Change here
        }
    }
}
