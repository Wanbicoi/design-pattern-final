using System;
using System.Reflection;
using System.Windows.Forms;

namespace GenericForm.Fields
{
    public class TextBoxStrategy : IInputControlStrategy
    {
        private TextBox? textBox;
        private PropertyInfo? propertyInfo;

        public Control CreateControl(PropertyInfo propertyInfo)
        {
            textBox = new TextBox { Width = 200 };
            this.propertyInfo = propertyInfo;
            return textBox;
        }

        public object GetValue()
        {
            return textBox?.Text ?? "";
        }

        public void SetValue(object value)
        {
            try
            {
                textBox!.Text = value?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting value to string: {ex.Message}");
                // Handle the exception appropriately (e.g., display an error message, use a default value)
            }
        }
    }
}
