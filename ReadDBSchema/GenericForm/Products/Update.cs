using System.Reflection;
using GenericForm.Fields;
using Microsoft.EntityFrameworkCore; 

namespace GenericForm.Products
{
    public partial class Update : Form
    {
        private readonly ApplicationDbContext _context;
        private readonly Product _product;
        private readonly Dictionary<PropertyInfo, IInputControlStrategy> _strategies;

        public Update(int productId)
        {
            InitializeComponent();
            _strategies = new Dictionary<PropertyInfo, IInputControlStrategy>();
            _context = new ApplicationDbContext();
            _product = _context.Products.Find(productId)!;
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
            var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance)
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
            var properties = typeof(Product).GetProperties(BindingFlags.Public | BindingFlags.Instance)
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
            //_context.Entry(_product).State = EntityState.Modified; // Added this line
            _context.SaveChanges();
            Close();
        }
    }
}
