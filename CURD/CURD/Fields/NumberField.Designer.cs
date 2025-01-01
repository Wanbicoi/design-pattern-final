namespace CURD.Fields
{
    partial class NumberField
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
            value = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)value).BeginInit();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(3, 1);
            label.Name = "label";
            label.Size = new Size(38, 15);
            label.TabIndex = 2;
            label.Text = "label1";
            // 
            // value
            // 
            value.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            value.ImeMode = ImeMode.NoControl;
            value.Location = new Point(3, 19);
            value.Name = "value";
            value.Size = new Size(147, 23);
            value.TabIndex = 3;
            value.ThousandsSeparator = true;
            // 
            // NumberField
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(value);
            Controls.Add(label);
            Name = "NumberField";
            Size = new Size(150, 45);
            ((System.ComponentModel.ISupportInitialize)value).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label;
        private NumericUpDown value;
    }
}
