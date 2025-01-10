using System.Reflection;

namespace GenericForm.Fields
{
    public interface IInputControlStrategy
    {
        Control CreateControl(PropertyInfo propertyInfo);
        object GetValue(Control control);
        void SetValue(Control control, object value);
    }
}
