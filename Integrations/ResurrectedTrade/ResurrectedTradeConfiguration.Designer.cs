namespace MapAssist.Integrations.ResurrectedTrade
{
    partial class ResurrectedTradeConfiguration
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
            this.StatusLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.ViewOnlineButton = new System.Windows.Forms.Button();
            this.checkBoxAutoLogin = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLabel.Location = new System.Drawing.Point(3, 10);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(351, 47);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(9, 66);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(41, 12);
            this.UsernameLabel.TabIndex = 1;
            this.UsernameLabel.Text = "用户名";
            // 
            // Username
            // 
            this.Username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Username.Location = new System.Drawing.Point(51, 63);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(208, 21);
            this.Username.TabIndex = 0;
            this.Username.TextChanged += new System.EventHandler(this.Inputs_TextChanged);
            this.Username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Inputs_KeyPress);
            // 
            // LoginButton
            // 
            this.LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginButton.Enabled = false;
            this.LoginButton.Location = new System.Drawing.Point(265, 62);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 21);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "登录";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegisterButton.Location = new System.Drawing.Point(265, 87);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(75, 21);
            this.RegisterButton.TabIndex = 3;
            this.RegisterButton.Text = "注册";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // Password
            // 
            this.Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Password.Location = new System.Drawing.Point(51, 88);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(208, 21);
            this.Password.TabIndex = 1;
            this.Password.UseSystemPasswordChar = true;
            this.Password.TextChanged += new System.EventHandler(this.Inputs_TextChanged);
            this.Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Inputs_KeyPress);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(9, 90);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(29, 12);
            this.PasswordLabel.TabIndex = 5;
            this.PasswordLabel.Text = "密码";
            // 
            // LogoutButton
            // 
            this.LogoutButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LogoutButton.Location = new System.Drawing.Point(74, 115);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(75, 21);
            this.LogoutButton.TabIndex = 4;
            this.LogoutButton.Text = "登出";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Visible = false;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // ViewOnlineButton
            // 
            this.ViewOnlineButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ViewOnlineButton.Location = new System.Drawing.Point(152, 115);
            this.ViewOnlineButton.Name = "ViewOnlineButton";
            this.ViewOnlineButton.Size = new System.Drawing.Size(75, 21);
            this.ViewOnlineButton.TabIndex = 6;
            this.ViewOnlineButton.Text = "在线查看";
            this.ViewOnlineButton.UseVisualStyleBackColor = true;
            this.ViewOnlineButton.Visible = false;
            this.ViewOnlineButton.Click += new System.EventHandler(this.ViewOnlineButton_Click);
            // 
            // checkBoxAutoLogin
            // 
            this.checkBoxAutoLogin.AutoSize = true;
            this.checkBoxAutoLogin.Location = new System.Drawing.Point(265, 120);
            this.checkBoxAutoLogin.Name = "checkBoxAutoLogin";
            this.checkBoxAutoLogin.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoLogin.TabIndex = 7;
            this.checkBoxAutoLogin.Text = "自动登录";
            this.checkBoxAutoLogin.UseVisualStyleBackColor = true;
            this.checkBoxAutoLogin.Visible = false;
            this.checkBoxAutoLogin.CheckedChanged += new System.EventHandler(this.checkBoxAutoLogin_CheckedChanged);
            // 
            // ResurrectedTradeConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.checkBoxAutoLogin);
            this.Controls.Add(this.ViewOnlineButton);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.StatusLabel);
            this.Name = "ResurrectedTradeConfiguration";
            this.Size = new System.Drawing.Size(358, 294);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.Button ViewOnlineButton;
        private System.Windows.Forms.CheckBox checkBoxAutoLogin;
    }
}
