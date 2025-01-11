using System;
using GenericForm;

public class clients : IBaseModel
{
    public Int32 id { get; set; }
    public String firstname { get; set; }
    public String lastname { get; set; }
    public String email { get; set; }
    public String address { get; set; }
    public String phone { get; set; }
    public DateTime created_at { get; set; }
    //public int ID { get; set; } // IBaseModel requirement

}
