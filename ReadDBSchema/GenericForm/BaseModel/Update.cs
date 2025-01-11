using System.Reflection;
using GenericForm.Fields;

namespace GenericForm.Products
{
    public partial class Update<T> : Form where T : class, IBaseModel, new()
    {
        private readonly T _product;
        private readonly Dictionary<PropertyInfo, IInputControlStrategy> _strategies;

        public Update(int productId)
        {
            InitializeComponent();
            _strategies = new Dictionary<PropertyInfo, IInputControlStrategy>();
            _product = DbContextHelper.GetDbSet<T>().Find(productId)!;
            GenerateForm();
            LoadData();
        }

        private void GenerateForm()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                var label = new Label { Text = property.Name, AutoSize = true };
                flowLayoutPanel.Controls.Add(label);

                var strategy = InputControlStrategyFactory.CreateStrategy(property.PropertyType);
                Control inputControl = strategy.CreateControl(property);
                inputControl.Name = property.Name + "Control";
                flowLayoutPanel.Controls.Add(inputControl);
                _strategies.Add(property, strategy);
            }

            var saveButton = new Button { Text = "Save", Width = 100 };
            saveButton.Click += SaveButton_Click!;
            flowLayoutPanel.Controls.Add(saveButton);
        }

        private void LoadData()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                if (_strategies.TryGetValue(property, out var strategy))
                {
                    var control = flowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
                    if (control != null)
                    {
                        strategy.SetValue(property.GetValue(_product)!);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name != "ID");

                foreach (var property in properties)
                {
                    if (_strategies.TryGetValue(property, out var strategy))
                    {
                        var control = flowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
                        if (control != null)
                        {
                            property.SetValue(_product, strategy.GetValue());
                        }
                    }
                }
                DbContextHelper.Context.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}");
            }
        }
    }
}
