namespace CURD
{
    public partial class BaseModal : UserControl
    {
        public BaseModal()
        {
            InitializeComponent();

            Reload();

            fieldName.SetLabel("Name");
            fieldAge.SetLabel("Age");
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                if(btnCreate.Text == "Create")
                {
                    var user = new User()
                    {
                        Name = fieldName.GetValue(),
                        Age = fieldAge.GetValue()
                    };
                    db.Add(user);
                }
                else
                {
                    var user = db.Users.FirstOrDefault(u => u.ID == (dgModal.CurrentRow.DataBoundItem as User)!.ID);
                    user!.Name = fieldName.GetValue();
                    user.Age = fieldAge.GetValue();
                    db.Update(user);
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
                        var user = row.DataBoundItem as User;
                        if (user != null)
                        {
                            var userInDb = db.Users.FirstOrDefault(u => u.ID == user.ID);
                            if (userInDb != null)
                            {
                                db.Users.Remove(userInDb);
                            }
                        }
                    }
                }

                db.SaveChanges();
            }

            Reload();
        }

        void Reload()
        {
            using (var db = new ApplicationDbContext())
            {
                var users = db.Users.ToList();
                dgModal.DataSource = users;
            }
        }

        private void dgModal_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgModal.CurrentRow;
            if (row == null) return;
            var user = row.DataBoundItem as User;
            if (user != null)
            {
                fieldName.SetValue(user.Name);
                fieldAge.SetValue(user.Age);

                btnCreate.Text = "Update";
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            dgModal.ClearSelection();
            btnCreate.Text = "Create";

            fieldAge.SetValue(0);
            fieldName.SetValue("");
        }
    }
}

public class Modal
{
    public int ID { get; set; }
}
