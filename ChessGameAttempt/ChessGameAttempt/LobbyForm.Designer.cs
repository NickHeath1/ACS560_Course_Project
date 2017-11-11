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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.details = new System.Windows.Forms.Button();
            this.lobbyTable = new System.Windows.Forms.DataGridView();
            this.custom = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.turnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gameTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lobbyTable)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(80, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(80, 184);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // details
            // 
            this.details.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.details.Location = new System.Drawing.Point(373, 431);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(122, 61);
            this.details.TabIndex = 4;
            this.details.Text = "View Details";
            this.details.UseVisualStyleBackColor = true;
            // 
            // lobbyTable
            // 
            this.lobbyTable.AllowUserToAddRows = false;
            this.lobbyTable.AllowUserToDeleteRows = false;
            this.lobbyTable.AllowUserToOrderColumns = true;
            this.lobbyTable.AllowUserToResizeRows = false;
            this.lobbyTable.BackgroundColor = System.Drawing.Color.Aqua;
            this.lobbyTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lobbyTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.custom,
            this.username,
            this.turnTime,
            this.gameTime});
            this.lobbyTable.Location = new System.Drawing.Point(373, 69);
            this.lobbyTable.MultiSelect = false;
            this.lobbyTable.Name = "lobbyTable";
            this.lobbyTable.ReadOnly = true;
            this.lobbyTable.RowHeadersVisible = false;
            this.lobbyTable.RowTemplate.Height = 24;
            this.lobbyTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lobbyTable.Size = new System.Drawing.Size(539, 335);
            this.lobbyTable.TabIndex = 8;
            this.lobbyTable.SelectionChanged += new System.EventHandler(this.lobbyTable_SelectionChanged);
            // 
            // custom
            // 
            this.custom.HeaderText = "Custom?";
            this.custom.Name = "custom";
            this.custom.ReadOnly = true;
            // 
            // username
            // 
            this.username.HeaderText = "UserName";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            // 
            // turnTime
            // 
            this.turnTime.HeaderText = "Turn Time";
            this.turnTime.Name = "turnTime";
            this.turnTime.ReadOnly = true;
            // 
            // gameTime
            // 
            this.gameTime.HeaderText = "Game Time";
            this.gameTime.Name = "gameTime";
            this.gameTime.ReadOnly = true;
            // 
            // join
            // 
            this.join.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.join.Location = new System.Drawing.Point(582, 431);
            this.join.Name = "join";
            this.join.Size = new System.Drawing.Size(122, 61);
            this.join.TabIndex = 9;
            this.join.Text = "Join Game";
            this.join.UseVisualStyleBackColor = true;
            // 
            // remove
            // 
            this.remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remove.Location = new System.Drawing.Point(790, 431);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(122, 61);
            this.remove.TabIndex = 10;
            this.remove.Text = "Remove Game";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ChessGameAttempt.Properties.Resources.Abstract_Blue_Water_Backgrounds_800x600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(972, 542);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.join);
            this.Controls.Add(this.lobbyTable);
            this.Controls.Add(this.details);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "LobbyForm";
            this.Text = "LobbyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LobbyForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.lobbyTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button details;
        private System.Windows.Forms.DataGridView lobbyTable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn custom;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn turnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameTime;
        private System.Windows.Forms.Button join;
        private System.Windows.Forms.Button remove;
    }
}