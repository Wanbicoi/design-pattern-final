using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GenericForm.Fields
{
    public interface IInputControlStrategy
    {
        Control CreateControl(PropertyInfo propertyInfo);
        object GetValue();
        void SetValue(object value);
    }
}
