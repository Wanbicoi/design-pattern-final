using Microsoft.EntityFrameworkCore;

namespace GenericForm
{
    public partial class GenericForm<T> : Form where T : class
    {
        public GenericForm()
        {
            InitializeComponent();

            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();

            // Load data and bind to the DataGridView
            LoadData();
        }

    }
    public partial class GenericForm<T> : GenericForm<User>
    {
        public GenericForm()
        {
            InitializeComponent();
        }

    }
}

