//using GenericForm.Base;
//using GenericForm.Fields;
//using System;
//using System.Linq;
//using System.Reflection;
//using System.Windows.Forms;
//using GenericForm.DBContext;

//namespace GenericForm.BaseModel
//{
//    public abstract class BaseCreatedForm<T> : Form where T : class, new()
//    {
//        protected readonly FlowLayoutPanel FlowLayoutPanel;
//        protected readonly ApplicationDbContext Context;
//        protected readonly T Entity;

//        protected BaseCreatedForm()
//        {
//            //Context = new ApplicationDbContext();
//            Context = DbContextHelper.Context;
//            Entity = new T();

//            FlowLayoutPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
//            Controls.Add(FlowLayoutPanel);

//            GenerateFields();
//        }

//        private void GenerateFields()
//        {
//            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
//                .Where(p => p.Name != "ID");

//            foreach (var property in properties)
//            {
//                var label = new Label { Text = property.Name, AutoSize = true };
//                FlowLayoutPanel.Controls.Add(label);

//                Control inputControl;
//                if (property.PropertyType == typeof(int))
//                {
//                    inputControl = new NumericUpDown { Width = 200 };
//                }
//                else if (property.PropertyType == typeof(string))
//                {
//                    inputControl = new TextBox { Width = 200 };
//                }
//                else
//                {
//                    throw new ArgumentException($"Unsupported property type: {property.PropertyType}");
//                }

//                inputControl.Name = property.Name + "Control";
//                FlowLayoutPanel.Controls.Add(inputControl);
//            }

//            var saveButton = new Button { Text = "Save", Width = 100 };
//            saveButton.Click += SaveButton_Click;
//            FlowLayoutPanel.Controls.Add(saveButton);
//        }

//        private void SaveButton_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
//                    .Where(p => p.Name != "ID");
//                var product = new T();
//                foreach (var property in properties)
//                {
//                    if (_strategies.TryGetValue(property, out var strategy))
//                    {
//                        var control = flowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
//                        if (control != null)
//                        {
//                            object value = strategy.GetValue();
//                            property.SetValue(product, value);
//                        }
//                    }
//                }

//                DbContextHelper.GetDbSet<T>().Add(product);
//                DbContextHelper.Context.SaveChanges();
//                Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error creating product: {ex.Message}");
//            }
//        }

//        protected virtual void SaveEntity()
//        {
//            Context.Set<T>().Add(Entity);
//            Context.SaveChanges();
//            Close();
//        }
//    }
//}

//namespace GenericForm.Products
//{
//    public partial class CreateProductForm : BaseCreatedForm<Product>
//    {
//        public CreateProductForm()
//        {
//            Text = "Create Product";
//        }
//    }
//}
