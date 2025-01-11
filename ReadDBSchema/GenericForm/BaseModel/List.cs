﻿using GenericForm.DBContext;
using GenericForm.ModelForms;
using Microsoft.EntityFrameworkCore;

namespace GenericForm.BaseModel
{
    public partial class List<T> : Form where T : class, IBaseModel, new()
    {
        private readonly DbSet<T> _dbSet;
        private BaseApplicationDbContext<T> _context;

        public List(BaseApplicationDbContext<T> context)
        {
            InitializeComponent();
            //_dbSet = DbContextHelper.GetDbSet<T>();
            _context = context;
            _dbSet = context.Set();
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
            new Create<T>(_context).ShowDialog();
            dataGridView.Refresh();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;

            var id = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            new Update<T>(id, _context).ShowDialog();
            dataGridView.Refresh();
        }
    }
}
