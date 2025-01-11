using GenericForm.ModelForms;

namespace GenericForm
{
    public partial class MainWindow : Form
    {
        private readonly Dictionary<string, Type> formTypes = new Dictionary<string, Type>()
        {
            { "Users", typeof(User) },
            { "Products", typeof(Product) }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(formTypes.Keys.ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && formTypes.TryGetValue(listBox1.SelectedItem.ToString()!, out Type formType))
            {
                Form form = (Form)Activator.CreateInstance(formType)!;
                form.ShowDialog();
            }
        }
    }
}
