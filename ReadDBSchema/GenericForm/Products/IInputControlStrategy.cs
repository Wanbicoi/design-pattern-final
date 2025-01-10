using System.Reflection;

namespace GenericForm.Products
{
    public interface IInputControlStrategy
    {
        Control CreateControl(PropertyInfo propertyInfo);
        object GetValue(Control control);
        void SetValue(Control control, object value);
    }
}
