using System;
using GenericForm;
using System.ComponentModel.DataAnnotations;


namespace GenericForm.DBContext
{
    public class Products : IBaseModel
    {
        [Key]
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public Int32 Age { get; set; }

    }
}
