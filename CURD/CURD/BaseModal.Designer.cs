namespace CURD
{
    partial class BaseModal
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer2 = new SplitContainer();
            dgModal = new DataGridView();
            btnUnSelect = new Button();
            fpFields = new FlowLayoutPanel();
            fieldName = new Fields.TextField();
            fieldAge = new Fields.NumberField();
            btnCreate = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgModal).BeginInit();
            fpFields.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(dgModal);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(btnUnSelect);
            splitContainer2.Panel2.Controls.Add(fpFields);
            splitContainer2.Panel2.Controls.Add(btnCreate);
            splitContainer2.Panel2.Controls.Add(btnDelete);
            splitContainer2.Size = new Size(677, 497);
            splitContainer2.SplitterDistance = 339;
            splitContainer2.TabIndex = 1;
            // 
            // dgModal
            // 
            dgModal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgModal.Dock = DockStyle.Fill;
            dgModal.Location = new Point(0, 0);
            dgModal.Name = "dgModal";
            dgModal.ReadOnly = true;
            dgModal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgModal.Size = new Size(339, 497);
            dgModal.TabIndex = 0;
            dgModal.SelectionChanged += dgModal_SelectionChanged;
            // 
            // btnUnSelect
            // 
            btnUnSelect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnUnSelect.Location = new Point(54, 459);
            btnUnSelect.Name = "btnUnSelect";
            btnUnSelect.Size = new Size(100, 23);
            btnUnSelect.TabIndex = 3;
            btnUnSelect.Text = "Clear selection";
            btnUnSelect.UseVisualStyleBackColor = true;
            btnUnSelect.Click += btnUnSelect_Click;
            // 
            // fpFields
            // 
            fpFields.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fpFields.Controls.Add(fieldName);
            fpFields.Controls.Add(fieldAge);
            fpFields.FlowDirection = FlowDirection.TopDown;
            fpFields.Location = new Point(3, 3);
            fpFields.Name = "fpFields";
            fpFields.Size = new Size(328, 439);
            fpFields.TabIndex = 2;
            // 
            // fieldName
            // 
            fieldName.Location = new Point(3, 3);
            fieldName.Name = "fieldName";
            fieldName.Size = new Size(107, 45);
            fieldName.TabIndex = 1;
            // 
            // fieldAge
            // 
            fieldAge.AutoSize = true;
            fieldAge.Location = new Point(3, 54);
            fieldAge.Name = "fieldAge";
            fieldAge.Size = new Size(150, 45);
            fieldAge.TabIndex = 2;
            // 
            // btnCreate
            // 
            btnCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreate.Location = new Point(241, 459);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 1;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDelete.Location = new Point(160, 459);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 0;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // BaseModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer2);
            Name = "BaseModal";
            Size = new Size(677, 497);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgModal).EndInit();
            fpFields.ResumeLayout(false);
            fpFields.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer2;
        private Button btnCreate;
        private Button btnDelete;
        private DataGridView dgModal;
        private FlowLayoutPanel fpFields;
        private Fields.NumberField numberField1;
        private Fields.TextField fieldName;
        private Fields.NumberField fieldAge;
        private Button btnUnSelect;
    }
}
