namespace CURD.Fields
{
    public partial class TextField : UserControl, IField<string>
    {
        public TextField()
        {
            InitializeComponent();
        }
         
        public string GetValue()
        {
            return this.value.Text;
        }

        public void SetLabel(string label)
        {
            this.label.Text = label;
        }

        public void SetValue(string value)
        {
            this.value.Text = value;
        }
    }
}
