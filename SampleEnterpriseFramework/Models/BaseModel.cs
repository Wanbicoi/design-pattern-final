using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEnterpriseFramework.Models
{
    public abstract class  BaseModel
    {
        public abstract String GetTableName();

        public abstract List<String> GetColumnsName();


        // Virtual method for getting object state
        public virtual Dictionary<string, object> GetObjectState()
        {
            var state = new Dictionary<string, object>();
            foreach (var field in this.GetType().GetFields())
            {
                state.Add(field.Name, field.GetValue(this));
            }
            return state;
        }

        // Virtual method for getting attribute names
        public virtual List<string> GetAttributeNames()
        {
            var attributeNames = new List<string>();
            foreach (var field in this.GetType().GetFields())
            {
                attributeNames.Add(field.Name);
            }
            return attributeNames;
        }
    }
}
