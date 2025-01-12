using GenericForm.DBContext;
using GenericForm.ModelForms;
using System.ComponentModel;

namespace GenericForm
{
    public partial class MainWindow : Form
    {

        private readonly Dictionary<string, Type> _formTypes = ModelFormMapper.FormTypes;
        private readonly Dictionary<Type, Type> _modelFormMapping = ModelFormMapper.ModelFormMapping;

        public MainWindow(string databaseType, string connectionString)
        {
            InitializeComponent();
            DynamicRegistDependencies(databaseType, connectionString);
        }


        private void DynamicRegistDependencies(string databaseType, string connectionString)
        {

            foreach (var mapping in _modelFormMapping)
            {
                var modelType = mapping.Key;
                var clientType = mapping.Value;

                // Register the DbContext with the generic method
                var dbContextRegistrationMethod = typeof(SimpleIoCContainer)
                    .GetMethod(nameof(SimpleIoCContainer.Register))
                    ?.MakeGenericMethod(typeof(BaseApplicationDbContext<>).MakeGenericType(modelType));

                dbContextRegistrationMethod?.Invoke(SimpleIoCContainer.Instance, new object[]
                {
                    (Func<object>)(() => Activator.CreateInstance(typeof(BaseApplicationDbContext<>).MakeGenericType(modelType), databaseType, connectionString)),
                    null
                });

                // Register the service with the generic method
                var serviceRegistrationMethod = typeof(SimpleIoCContainer)
                    .GetMethod(nameof(SimpleIoCContainer.Register))
                    ?.MakeGenericMethod(clientType);

                serviceRegistrationMethod?.Invoke(SimpleIoCContainer.Instance, new object[]
                {
                    (Func<object>)(() =>
                    {
                        var dbContext = SimpleIoCContainer.Instance.Resolve(typeof(BaseApplicationDbContext<>).MakeGenericType(modelType));
                        return Activator.CreateInstance(clientType, dbContext);
                    }),
                    null
                });
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(_formTypes.Keys.ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && _formTypes.TryGetValue(listBox1.SelectedItem.ToString()!, out Type formType))
            {
  
                if (formType != null)
                {

                    var form = SimpleIoCContainer.Instance.Resolve(formType) as Form;
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
