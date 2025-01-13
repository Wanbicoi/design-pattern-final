using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GenericForm.ModelForms
{
    public partial class users : BaseModel.List<DBContext.users>
    {
        public users(BaseApplicationDbContext<DBContext.users> dbContext) : base(dbContext)
        {
            InitializeComponent();
        }

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "users";
        }
    }
}
