namespace CURD.Fields
{
    public partial class NumberField : UserControl, IField<int>
    {
        public NumberField()
        {
            InitializeComponent();
        }

        public int GetValue()
        {
            return (int)value.Value;
        }

        public void SetLabel(string label)
        {
            this.label.Text = label;
        }

        public void SetValue(int value)
        {
            this.value.Value = value;
        }
    }
}
