using System;
using GenericForm;
using System.ComponentModel.DataAnnotations;

namespace GenericForm.DBContext
{
public class users : IBaseModel
{
[Key]
    public Int32 id { get; set; }
    public String username { get; set; }
    public String email { get; set; }
}
}
