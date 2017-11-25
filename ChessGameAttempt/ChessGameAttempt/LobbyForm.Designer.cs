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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
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
            // 
            // lobbyTable
            // 
            this.lobbyTable.AllowUserToAddRows = false;
            this.lobbyTable.AllowUserToDeleteRows = false;
            this.lobbyTable.AllowUserToOrderColumns = true;
            this.lobbyTable.AllowUserToResizeRows = false;
            this.lobbyTable.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lobbyTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
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
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lobbyTable.RowsDefaultCellStyle = dataGridViewCellStyle12;
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.NullValue = false;
            this.custom.DefaultCellStyle = dataGridViewCellStyle10;
            this.custom.HeaderText = "Custom?";
            this.custom.Name = "custom";
            this.custom.ReadOnly = true;
            this.custom.Width = 90;
            // 
            // username
            // 
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.DefaultCellStyle = dataGridViewCellStyle11;
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
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(29, 333);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 50);
            this.button4.TabIndex = 14;
            this.button4.Text = "1\' Blitz";
            this.button4.UseVisualStyleBackColor = true;
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
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(29, 387);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(92, 50);
            this.button6.TabIndex = 16;
            this.button6.Text = "2\' Blitz";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(29, 441);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(92, 50);
            this.button7.TabIndex = 17;
            this.button7.Text = "Change Settings";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(29, 520);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(92, 50);
            this.button8.TabIndex = 18;
            this.button8.Text = "Logout";
            this.button8.UseVisualStyleBackColor = true;
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
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ChessGameAttempt.Properties.Resources.Abstract_Blue_Water_Backgrounds_800x600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(921, 595);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
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
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn custom;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn turnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameTime;
        private System.Windows.Forms.Button refreshButton;
    }
}