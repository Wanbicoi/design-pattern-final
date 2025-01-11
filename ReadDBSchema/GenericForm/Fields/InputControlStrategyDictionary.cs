using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericForm.Fields
{
    public static class InputControlStrategyFactory
    {
        public static IInputControlStrategy CreateStrategy(Control control)
        {
            if (control is NumericUpDown numericUpDown)
            {
                return new NumericUpDownStrategy();
            }
            else if (control is TextBox textBox)
            {
                return new TextBoxStrategy();
            }
            else
            {
                throw new ArgumentException($"Unsupported control type: {control.GetType()}");
            }
        }
    }
}
