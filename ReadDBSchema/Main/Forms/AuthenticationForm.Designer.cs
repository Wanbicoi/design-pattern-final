namespace Main.Forms
{
    partial class AuthenticationForm
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
            ChangePasswordLinkLabel = new LinkLabel();
            Password = new Label();
            UsernameTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            AccessTableButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(180, 106);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // ChangePasswordLinkLabel
            // 
            ChangePasswordLinkLabel.AutoSize = true;
            ChangePasswordLinkLabel.Location = new Point(428, 214);
            ChangePasswordLinkLabel.Name = "ChangePasswordLinkLabel";
            ChangePasswordLinkLabel.Size = new Size(126, 20);
            ChangePasswordLinkLabel.TabIndex = 1;
            ChangePasswordLinkLabel.TabStop = true;
            ChangePasswordLinkLabel.Text = "Change password";
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Location = new Point(180, 171);
            Password.Name = "Password";
            Password.Size = new Size(70, 20);
            Password.TabIndex = 2;
            Password.Text = "Password";
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Location = new Point(279, 106);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(275, 27);
            UsernameTextBox.TabIndex = 3;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(279, 171);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(275, 27);
            PasswordTextBox.TabIndex = 4;
            // 
            // AccessTableButton
            // 
            AccessTableButton.Location = new Point(320, 265);
            AccessTableButton.Name = "AccessTableButton";
            AccessTableButton.Size = new Size(127, 29);
            AccessTableButton.TabIndex = 5;
            AccessTableButton.Text = "Access Table";
            AccessTableButton.UseVisualStyleBackColor = true;
            // 
            // AuthenticationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(AccessTableButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(UsernameTextBox);
            Controls.Add(Password);
            Controls.Add(ChangePasswordLinkLabel);
            Controls.Add(label1);
            Name = "AuthenticationForm";
            Text = "AuthenticationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private LinkLabel ChangePasswordLinkLabel;
        private Label Password;
        private TextBox UsernameTextBox;
        private TextBox PasswordTextBox;
        private Button AccessTableButton;
    }
}