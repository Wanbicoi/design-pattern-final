using System.Windows.Forms;

namespace GenericForm.Fields
{
    public class NumericUpDownStrategy : IInputControlStrategy
    {
        public Control CreateControl(string name)
        {
            return new NumericUpDown { Width = 200, Name = name };
        }

        public void SetValue(Control control, object value)
        {
            if (control is NumericUpDown numericUpDown && value != null)
            {
                numericUpDown.Value = (int)value;
            }
        }
        public object GetValue(Control control)
        {
            if (control is NumericUpDown numericUpDown)
            {
                return numericUpDown.Value;
            }
            return null;
        }
    }
}
