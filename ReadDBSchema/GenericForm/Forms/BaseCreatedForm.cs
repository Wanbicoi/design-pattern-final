using GenericForm.Base;
using GenericForm.Fields;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GenericForm.Base
{
    public abstract class BaseCreatedForm<T> : Form where T : class, new()
    {
        protected readonly FlowLayoutPanel FlowLayoutPanel;
        protected readonly ApplicationDbContext Context;
        protected readonly T Entity;

        protected BaseCreatedForm()
        {
            Context = new ApplicationDbContext();
            Entity = new T();

            FlowLayoutPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
            Controls.Add(FlowLayoutPanel);

            GenerateFields();
        }

        private void GenerateFields()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                var label = new Label { Text = property.Name, AutoSize = true };
                FlowLayoutPanel.Controls.Add(label);

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
                FlowLayoutPanel.Controls.Add(inputControl);
            }

            var saveButton = new Button { Text = "Save", Width = 100 };
            saveButton.Click += SaveButton_Click;
            FlowLayoutPanel.Controls.Add(saveButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID");

            foreach (var property in properties)
            {
                var control = FlowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
                if (control != null)
                {
                    var strategy = InputControlStrategyFactory.CreateStrategy(control);
                    object value = strategy.GetValue(control);

                    property.SetValue(Entity, value);
                }
            }

            SaveEntity();
        }

        protected virtual void SaveEntity()
        {
            Context.Set<T>().Add(Entity);
            Context.SaveChanges();
            Close();
        }
    }
}

namespace GenericForm.Products
{
    public partial class CreateProductForm : BaseCreatedForm<Product>
    {
        public CreateProductForm()
        {
            Text = "Create Product";
        }
    }
}
