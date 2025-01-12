namespace SampleEnterpriseFramework
{
    partial class CreateEditForm
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
            lbTitle = new Label();
            label2 = new Label();
            lbId = new Label();
            label3 = new Label();
            tbFirstName = new TextBox();
            tbLastName = new TextBox();
            label4 = new Label();
            tbEmail = new TextBox();
            label5 = new Label();
            tbPhone = new TextBox();
            label6 = new Label();
            tbAddress = new TextBox();
            label7 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(12, 9);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(763, 37);
            lbTitle.TabIndex = 0;
            lbTitle.Text = "Create Client";
            lbTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(94, 112);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 1;
            label2.Text = "Client ID";
            label2.Click += label2_Click;
            // 
            // lbId
            // 
            lbId.AutoSize = true;
            lbId.Location = new Point(263, 112);
            lbId.Name = "lbId";
            lbId.Size = new Size(0, 20);
            lbId.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(94, 148);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 3;
            label3.Text = "First Name";
            label3.Click += label3_Click;
            // 
            // tbFirstName
            // 
            tbFirstName.Location = new Point(263, 145);
            tbFirstName.Name = "tbFirstName";
            tbFirstName.Size = new Size(430, 27);
            tbFirstName.TabIndex = 4;
            // 
            // tbLastName
            // 
            tbLastName.Location = new Point(263, 194);
            tbLastName.Name = "tbLastName";
            tbLastName.Size = new Size(430, 27);
            tbLastName.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(94, 197);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 5;
            label4.Text = "Last Name";
            label4.Click += label4_Click;
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(263, 238);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(430, 27);
            tbEmail.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(94, 241);
            label5.Name = "label5";
            label5.Size = new Size(46, 20);
            label5.TabIndex = 7;
            label5.Text = "Email";
            // 
            // tbPhone
            // 
            tbPhone.Location = new Point(263, 285);
            tbPhone.Name = "tbPhone";
            tbPhone.Size = new Size(430, 27);
            tbPhone.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(94, 288);
            label6.Name = "label6";
            label6.Size = new Size(50, 20);
            label6.TabIndex = 9;
            label6.Text = "Phone";
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(263, 334);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(430, 27);
            tbAddress.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(94, 337);
            label7.Name = "label7";
            label7.Size = new Size(62, 20);
            label7.TabIndex = 11;
            label7.Text = "Address";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(263, 394);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 13;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(388, 394);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // CreateEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(787, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(tbAddress);
            Controls.Add(label7);
            Controls.Add(tbPhone);
            Controls.Add(label6);
            Controls.Add(tbEmail);
            Controls.Add(label5);
            Controls.Add(tbLastName);
            Controls.Add(label4);
            Controls.Add(tbFirstName);
            Controls.Add(label3);
            Controls.Add(lbId);
            Controls.Add(label2);
            Controls.Add(lbTitle);
            Name = "CreateEditForm";
            Text = "Create Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitle;
        private Label label2;
        private Label lbId;
        private Label label3;
        private TextBox tbFirstName;
        private TextBox tbLastName;
        private Label label4;
        private TextBox tbEmail;
        private Label label5;
        private TextBox tbPhone;
        private Label label6;
        private TextBox tbAddress;
        private Label label7;
        private Button btnSave;
        private Button btnCancel;
    }
}