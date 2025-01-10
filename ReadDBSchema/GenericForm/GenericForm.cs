using Microsoft.EntityFrameworkCore;

namespace GenericForm
{
    public partial class GenericForm<T> : Form where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericForm()
        {
            InitializeComponent();

            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();

            // Load data and bind to the DataGridView
            LoadData();
        }

        private void LoadData()
        {
            // Load data from the database and bind to the DataGridView
            _dbSet.Load();
            //dataGridView.DataSource = _dbSet.Local.ToBindingList();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Save changes to the database
                _context.SaveChanges();
                MessageBox.Show("Changes saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dispose the DbContext when the form is closed
            _context.Dispose();
            base.OnFormClosing(e);
        }
    }
}