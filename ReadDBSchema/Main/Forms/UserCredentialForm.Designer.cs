namespace Main.Forms
{
    partial class UserCredentialForm
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
            label1 = new Label();
            UsernameTextBox = new TextBox();
            label2 = new Label();
            PasswordTextBox = new TextBox();
            button1 = new Button();
            GenerateButton = new Button();
            TableComboBox = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(203, 86);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Location = new Point(315, 83);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(193, 27);
            UsernameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(203, 141);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(315, 138);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(193, 27);
            PasswordTextBox.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(356, 258);
            button1.Name = "button1";
            button1.Size = new Size(114, 29);
            button1.TabIndex = 4;
            button1.Text = "Create user";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CreateUser_Click;
            // 
            // GenerateButton
            // 
            GenerateButton.Location = new Point(356, 365);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new Size(94, 29);
            GenerateButton.TabIndex = 6;
            GenerateButton.Text = "Generate";
            GenerateButton.UseVisualStyleBackColor = true;
            GenerateButton.Click += GenerateButton_Click;
            // 
            // TableComboBox
            // 
            TableComboBox.FormattingEnabled = true;
            TableComboBox.Location = new Point(315, 195);
            TableComboBox.Name = "TableComboBox";
            TableComboBox.Size = new Size(193, 28);
            TableComboBox.TabIndex = 7;
            TableComboBox.Visible = false;
            // 
            // UserCredentialForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TableComboBox);
            Controls.Add(GenerateButton);
            Controls.Add(button1);
            Controls.Add(PasswordTextBox);
            Controls.Add(label2);
            Controls.Add(UsernameTextBox);
            Controls.Add(label1);
            Name = "UserCredentialForm";
            Text = "UserCredentialForm";
            Load += UserCredentialForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox UsernameTextBox;
        private Label label2;
        private TextBox PasswordTextBox;
        private Button button1;
        private Button GenerateButton;
        private ComboBox TableComboBox;
    }
}