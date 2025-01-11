using System.Reflection;
using GenericForm.DBContext;
using GenericForm.Fields;

namespace GenericForm.BaseModel
{
    public partial class Update<T> : Form where T : class, IBaseModel, new()
    {
        private readonly T _product;
        private readonly Dictionary<PropertyInfo, IInputControlStrategy> _strategies;
        private BaseApplicationDbContext<T> _context;
        public Update(int productId, BaseApplicationDbContext<T> context)
        {
            InitializeComponent();
            _context = context;
            _strategies = new Dictionary<PropertyInfo, IInputControlStrategy>();
            //_product = DbContextHelper.GetDbSet<T>().Find(productId)!;
            _product = _context.Set().Find(productId);
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
                //DbContextHelper.Context.SaveChanges();
                _context.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}");
            }
        }
    }
}
