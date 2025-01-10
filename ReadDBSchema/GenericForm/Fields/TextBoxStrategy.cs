using System.Windows.Forms;

namespace GenericForm.Fields
{
    public class TextBoxStrategy : IInputControlStrategy
    {
        public Control CreateControl(string name)
        {
            return new TextBox { Width = 200, Name = name };
        }

        public void SetValue(Control control, object value)
        {
            if (control is TextBox textBox)
            {
                textBox.Text = value?.ToString();
            }
        }

        public object GetValue(Control control)
        {
            if (control is TextBox textBox)
            {
                return textBox.Text;
            }
            return null;
        }
    }
}
