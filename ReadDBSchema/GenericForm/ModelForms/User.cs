using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;

namespace GenericForm.ModelForms
{
    public partial class User : BaseModel.List<DBContext.User>
    {
        public User(BaseApplicationDbContext<DBContext.User> dbContext) : base (dbContext)
        {
            InitializeComponent();
        }
    }
}
