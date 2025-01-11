using Microsoft.EntityFrameworkCore;

namespace GenericForm.BaseModel
{
    public partial class List<T> : Form where T : class, IBaseModel, new()
    {
        private readonly DbSet<T> _dbSet;

        public List()
        {
            InitializeComponent();
            _dbSet = DbContextHelper.GetDbSet<T>();
            _dbSet.Load();
            dataGridView.DataSource = _dbSet.Local.ToBindingList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;

            var id = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            var product = _dbSet.Find(id);
            if (product != null)
            {
                _dbSet.Remove(product);
                dataGridView.Refresh();
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Create<T>().ShowDialog();
            dataGridView.Refresh();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;

            var id = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            new Update<T>(id).ShowDialog();
            dataGridView.Refresh();
        }
    }
}
