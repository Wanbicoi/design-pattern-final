using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericForm.Fields
{
    public class CheckboxBooleanStrategy : IInputControlStrategy
    {
        private CheckBox _checkBox;

        // Creates a CheckBox control for the boolean property
        public Control CreateControl(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType != typeof(bool) && propertyInfo.PropertyType != typeof(bool?))
            {
                throw new ArgumentException($"Property {propertyInfo.Name} is not of type bool or nullable bool.");
            }

            _checkBox = new CheckBox
            {
                Text = propertyInfo.Name,
                AutoSize = true
            };

            return _checkBox;
        }

        // Retrieves the value from the CheckBox control
        public object GetValue()
        {
            return _checkBox?.Checked ?? false;
        }

        // Sets the value of the CheckBox control
        public void SetValue(object value)
        {
            if (_checkBox == null)
            {
                throw new InvalidOperationException("CreateControl must be called before setting a value.");
            }

            if (value is bool boolValue)
            {
                _checkBox.Checked = boolValue;
            }
            else if (value == null && _checkBox.CheckState == CheckState.Indeterminate)
            {
                _checkBox.CheckState = CheckState.Indeterminate;
            }
            else
            {
                throw new ArgumentException($"Value must be of type bool or null.");
            }
        }
    } 
}
