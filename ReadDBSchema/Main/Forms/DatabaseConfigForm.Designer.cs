namespace SimpleEnterprisesFramework
{
    partial class DatabaseConfigForm
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
            LabelUsername = new Label();
            PasswordLabel = new Label();
            UsernameTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            DatabaseTypeComboBox = new ComboBox();
            DatabaseTypeLabel = new Label();
            AddressLabel = new Label();
            AddressTextBox = new TextBox();
            PortLabel = new Label();
            DatabaseNameLabel = new Label();
            DatabaseNameTextBox = new TextBox();
            NextButton = new Button();
            PortTextBox = new TextBox();
            SuspendLayout();
            // 
            // LabelUsername
            // 
            LabelUsername.AutoSize = true;
            LabelUsername.Location = new Point(208, 27);
            LabelUsername.Name = "LabelUsername";
            LabelUsername.Size = new Size(75, 20);
            LabelUsername.TabIndex = 0;
            LabelUsername.Text = "Username";
            LabelUsername.Click += Username_Click;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new Point(208, 88);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(70, 20);
            PasswordLabel.TabIndex = 1;
            PasswordLabel.Text = "Password";
            PasswordLabel.Click += Password_Click;
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Location = new Point(420, 27);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(125, 27);
            UsernameTextBox.TabIndex = 2;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(420, 88);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(125, 27);
            PasswordTextBox.TabIndex = 3;
            // 
            // DatabaseTypeComboBox
            // 
            DatabaseTypeComboBox.FormattingEnabled = true;
            DatabaseTypeComboBox.Location = new Point(420, 152);
            DatabaseTypeComboBox.Name = "DatabaseTypeComboBox";
            DatabaseTypeComboBox.Size = new Size(125, 28);
            DatabaseTypeComboBox.TabIndex = 6;
            // 
            // DatabaseTypeLabel
            // 
            DatabaseTypeLabel.AutoSize = true;
            DatabaseTypeLabel.Location = new Point(208, 155);
            DatabaseTypeLabel.Name = "DatabaseTypeLabel";
            DatabaseTypeLabel.Size = new Size(107, 20);
            DatabaseTypeLabel.TabIndex = 7;
            DatabaseTypeLabel.Text = "Database Type";
            // 
            // AddressLabel
            // 
            AddressLabel.AutoSize = true;
            AddressLabel.Location = new Point(208, 217);
            AddressLabel.Name = "AddressLabel";
            AddressLabel.Size = new Size(62, 20);
            AddressLabel.TabIndex = 8;
            AddressLabel.Text = "Address";
            // 
            // AddressTextBox
            // 
            AddressTextBox.Location = new Point(420, 210);
            AddressTextBox.Name = "AddressTextBox";
            AddressTextBox.Size = new Size(125, 27);
            AddressTextBox.TabIndex = 9;
            // 
            // PortLabel
            // 
            PortLabel.AutoSize = true;
            PortLabel.Location = new Point(208, 268);
            PortLabel.Name = "PortLabel";
            PortLabel.Size = new Size(35, 20);
            PortLabel.TabIndex = 10;
            PortLabel.Text = "Port";
            PortLabel.Click += Port_Click;
            // 
            // DatabaseNameLabel
            // 
            DatabaseNameLabel.AutoSize = true;
            DatabaseNameLabel.Location = new Point(208, 309);
            DatabaseNameLabel.Name = "DatabaseNameLabel";
            DatabaseNameLabel.Size = new Size(120, 20);
            DatabaseNameLabel.TabIndex = 11;
            DatabaseNameLabel.Text = "Database Name ";
            // 
            // DatabaseNameTextBox
            // 
            DatabaseNameTextBox.Location = new Point(420, 309);
            DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            DatabaseNameTextBox.Size = new Size(125, 27);
            DatabaseNameTextBox.TabIndex = 14;
            // 
            // NextButton
            // 
            NextButton.Location = new Point(690, 378);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(94, 29);
            NextButton.TabIndex = 15;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(420, 261);
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(125, 27);
            PortTextBox.TabIndex = 16;
            // 
            // DatabaseConfigForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 450);
            Controls.Add(PortTextBox);
            Controls.Add(NextButton);
            Controls.Add(DatabaseNameTextBox);
            Controls.Add(DatabaseNameLabel);
            Controls.Add(PortLabel);
            Controls.Add(AddressTextBox);
            Controls.Add(AddressLabel);
            Controls.Add(DatabaseTypeLabel);
            Controls.Add(DatabaseTypeComboBox);
            Controls.Add(PasswordTextBox);
            Controls.Add(UsernameTextBox);
            Controls.Add(PasswordLabel);
            Controls.Add(LabelUsername);
            Name = "DatabaseConfigForm";
            Text = "Form1";
            Load += DatabaseConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LabelUsername;
        private Label PasswordLabel;
        private TextBox UsernameTextBox;
        private TextBox PasswordTextBox;
        private ComboBox DatabaseTypeComboBox;
        private Label DatabaseTypeLabel;
        private Label AddressLabel;
        private TextBox AddressTextBox;
        private Label PortLabel;
        private Label DatabaseNameLabel;
        private TextBox DatabaseNameTextBox;
        private Button NextButton;
        private TextBox PortTextBox;
    }
}
