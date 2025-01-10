using System.Reflection;
using System.Windows.Forms;
using GenericForm.Fields;

namespace GenericForm.Products
{
    public partial class Update : Form
    {
        private readonly ApplicationDbContext _context;
        private readonly Product _product;

        public Update(int productId)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _product = _context.Products.Find(productId);
            GenerateForm();
            LoadData();
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

        private void LoadData()
        {
            var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                var control = flowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
                if (control != null)
                {
                    var strategy = InputControlStrategyFactory.CreateStrategy(control);
                    strategy.SetValue(control, property.GetValue(_product)!);
                }
            }
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
                    property.SetValue(_product, strategy.GetValue(control));
                }
            }
            _context.SaveChanges();
            Close();
        }
    }
}
