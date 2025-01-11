using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericForm.Fields
{
    public static class InputControlStrategyFactory
    {
        public static IInputControlStrategy CreateStrategy(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case "Int32":
                case "Decimal":
                case "Double":
                case "Single":
                    return new NumericUpDownStrategy();
                case "String":
                    return new TextBoxStrategy();
                case "DateTime":
                    return new DateTimeInputControlStrategy();
                default:
                    throw new ArgumentException($"Unsupported property type: {propertyType}");
            }
        }
    }
}
