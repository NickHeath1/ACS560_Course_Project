namespace ChessGameAttempt
{
    partial class CreateUserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.passwordRequiredErrorLabel = new System.Windows.Forms.Label();
            this.usernameTakenLabel = new System.Windows.Forms.Label();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.passwordNoMatchLabel = new System.Windows.Forms.Label();
            this.confirmPasswordText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.usernameAvailableLabel = new System.Windows.Forms.Label();
            this.noUsernameErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(142, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(393, 91);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome!";
            // 
            // passwordRequiredErrorLabel
            // 
            this.passwordRequiredErrorLabel.AutoSize = true;
            this.passwordRequiredErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordRequiredErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.passwordRequiredErrorLabel.Location = new System.Drawing.Point(247, 313);
            this.passwordRequiredErrorLabel.Name = "passwordRequiredErrorLabel";
            this.passwordRequiredErrorLabel.Size = new System.Drawing.Size(167, 20);
            this.passwordRequiredErrorLabel.TabIndex = 24;
            this.passwordRequiredErrorLabel.Text = "Password is required";
            this.passwordRequiredErrorLabel.Visible = false;
            // 
            // usernameTakenLabel
            // 
            this.usernameTakenLabel.AutoSize = true;
            this.usernameTakenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameTakenLabel.ForeColor = System.Drawing.Color.Red;
            this.usernameTakenLabel.Location = new System.Drawing.Point(195, 218);
            this.usernameTakenLabel.Name = "usernameTakenLabel";
            this.usernameTakenLabel.Size = new System.Drawing.Size(287, 20);
            this.usernameTakenLabel.TabIndex = 23;
            this.usernameTakenLabel.Text = "Sorry, that username is already taken";
            this.usernameTakenLabel.Visible = false;
            // 
            // passwordText
            // 
            this.passwordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.Location = new System.Drawing.Point(126, 276);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '•';
            this.passwordText.Size = new System.Drawing.Size(422, 34);
            this.passwordText.TabIndex = 22;
            this.passwordText.TextChanged += new System.EventHandler(this.passwordText_TextChanged);
            // 
            // usernameText
            // 
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.Location = new System.Drawing.Point(126, 181);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(422, 34);
            this.usernameText.TabIndex = 21;
            this.usernameText.TextChanged += new System.EventHandler(this.usernameText_TextChanged);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(271, 244);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(126, 29);
            this.PasswordLabel.TabIndex = 20;
            this.PasswordLabel.Text = "Password:";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.Location = new System.Drawing.Point(267, 148);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(130, 29);
            this.UserLabel.TabIndex = 19;
            this.UserLabel.Text = "Username:";
            // 
            // passwordNoMatchLabel
            // 
            this.passwordNoMatchLabel.AutoSize = true;
            this.passwordNoMatchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordNoMatchLabel.ForeColor = System.Drawing.Color.Red;
            this.passwordNoMatchLabel.Location = new System.Drawing.Point(236, 414);
            this.passwordNoMatchLabel.Name = "passwordNoMatchLabel";
            this.passwordNoMatchLabel.Size = new System.Drawing.Size(194, 20);
            this.passwordNoMatchLabel.TabIndex = 27;
            this.passwordNoMatchLabel.Text = "Passwords do not match";
            this.passwordNoMatchLabel.Visible = false;
            // 
            // confirmPasswordText
            // 
            this.confirmPasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPasswordText.Location = new System.Drawing.Point(126, 377);
            this.confirmPasswordText.Name = "confirmPasswordText";
            this.confirmPasswordText.PasswordChar = '•';
            this.confirmPasswordText.Size = new System.Drawing.Size(422, 34);
            this.confirmPasswordText.TabIndex = 26;
            this.confirmPasswordText.TextChanged += new System.EventHandler(this.confirmPasswordText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(224, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 29);
            this.label3.TabIndex = 25;
            this.label3.Text = "Confirm Password:";
            // 
            // createButton
            // 
            this.createButton.Enabled = false;
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createButton.Location = new System.Drawing.Point(230, 460);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(118, 49);
            this.createButton.TabIndex = 28;
            this.createButton.Text = "Create!";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(354, 460);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(116, 49);
            this.cancelButton.TabIndex = 29;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // usernameAvailableLabel
            // 
            this.usernameAvailableLabel.AutoSize = true;
            this.usernameAvailableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameAvailableLabel.ForeColor = System.Drawing.Color.Lime;
            this.usernameAvailableLabel.Location = new System.Drawing.Point(236, 218);
            this.usernameAvailableLabel.Name = "usernameAvailableLabel";
            this.usernameAvailableLabel.Size = new System.Drawing.Size(179, 20);
            this.usernameAvailableLabel.TabIndex = 30;
            this.usernameAvailableLabel.Text = "Username is available!";
            this.usernameAvailableLabel.Visible = false;
            // 
            // noUsernameErrorLabel
            // 
            this.noUsernameErrorLabel.AutoSize = true;
            this.noUsernameErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noUsernameErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.noUsernameErrorLabel.Location = new System.Drawing.Point(244, 218);
            this.noUsernameErrorLabel.Name = "noUsernameErrorLabel";
            this.noUsernameErrorLabel.Size = new System.Drawing.Size(170, 20);
            this.noUsernameErrorLabel.TabIndex = 31;
            this.noUsernameErrorLabel.Text = "Username is required";
            this.noUsernameErrorLabel.Visible = false;
            // 
            // CreateUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 521);
            this.Controls.Add(this.noUsernameErrorLabel);
            this.Controls.Add(this.usernameAvailableLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.passwordNoMatchLabel);
            this.Controls.Add(this.confirmPasswordText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwordRequiredErrorLabel);
            this.Controls.Add(this.usernameTakenLabel);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.label1);
            this.Name = "CreateUserForm";
            this.Text = "CreateUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label passwordRequiredErrorLabel;
        private System.Windows.Forms.Label usernameTakenLabel;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label passwordNoMatchLabel;
        private System.Windows.Forms.TextBox confirmPasswordText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label usernameAvailableLabel;
        private System.Windows.Forms.Label noUsernameErrorLabel;
    }
}