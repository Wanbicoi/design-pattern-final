
using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;

namespace GenericForm.ModelForms
{
    public partial class Product : BaseModel.List<DBContext.Product>
    {
        public Product(BaseApplicationDbContext<DBContext.Product> dbContext) : base(dbContext)
        {
            InitializeComponent();
        }
    }
}
