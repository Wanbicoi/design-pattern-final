namespace CURD
{
    partial class ProductForm
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
        private void InitializeComponent()
        {
            mainContainer = new SplitContainer();
            btnSignOut = new Button();
            lbUserName = new Label();
            splitContainer = new SplitContainer();
            lbModalNames = new ListBox();
            ((System.ComponentModel.ISupportInitialize)mainContainer).BeginInit();
            mainContainer.Panel1.SuspendLayout();
            mainContainer.Panel2.SuspendLayout();
            mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mainContainer
            // 
            mainContainer.Dock = DockStyle.Fill;
            mainContainer.Location = new Point(0, 0);
            mainContainer.Name = "mainContainer";
            mainContainer.Orientation = Orientation.Horizontal;
            // 
            // mainContainer.Panel1
            // 
            mainContainer.Panel1.Controls.Add(btnSignOut);
            mainContainer.Panel1.Controls.Add(lbUserName);
            mainContainer.Panel1.RightToLeft = RightToLeft.No;
            // 
            // mainContainer.Panel2
            // 
            mainContainer.Panel2.Controls.Add(splitContainer);
            mainContainer.Panel2.RightToLeft = RightToLeft.No;
            mainContainer.RightToLeft = RightToLeft.No;
            mainContainer.Size = new Size(875, 568);
            mainContainer.SplitterDistance = 58;
            mainContainer.TabIndex = 0;
            // 
            // btnSignOut
            // 
            btnSignOut.Anchor = AnchorStyles.Right;
            btnSignOut.Location = new Point(777, 18);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Size = new Size(75, 23);
            btnSignOut.TabIndex = 2;
            btnSignOut.Text = "Sign Out";
            btnSignOut.UseVisualStyleBackColor = true;
            // 
            // lbUserName
            // 
            lbUserName.AutoSize = true;
            lbUserName.Location = new Point(724, 22);
            lbUserName.Name = "lbUserName";
            lbUserName.Size = new Size(38, 15);
            lbUserName.TabIndex = 1;
            lbUserName.Text = "label1";
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(lbModalNames);
            splitContainer.Size = new Size(875, 506);
            splitContainer.SplitterDistance = 191;
            splitContainer.TabIndex = 0;
            // 
            // lbModalNames
            // 
            lbModalNames.Dock = DockStyle.Fill;
            lbModalNames.FormattingEnabled = true;
            lbModalNames.ItemHeight = 15;
            lbModalNames.Location = new Point(0, 0);
            lbModalNames.Name = "lbModalNames";
            lbModalNames.Size = new Size(191, 506);
            lbModalNames.TabIndex = 0;
            lbModalNames.SelectedIndexChanged += lbModalNames_SelectedIndexChanged;
            // 
            // ProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 568);
            Controls.Add(mainContainer);
            MaximizeBox = false;
            Name = "ProductForm";
            ShowIcon = false;
            Text = "Main window";
            mainContainer.Panel1.ResumeLayout(false);
            mainContainer.Panel1.PerformLayout();
            mainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainContainer).EndInit();
            mainContainer.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer mainContainer;
        private Label lbUserName;
        private Button btnSignOut;
        private SplitContainer splitContainer;
        private ListBox lbModalNames;
    }
}
