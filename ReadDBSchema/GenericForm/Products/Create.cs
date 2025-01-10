using System.Reflection;
using System.Windows.Forms;
using GenericForm.Products.Fields;

namespace GenericForm.Products
{
    public partial class Create : Form
    {
        private readonly ApplicationDbContext _context;
        private readonly Product _product;

        public Create()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _product = new Product();
            GenerateForm();
        }

        private void GenerateForm()
        {
            var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                var label = new Label { Text = property.Name, AutoSize = true };
                flowLayoutPanel.Controls.Add(label);

                Control inputControl;
                if (property.PropertyType == typeof(int))
                {
                    inputControl = new NumericUpDown { Width = 200 };
                }
                else if (property.PropertyType == typeof(string))
                {
                    inputControl = new TextBox { Width = 200 };
                }
                else
                {
                    throw new ArgumentException($"Unsupported property type: {property.PropertyType}");
                }
                inputControl.Name = property.Name + "Control";
                flowLayoutPanel.Controls.Add(inputControl);
            }

            var saveButton = new Button { Text = "Save", Width = 100 };
            saveButton.Click += SaveButton_Click!;
            flowLayoutPanel.Controls.Add(saveButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                var control = flowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
                if (control != null)
                {
                    var strategy = InputControlStrategyFactory.CreateStrategy(control);
                    object value = strategy.GetValue(control);

                    property.SetValue(_product, value);
                }
            }

            _context.Products.Add(_product);
            _context.SaveChanges();
            Close();
        }
    }
}
