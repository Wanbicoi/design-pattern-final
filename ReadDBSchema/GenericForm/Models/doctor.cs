using System;
using GenericForm;

public class doctor : IBaseModel
{
    public Int32 id { get; set; }
    public String firstname { get; set; }
    public String lastname { get; set; }
    public String email { get; set; }
    public String hospital { get; set; }
    public Boolean male { get; set; }
    public DateTime created_at { get; set; }
    public int ID { get; set; } // IBaseModel requirement


    // Implement TableName property from IBaseModel interface
    public string TableName { get; set; } = "Doctor"; // Set table name to "Users"
}
