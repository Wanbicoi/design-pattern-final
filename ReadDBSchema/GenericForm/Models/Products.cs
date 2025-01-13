using System;
using GenericForm;
using System.ComponentModel.DataAnnotations;

namespace GenericForm.DBContext
{
public class products : IBaseModel
{
[Key]
    public Int32 id { get; set; }
    public String product_name { get; set; }
    public Decimal price { get; set; }
}
}
