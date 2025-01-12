using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GenericForm.ModelForms
{
    public partial class doctor : BaseModel.List<DBContext.doctor>
    {
        public doctor(BaseApplicationDbContext<DBContext.doctor> dbContext) : base(dbContext)
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
            //this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "doctor";
        }
    }
}
