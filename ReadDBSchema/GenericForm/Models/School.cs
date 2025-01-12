using System;
using GenericForm;
using System.ComponentModel.DataAnnotations;

namespace GenericForm.DBContext
{
public class School : IBaseModel
{
    public Int32 SchoolID { get; set; }
    public String SchoolName { get; set; }
    public String Address { get; set; }
    public String City { get; set; }
    public String State { get; set; }
    public String ZipCode { get; set; }
    public Int32? EstablishedYear { get; set; }
    public Boolean? IsPublic { get; set; }
}
}
