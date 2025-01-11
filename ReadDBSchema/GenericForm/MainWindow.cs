using System;
namespace GenericForm
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Users");
            listBox1.Items.Add("Products");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new GenericForm.GenericForm<User>().ShowDialog();
        }
    }
}
