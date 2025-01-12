using System;
using System.Collections.Generic;
using GenericForm.DBContext;
namespace GenericForm.ModelForms
{
    public static class ModelFormMapper
    {
        public static readonly Dictionary<string, Type> FormTypes = new Dictionary<string, Type>()
        {
            { "clients", typeof(ModelForms.clients) },
            { "doctor", typeof(ModelForms.doctor) },
            { "School", typeof(ModelForms.School) },
            { "Products", typeof(ModelForms.Products) },
        };

        public static readonly Dictionary<Type, Type> ModelFormMapping = new Dictionary<Type, Type>
        {
            { typeof(DBContext.clients), typeof(ModelForms.clients) },
            { typeof(DBContext.doctor), typeof(ModelForms.doctor) },
            { typeof(DBContext.School), typeof(ModelForms.School) },
            { typeof(DBContext.Products), typeof(ModelForms.Products) },
        };
    }
}
