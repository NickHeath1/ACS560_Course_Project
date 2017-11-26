namespace ChessGameAttempt
{
    partial class LobbyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LobbyForm));
            this.details = new System.Windows.Forms.Button();
            this.lobbyTable = new System.Windows.Forms.DataGridView();
            this.SessionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custom = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.turnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gameTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.addStandardGame = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.oneMinBlitz = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.twoMinBlitz = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.logout = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lobbyTable)).BeginInit();
            this.SuspendLayout();
            // 
            // details
            // 
            this.details.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.details.Location = new System.Drawing.Point(488, 520);
            this.details.Margin = new System.Windows.Forms.Padding(2);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(92, 50);
            this.details.TabIndex = 4;
            this.details.Text = "View Details";
            this.details.UseVisualStyleBackColor = true;
            this.details.Click += new System.EventHandler(this.details_Click);
            // 
            // lobbyTable
            // 
            this.lobbyTable.AllowUserToAddRows = false;
            this.lobbyTable.AllowUserToDeleteRows = false;
            this.lobbyTable.AllowUserToOrderColumns = true;
            this.lobbyTable.AllowUserToResizeRows = false;
            this.lobbyTable.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lobbyTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.lobbyTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lobbyTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SessionID,
            this.custom,
            this.username,
            this.turnTime,
            this.gameTime});
            this.lobbyTable.Location = new System.Drawing.Point(152, 33);
            this.lobbyTable.Margin = new System.Windows.Forms.Padding(2);
            this.lobbyTable.MultiSelect = false;
            this.lobbyTable.Name = "lobbyTable";
            this.lobbyTable.ReadOnly = true;
            this.lobbyTable.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lobbyTable.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.lobbyTable.RowTemplate.Height = 24;
            this.lobbyTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lobbyTable.Size = new System.Drawing.Size(740, 458);
            this.lobbyTable.TabIndex = 8;
            this.lobbyTable.SelectionChanged += new System.EventHandler(this.lobbyTable_SelectionChanged);
            // 
            // SessionID
            // 
            this.SessionID.HeaderText = "SessionID";
            this.SessionID.Name = "SessionID";
            this.SessionID.ReadOnly = true;
            this.SessionID.Visible = false;
            // 
            // custom
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.NullValue = false;
            this.custom.DefaultCellStyle = dataGridViewCellStyle2;
            this.custom.HeaderText = "Custom?";
            this.custom.Name = "custom";
            this.custom.ReadOnly = true;
            this.custom.Width = 90;
            // 
            // username
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.DefaultCellStyle = dataGridViewCellStyle3;
            this.username.HeaderText = "UserName";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.Width = 380;
            // 
            // turnTime
            // 
            this.turnTime.HeaderText = "Turn Time";
            this.turnTime.Name = "turnTime";
            this.turnTime.ReadOnly = true;
            this.turnTime.Width = 125;
            // 
            // gameTime
            // 
            this.gameTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gameTime.HeaderText = "Game Time";
            this.gameTime.Name = "gameTime";
            this.gameTime.ReadOnly = true;
            // 
            // join
            // 
            this.join.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.join.Location = new System.Drawing.Point(644, 520);
            this.join.Margin = new System.Windows.Forms.Padding(2);
            this.join.Name = "join";
            this.join.Size = new System.Drawing.Size(92, 50);
            this.join.TabIndex = 9;
            this.join.Text = "Join Game";
            this.join.UseVisualStyleBackColor = true;
            this.join.Click += new System.EventHandler(this.join_Click);
            // 
            // remove
            // 
            this.remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remove.Location = new System.Drawing.Point(800, 520);
            this.remove.Margin = new System.Windows.Forms.Padding(2);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(92, 50);
            this.remove.TabIndex = 10;
            this.remove.Text = "Remove Game";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // addStandardGame
            // 
            this.addStandardGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStandardGame.Location = new System.Drawing.Point(29, 279);
            this.addStandardGame.Margin = new System.Windows.Forms.Padding(2);
            this.addStandardGame.Name = "addStandardGame";
            this.addStandardGame.Size = new System.Drawing.Size(92, 50);
            this.addStandardGame.TabIndex = 13;
            this.addStandardGame.Text = "Standard Game";
            this.addStandardGame.UseVisualStyleBackColor = true;
            this.addStandardGame.Click += new System.EventHandler(this.addStandardGame_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(29, 225);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 50);
            this.button2.TabIndex = 12;
            this.button2.Text = "Add Custom";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(29, 171);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 50);
            this.button3.TabIndex = 11;
            this.button3.Text = "Create Custom";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // oneMinBlitz
            // 
            this.oneMinBlitz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneMinBlitz.Location = new System.Drawing.Point(29, 333);
            this.oneMinBlitz.Margin = new System.Windows.Forms.Padding(2);
            this.oneMinBlitz.Name = "oneMinBlitz";
            this.oneMinBlitz.Size = new System.Drawing.Size(92, 50);
            this.oneMinBlitz.TabIndex = 14;
            this.oneMinBlitz.Text = "1\' Blitz";
            this.oneMinBlitz.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(152, 520);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(153, 50);
            this.button5.TabIndex = 15;
            this.button5.Text = "View Achievements";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // twoMinBlitz
            // 
            this.twoMinBlitz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoMinBlitz.Location = new System.Drawing.Point(29, 387);
            this.twoMinBlitz.Margin = new System.Windows.Forms.Padding(2);
            this.twoMinBlitz.Name = "twoMinBlitz";
            this.twoMinBlitz.Size = new System.Drawing.Size(92, 50);
            this.twoMinBlitz.TabIndex = 16;
            this.twoMinBlitz.Text = "2\' Blitz";
            this.twoMinBlitz.UseVisualStyleBackColor = true;
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.Location = new System.Drawing.Point(29, 441);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(92, 50);
            this.settingsButton.TabIndex = 17;
            this.settingsButton.Text = "Change Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // logout
            // 
            this.logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.Location = new System.Drawing.Point(29, 520);
            this.logout.Margin = new System.Windows.Forms.Padding(2);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(92, 50);
            this.logout.TabIndex = 18;
            this.logout.Text = "Logout";
            this.logout.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refreshButton.BackgroundImage")));
            this.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(29, 33);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(2);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(92, 92);
            this.refreshButton.TabIndex = 19;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ChessGameAttempt.Properties.Resources.Abstract_Blue_Water_Backgrounds_800x600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(921, 595);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.logout);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.twoMinBlitz);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.oneMinBlitz);
            this.Controls.Add(this.addStandardGame);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.join);
            this.Controls.Add(this.lobbyTable);
            this.Controls.Add(this.details);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LobbyForm";
            this.Text = "LobbyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LobbyForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.lobbyTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button details;
        private System.Windows.Forms.DataGridView lobbyTable;
        private System.Windows.Forms.Button join;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button addStandardGame;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button oneMinBlitz;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button twoMinBlitz;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn custom;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn turnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameTime;
        private System.Windows.Forms.Button refreshButton;
    }
}