using System;
using System.Reflection;
using System.Windows.Forms;

namespace GenericForm.Fields
{
    public class DateTimeInputControlStrategy : IInputControlStrategy
    {
        private DateTimePicker _dateTimePicker;

        public Control CreateControl(PropertyInfo propertyInfo)
        {
            // Create a DateTimePicker control
            _dateTimePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short, // Display date in short format (e.g., MM/dd/yyyy)
                Value = DateTime.Now // Default to current date if no value is set
            };
            return _dateTimePicker;
        }

        public object GetValue()
        {
            // Return the selected value as a DateTime
            return _dateTimePicker.Value;
        }

        public void SetValue(object value)
        {
            if (value is DateTime dateTime)
            {
                // Set the DateTimePicker value if it's a valid DateTime object
                _dateTimePicker.Value = dateTime;
            }
            else
            {
                // Optionally handle invalid value types, for example, set a default value or throw an exception
                _dateTimePicker.Value = DateTime.Now;
            }
        }
    }
}
