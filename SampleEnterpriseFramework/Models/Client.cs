using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEnterpriseFramework.Models
{
    public class Client : BaseModel 
    {
        public int id;
        public string firstName = "";
        public string lastName = "";
        public string email = "";
        public string phone = "";
        public string address = "";
        public string createdAt = "";
        public Client() { }

        public new static List<string> GetAttributeNames()
        {
            return new List<string>
            {
                "id",
                "Name",
                "email",
                "phone",
                "address",
                "createdAt"
            };
        }

        public override List<string> GetColumnsName()
        {
            return new List<string> {

                "id",
                "firstName",
                "lastName",
                "email",
                "phone",
                "address",
                "createdAt"
            };
        }

        public override Dictionary<string, object> GetObjectState()
        {
            return new Dictionary<string, object>
                    {
                        { "id", id },
                        { "Name", firstName + " " + lastName },
                        { "email", email },
                        { "phone", phone },
                        { "address", address },
                        { "createdAt", createdAt }
                    };
        }

        public override string GetTableName()
        {
            return "clients";
        }
    }
}
