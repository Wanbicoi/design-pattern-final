using System.Reflection;
using System.Windows.Forms;

namespace GenericForm.Products
{
    public class NumericUpDownStrategy : IInputControlStrategy
    {
        public Control CreateControl(PropertyInfo propertyInfo)
        {
            return new NumericUpDown { Width = 200 };
        }

        public object GetValue(Control control)
        {
            return (int)((NumericUpDown)control).Value;
        }

        public void SetValue(Control control, object value)
        {
            ((NumericUpDown)control).Value = Convert.ToDecimal(value);
        }
    }
}
