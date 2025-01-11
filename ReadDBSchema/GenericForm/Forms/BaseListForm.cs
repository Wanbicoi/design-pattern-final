using GenericForm.Fields;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using GenericForm.Base;


namespace GenericForm.Base
{
    public abstract class BaseListForm<T> : Form where T : class
    {
        //private readonly DbSet<T> _dbSet;
        //private readonly ProductDbContext _context;
        private readonly ClientsDbContext _context;
        //private readonly ApplicationDbContext _context;
        protected DataGridView DataGridView;
        private ContextMenuStrip ContextMenuStrip;
        private ToolStripMenuItem CreateToolStripMenuItem;
        private ToolStripMenuItem UpdateToolStripMenuItem;
        private ToolStripMenuItem DeleteToolStripMenuItem;

        protected BaseListForm(string databaseType, string connectionString)
        {
            _context = new ClientsDbContext(databaseType, connectionString);
            //_context = new ApplicationDbContext();
            //_dbSet = _context.Set<T>();

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            var components = new System.ComponentModel.Container();

            DataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            ContextMenuStrip = new ContextMenuStrip(components);

            CreateToolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Create"
            };
            CreateToolStripMenuItem.Click += (s, e) => CreateNewItem();

            UpdateToolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Update"
            };
            UpdateToolStripMenuItem.Click += (s, e) => UpdateSelectedItem();

            DeleteToolStripMenuItem = new ToolStripMenuItem
            {
                Text = "Delete"
            };
            DeleteToolStripMenuItem.Click += (s, e) => DeleteSelectedItem();

            ContextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                CreateToolStripMenuItem,
                UpdateToolStripMenuItem,
                DeleteToolStripMenuItem
            });

            DataGridView.ContextMenuStrip = ContextMenuStrip;

            Controls.Add(DataGridView);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Text = typeof(T).Name + " List";
        }

        private void LoadData()
        {
            //_dbSet.Load();
            // Use Set<T>() to get the DbSet dynamically
            var dbSet = _context.Set<T>();
            dbSet.Load();

            // Bind to the DataGridView
            DataGridView.DataSource = dbSet.Local.ToBindingList();
        }

        protected virtual void DeleteSelectedItem()
        {
            if (DataGridView.SelectedRows.Count == 0)
                return;

            var dbSet = _context.Set<T>();
            var entity = dbSet.Find(DataGridView.SelectedRows[0].Cells[0].Value);
            if (entity != null)
            {
                dbSet.Remove(entity);
                _context.SaveChanges();
                LoadData();
            }
        }

        protected virtual void CreateNewItem()
        {
            var createForm = CreateForm();
            createForm.ShowDialog();
            LoadData();
        }

        protected virtual void UpdateSelectedItem()
        {
            if (DataGridView.SelectedRows.Count == 0)
                return;

            var id = (int)DataGridView.SelectedRows[0].Cells[0].Value;
            var updateForm = UpdateForm(id);
            updateForm.ShowDialog();
            LoadData();
        }

        protected abstract Form CreateForm();

        protected abstract Form UpdateForm(int id);
    }
}

namespace GenericForm.Products
{
    public partial class ProductListForm : BaseListForm<Product>
    {
        public ProductListForm(string databaseType, string connectionString) : base(databaseType, connectionString)
        {
            Text = "Product List";
        }

        protected override Form CreateForm()
        {
            return new Create();
        }

        protected override Form UpdateForm(int id)
        {
            return new Update(id);
        }
    }


    public partial class ClientsListForm : BaseListForm<clients>
    {
        public ClientsListForm(string databaseType, string connectionString) : base(databaseType, connectionString)
        {
            Text = "Product List";
        }

        protected override Form CreateForm()
        {
            return new Create();
        }

        protected override Form UpdateForm(int id)
        {
            return new Update(id);
        }
    }
}
