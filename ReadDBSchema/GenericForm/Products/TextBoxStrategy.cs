using System.Reflection;
using System.Windows.Forms;

namespace GenericForm.Products
{
    public class TextBoxStrategy : IInputControlStrategy
    {
        public Control CreateControl(PropertyInfo propertyInfo)
        {
            return new TextBox { Width = 200 };
        }

        public object GetValue(Control control)
        {
            return ((TextBox)control).Text;
        }

        public void SetValue(Control control, object value)
        {
            ((TextBox)control).Text = value?.ToString();
        }
    }
}
