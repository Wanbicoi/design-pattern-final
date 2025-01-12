using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericForm.Fields
{
    public static class InputControlStrategyFactory
    {
        public static IInputControlStrategy CreateStrategy(Type propertyType)
        {
            Type actualType = propertyType;
            // Check if the type is nullable and extract the underlying type
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                actualType = Nullable.GetUnderlyingType(propertyType);
            }
            switch (actualType.FullName)
            {
                case "System.Int32":
                case "System.Decimal":
                case "System.Double":
                case "System.Single":
                    return new NumericUpDownStrategy();

                case "System.String":
                    return new TextBoxStrategy();

                case "System.DateTime":
                    return new DateTimeInputControlStrategy();

                case "System.Boolean":
                    return new CheckboxBooleanStrategy();

                default:
                    throw new ArgumentException($"Unsupported property type: {actualType.FullName}");
            }
        }
    }
}
