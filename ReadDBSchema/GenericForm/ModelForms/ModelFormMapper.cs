using System;
using System.Collections.Generic;
using GenericForm.DBContext;
namespace GenericForm.ModelForms
{
    public static class ModelFormMapper
    {
        public static readonly Dictionary<string, Type> FormTypes = new Dictionary<string, Type>()
        {
            { "users", typeof(ModelForms.users) },
            { "products", typeof(ModelForms.products) },
        };

        public static readonly Dictionary<Type, Type> ModelFormMapping = new Dictionary<Type, Type>
        {
            { typeof(DBContext.users), typeof(ModelForms.users) },
            { typeof(DBContext.products), typeof(ModelForms.products) },
        };
    }
}
