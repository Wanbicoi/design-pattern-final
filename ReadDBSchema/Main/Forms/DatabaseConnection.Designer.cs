namespace Main.Forms
{
    partial class DatabaseConnection
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GenerateButton = new Button();
            label1 = new Label();
            ConnectionStringTextBox = new TextBox();
            CheckConnectionButton = new Button();
            PreviousButton = new Button();
            SuspendLayout();
            // 
            // GenerateButton
            // 
            GenerateButton.Location = new Point(344, 244);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new Size(94, 29);
            GenerateButton.TabIndex = 0;
            GenerateButton.Text = "Generate";
            GenerateButton.UseVisualStyleBackColor = true;
            GenerateButton.Visible = false;
            GenerateButton.Click += Generate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 63);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 1;
            label1.Text = "Connection string";
            // 
            // ConnectionStringTextBox
            // 
            ConnectionStringTextBox.Location = new Point(184, 63);
            ConnectionStringTextBox.Name = "ConnectionStringTextBox";
            ConnectionStringTextBox.Size = new Size(411, 27);
            ConnectionStringTextBox.TabIndex = 2;
            ConnectionStringTextBox.ReadOnly = true;
            // 
            // CheckConnectionButton
            // 
            CheckConnectionButton.Location = new Point(620, 63);
            CheckConnectionButton.Name = "CheckConnectionButton";
            CheckConnectionButton.Size = new Size(146, 29);
            CheckConnectionButton.TabIndex = 3;
            CheckConnectionButton.Text = "Check connection";
            CheckConnectionButton.UseVisualStyleBackColor = true;
            CheckConnectionButton.Click += CheckConnection_Click;
            // 
            // PreviousButton
            // 
            PreviousButton.Location = new Point(53, 329);
            PreviousButton.Name = "PreviousButton";
            PreviousButton.Size = new Size(94, 29);
            PreviousButton.TabIndex = 4;
            PreviousButton.Text = "Previous";
            PreviousButton.UseVisualStyleBackColor = true;
            PreviousButton.Click += Previous_Click;
            // 
            // DatabaseConnection
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PreviousButton);
            Controls.Add(CheckConnectionButton);
            Controls.Add(ConnectionStringTextBox);
            Controls.Add(label1);
            Controls.Add(GenerateButton);
            Name = "DatabaseConnection";
            Text = "DatabaseConnection";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button GenerateButton;
        private Label label1;
        private TextBox ConnectionStringTextBox;
        private Button CheckConnectionButton;
        private Button PreviousButton;
    }
}