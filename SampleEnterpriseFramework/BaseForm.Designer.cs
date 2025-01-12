namespace SampleEnterpriseFramework
{
    partial class BaseForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        protected virtual void InitializeComponent()
        {
            label1 = new Label();
            addBtnClient = new Button();
            btnDelete = new Button();
            btnEditClient = new Button();
            clientsTable = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)clientsTable).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(814, 43);
            label1.TabIndex = 0;
            label1.Text = "List of Clients";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // addBtnClient
            // 
            addBtnClient.Location = new Point(12, 83);
            addBtnClient.Name = "addBtnClient";
            addBtnClient.Size = new Size(94, 29);
            addBtnClient.TabIndex = 1;
            addBtnClient.Text = "Add Client";
            addBtnClient.UseVisualStyleBackColor = true;
            addBtnClient.Click += addBtnClient_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(700, 83);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(126, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEditClient
            // 
            btnEditClient.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditClient.Location = new Point(587, 83);
            btnEditClient.Name = "btnEditClient";
            btnEditClient.Size = new Size(107, 29);
            btnEditClient.TabIndex = 3;
            btnEditClient.Text = "Edit Client";
            btnEditClient.UseVisualStyleBackColor = true;
            btnEditClient.Click += btnEditClient_Click;
            // 
            // clientsTable
            // 
            clientsTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            clientsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            clientsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            clientsTable.Location = new Point(12, 118);
            clientsTable.MultiSelect = false;
            clientsTable.Name = "clientsTable";
            clientsTable.RowHeadersVisible = false;
            clientsTable.RowHeadersWidth = 51;
            clientsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            clientsTable.Size = new Size(814, 345);
            clientsTable.TabIndex = 4;
            clientsTable.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(838, 477);
            Controls.Add(clientsTable);
            Controls.Add(btnEditClient);
            Controls.Add(btnDelete);
            Controls.Add(addBtnClient);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Client Manager";
            ((System.ComponentModel.ISupportInitialize)clientsTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button addBtnClient;
        private Button btnDelete;
        private Button btnEditClient;
        private DataGridView clientsTable;
        public virtual void SetUpForm() { }
    }

}
