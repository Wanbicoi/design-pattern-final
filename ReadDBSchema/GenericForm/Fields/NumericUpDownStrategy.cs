using System;
using System.Reflection;
using System.Windows.Forms;

namespace GenericForm.Fields
{
    public class NumericUpDownStrategy : IInputControlStrategy
    {
        private NumericUpDown? numericUpDown;
        private PropertyInfo? propertyInfo;

        public Control CreateControl(PropertyInfo propertyInfo)
        {
            numericUpDown = new NumericUpDown { Width = 200 };
            this.propertyInfo = propertyInfo;
            return numericUpDown;
        }

        public object GetValue()
        {
            return (int)(numericUpDown?.Value ?? 0);
        }

        public void SetValue(object value)
        {
            if (value != null)
            {
                try
                {
                    numericUpDown!.Value = Convert.ToDecimal(value);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error converting value to decimal: {ex.Message}");
                    // Handle the exception appropriately (e.g., display an error message, use a default value)
                }
            }
        }
    }
}
