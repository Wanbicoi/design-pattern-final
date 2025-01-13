using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GenericForm.ModelForms
{
    public partial class products : BaseModel.List<DBContext.products>
    {
        public products(BaseApplicationDbContext<DBContext.products> dbContext) : base(dbContext)
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
            this.Text = "products";
        }
    }
}
