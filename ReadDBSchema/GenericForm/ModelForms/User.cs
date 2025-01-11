namespace GenericForm.ModelForms
{
    public partial class User : BaseModel.List<DBContext.User>
    {
        public User()
        {
            InitializeComponent();
        }
    }
}
