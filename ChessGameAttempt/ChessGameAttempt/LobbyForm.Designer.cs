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
            this.GameID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.addStandardGame = new System.Windows.Forms.Button();
            this.addCustomButton = new System.Windows.Forms.Button();
            this.createCustomGameButton = new System.Windows.Forms.Button();
            this.oneMinBlitz = new System.Windows.Forms.Button();
            this.viewAchievementsButton = new System.Windows.Forms.Button();
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
            this.gameTime,
            this.GameID});
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
            // GameID
            // 
            this.GameID.HeaderText = "GameID";
            this.GameID.Name = "GameID";
            this.GameID.ReadOnly = true;
            this.GameID.Visible = false;
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
            // addCustomButton
            // 
            this.addCustomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCustomButton.Location = new System.Drawing.Point(29, 225);
            this.addCustomButton.Margin = new System.Windows.Forms.Padding(2);
            this.addCustomButton.Name = "addCustomButton";
            this.addCustomButton.Size = new System.Drawing.Size(92, 50);
            this.addCustomButton.TabIndex = 12;
            this.addCustomButton.Text = "Add Custom";
            this.addCustomButton.UseVisualStyleBackColor = true;
            this.addCustomButton.Click += new System.EventHandler(this.addCustomButton_Click);
            // 
            // createCustomGameButton
            // 
            this.createCustomGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createCustomGameButton.Location = new System.Drawing.Point(29, 171);
            this.createCustomGameButton.Margin = new System.Windows.Forms.Padding(2);
            this.createCustomGameButton.Name = "createCustomGameButton";
            this.createCustomGameButton.Size = new System.Drawing.Size(92, 50);
            this.createCustomGameButton.TabIndex = 11;
            this.createCustomGameButton.Text = "Create Custom";
            this.createCustomGameButton.UseVisualStyleBackColor = true;
            this.createCustomGameButton.Click += new System.EventHandler(this.createCustomGameButton_Click);
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
            this.oneMinBlitz.Click += new System.EventHandler(this.oneMinBlitz_Click);
            // 
            // viewAchievementsButton
            // 
            this.viewAchievementsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewAchievementsButton.Location = new System.Drawing.Point(152, 520);
            this.viewAchievementsButton.Margin = new System.Windows.Forms.Padding(2);
            this.viewAchievementsButton.Name = "viewAchievementsButton";
            this.viewAchievementsButton.Size = new System.Drawing.Size(153, 50);
            this.viewAchievementsButton.TabIndex = 15;
            this.viewAchievementsButton.Text = "View Achievements";
            this.viewAchievementsButton.UseVisualStyleBackColor = true;
            this.viewAchievementsButton.Click += new System.EventHandler(this.viewAchievementsButton_Click);
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
            this.twoMinBlitz.Click += new System.EventHandler(this.twoMinBlitz_Click);
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
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
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
            this.logout.Click += new System.EventHandler(this.logout_Click);
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
            this.Controls.Add(this.viewAchievementsButton);
            this.Controls.Add(this.oneMinBlitz);
            this.Controls.Add(this.addStandardGame);
            this.Controls.Add(this.addCustomButton);
            this.Controls.Add(this.createCustomGameButton);
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
        private System.Windows.Forms.Button addCustomButton;
        private System.Windows.Forms.Button createCustomGameButton;
        private System.Windows.Forms.Button oneMinBlitz;
        private System.Windows.Forms.Button viewAchievementsButton;
        private System.Windows.Forms.Button twoMinBlitz;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn custom;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn turnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameID;
    }
}