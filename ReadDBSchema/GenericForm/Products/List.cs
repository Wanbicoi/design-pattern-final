using Microsoft.EntityFrameworkCore;

namespace GenericForm.Products
{
    public partial class List : Form
    {
        private readonly DbSet<Product> _dbSet;

        public List()
        {
            InitializeComponent();
            var _context = DbContextHelper.Context;
            _dbSet = _context.Set<Product>();
            _dbSet.Load();
            dataGridView.DataSource = _dbSet.Local.ToBindingList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;

            var product = _dbSet.Find(dataGridView.SelectedRows[0].Cells[0].Value);
            if (product != null)
            {
                _dbSet.Remove(product);
                dataGridView.Refresh();
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Create().ShowDialog();
            dataGridView.Refresh();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;

            var id = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            new Update(id).ShowDialog();
            dataGridView.Refresh();
        }
    }
}
