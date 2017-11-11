namespace ChessGameAttempt
{
    partial class LogInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.error3 = new System.Windows.Forms.Label();
            this.error2 = new System.Windows.Forms.Label();
            this.error1 = new System.Windows.Forms.Label();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.logInButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.createAccountButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // error3
            // 
            this.error3.AutoSize = true;
            this.error3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error3.ForeColor = System.Drawing.Color.Red;
            this.error3.Location = new System.Drawing.Point(361, 335);
            this.error3.Name = "error3";
            this.error3.Size = new System.Drawing.Size(232, 20);
            this.error3.TabIndex = 19;
            this.error3.Text = "Invalid username or password";
            this.error3.Visible = false;
            // 
            // error2
            // 
            this.error2.AutoSize = true;
            this.error2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error2.ForeColor = System.Drawing.Color.Red;
            this.error2.Location = new System.Drawing.Point(703, 309);
            this.error2.Name = "error2";
            this.error2.Size = new System.Drawing.Size(167, 20);
            this.error2.TabIndex = 18;
            this.error2.Text = "Password is required";
            this.error2.Visible = false;
            // 
            // error1
            // 
            this.error1.AutoSize = true;
            this.error1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error1.ForeColor = System.Drawing.Color.Red;
            this.error1.Location = new System.Drawing.Point(703, 225);
            this.error1.Name = "error1";
            this.error1.Size = new System.Drawing.Size(235, 20);
            this.error1.TabIndex = 17;
            this.error1.Text = "Please enter a valid username";
            this.error1.Visible = false;
            // 
            // passwordText
            // 
            this.passwordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.Location = new System.Drawing.Point(275, 298);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '•';
            this.passwordText.Size = new System.Drawing.Size(422, 34);
            this.passwordText.TabIndex = 16;
            this.passwordText.TextChanged += new System.EventHandler(this.passwordText_TextChanged);
            // 
            // usernameText
            // 
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.Location = new System.Drawing.Point(275, 215);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(422, 34);
            this.usernameText.TabIndex = 15;
            this.usernameText.TextChanged += new System.EventHandler(this.usernameText_TextChanged);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(416, 266);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(126, 29);
            this.PasswordLabel.TabIndex = 14;
            this.PasswordLabel.Text = "Password:";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.Location = new System.Drawing.Point(416, 182);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(130, 29);
            this.UserLabel.TabIndex = 13;
            this.UserLabel.Text = "Username:";
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(816, 371);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(133, 52);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "Exit";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // logInButton
            // 
            this.logInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logInButton.Location = new System.Drawing.Point(37, 371);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(133, 52);
            this.logInButton.TabIndex = 11;
            this.logInButton.Text = "Log In";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.logInButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(37, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(912, 161);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // createAccountButton
            // 
            this.createAccountButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createAccountButton.Location = new System.Drawing.Point(381, 371);
            this.createAccountButton.Name = "createAccountButton";
            this.createAccountButton.Size = new System.Drawing.Size(194, 52);
            this.createAccountButton.TabIndex = 20;
            this.createAccountButton.Text = "Create account";
            this.createAccountButton.UseVisualStyleBackColor = true;
            this.createAccountButton.Click += new System.EventHandler(this.createAccountButton_Click);
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.createAccountButton;
            this.ClientSize = new System.Drawing.Size(987, 441);
            this.Controls.Add(this.createAccountButton);
            this.Controls.Add(this.error3);
            this.Controls.Add(this.error2);
            this.Controls.Add(this.error1);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "LogInForm";
            this.Text = "Log In - Multiplayer Chess";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label error3;
        private System.Windows.Forms.Label error2;
        private System.Windows.Forms.Label error1;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button createAccountButton;
    }
}