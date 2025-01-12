using System;
using System.ComponentModel.DataAnnotations;
using GenericForm;

namespace GenericForm.DBContext
{
public class doctor : IBaseModel
{
    [Key]
    public Int32 id { get; set; }
    public String firstname { get; set; }
    public String lastname { get; set; }
    public String email { get; set; }
    public String hospital { get; set; }
    public Boolean male { get; set; }
    public DateTime created_at { get; set; }

    }
}
