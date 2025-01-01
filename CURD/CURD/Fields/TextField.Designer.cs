namespace CURD.Fields
{
    partial class TextField
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
            label = new Label();
            value = new TextBox();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(3, 3);
            label.Name = "label";
            label.Size = new Size(38, 15);
            label.TabIndex = 0;
            label.Text = "label1";
            // 
            // value
            // 
            value.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            value.Location = new Point(0, 21);
            value.Name = "value";
            value.Size = new Size(107, 23);
            value.TabIndex = 1;
            // 
            // TextField
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(value);
            Controls.Add(label);
            Name = "TextField";
            Size = new Size(107, 45);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private TextBox value;
    }
}
