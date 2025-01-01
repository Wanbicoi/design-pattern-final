namespace CURD
{
    public partial class ProductForm : Form
    {
        string[] modalNames = { "Product", "Task" };
        public ProductForm()
        {
            InitializeComponent();

            foreach (string name in modalNames)
            {
                lbModalNames.Items.Add(name);
            }
        }

        private void lbModalNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(new BaseModal());
        }
    }
}
