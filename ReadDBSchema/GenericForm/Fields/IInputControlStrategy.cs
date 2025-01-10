using System.Windows.Forms;

namespace GenericForm.Fields
{
    public interface IInputControlStrategy
    {
        Control CreateControl(string name);
        void SetValue(Control control, object value);
        object GetValue(Control control);
    }
}
