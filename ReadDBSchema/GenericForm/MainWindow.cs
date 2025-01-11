using GenericForm.DBContext;
using GenericForm.ModelForms;
using System.ComponentModel;

namespace GenericForm
{
    public partial class MainWindow : Form
    {
        private SimpleIoCContainer _container;
        private readonly Dictionary<string, Type> formTypes = new Dictionary<string, Type>()
        {
            { "Users", typeof(ModelForms.User) },
            { "Products", typeof(ModelForms.Product) },
            { "Clients", typeof(ModelForms.Clients) },
        };

        public MainWindow(string databaseType, string connectionString)
        {
            InitializeComponent();
            RegistDependencies(databaseType, connectionString);
        }

        private void RegistDependencies(string databaseType, string connectionString)
        {
            _container = new SimpleIoCContainer();

            _container.Register<BaseApplicationDbContext<clients>>(() =>
               new BaseApplicationDbContext<clients>(databaseType, connectionString));

            _container.Register<Clients>(() =>
            {
                var dbContext = _container.Resolve<BaseApplicationDbContext<clients>>();
                return new Clients(dbContext);
            });

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(formTypes.Keys.ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && formTypes.TryGetValue(listBox1.SelectedItem.ToString()!, out Type formType))
            {
                //Form form = (Form)Activator.CreateInstance(formType)!;
                //form.ShowDialog();
                if (formType != null)
                {
                    // Resolve the form dynamically using the type
                    var form = _container.Resolve(formType) as Form;
                    if (form != null)
                    {
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("The selected form could not be created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The selected form type is not registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
