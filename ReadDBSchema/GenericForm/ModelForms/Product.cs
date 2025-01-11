
namespace GenericForm.ModelForms
{
    public partial class Product : BaseModel.List<DBContext.Product>
    {
        public Product()
        {
            InitializeComponent();
        }
    }
}
