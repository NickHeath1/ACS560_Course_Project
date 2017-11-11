namespace ChessGameAttempt
{
    partial class PawnPromotionFormWhite
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
            this.QueenButton = new System.Windows.Forms.Button();
            this.RookButton = new System.Windows.Forms.Button();
            this.KnightButton = new System.Windows.Forms.Button();
            this.BishopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // QueenButton
            // 
            this.QueenButton.Image = global::ChessGameAttempt.Properties.Resources.wQueen;
            this.QueenButton.Location = new System.Drawing.Point(14, 64);
            this.QueenButton.Name = "QueenButton";
            this.QueenButton.Size = new System.Drawing.Size(75, 75);
            this.QueenButton.TabIndex = 0;
            this.QueenButton.Tag = "Queen";
            this.QueenButton.UseVisualStyleBackColor = true;
            this.QueenButton.Click += new System.EventHandler(this.QueenButton_Click);
            // 
            // RookButton
            // 
            this.RookButton.Image = global::ChessGameAttempt.Properties.Resources.wRook;
            this.RookButton.Location = new System.Drawing.Point(114, 64);
            this.RookButton.Name = "RookButton";
            this.RookButton.Size = new System.Drawing.Size(75, 75);
            this.RookButton.TabIndex = 1;
            this.RookButton.Tag = "Rook";
            this.RookButton.UseVisualStyleBackColor = true;
            this.RookButton.Click += new System.EventHandler(this.RookButton_Click);
            // 
            // KnightButton
            // 
            this.KnightButton.Image = global::ChessGameAttempt.Properties.Resources.wKnight;
            this.KnightButton.Location = new System.Drawing.Point(214, 64);
            this.KnightButton.Name = "KnightButton";
            this.KnightButton.Size = new System.Drawing.Size(75, 75);
            this.KnightButton.TabIndex = 2;
            this.KnightButton.Tag = "Knight";
            this.KnightButton.UseVisualStyleBackColor = true;
            this.KnightButton.Click += new System.EventHandler(this.KnightButton_Click);
            // 
            // BishopButton
            // 
            this.BishopButton.Image = global::ChessGameAttempt.Properties.Resources.wBishop;
            this.BishopButton.Location = new System.Drawing.Point(314, 64);
            this.BishopButton.Name = "BishopButton";
            this.BishopButton.Size = new System.Drawing.Size(75, 75);
            this.BishopButton.TabIndex = 3;
            this.BishopButton.Tag = "Bishop";
            this.BishopButton.UseVisualStyleBackColor = true;
            this.BishopButton.Click += new System.EventHandler(this.BishopButton_Click);
            // 
            // PawnPromotionFormWhite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 201);
            this.Controls.Add(this.BishopButton);
            this.Controls.Add(this.KnightButton);
            this.Controls.Add(this.RookButton);
            this.Controls.Add(this.QueenButton);
            this.Name = "PawnPromotionFormWhite";
            this.Text = "Promote White Pawn To...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button QueenButton;
        private System.Windows.Forms.Button RookButton;
        private System.Windows.Forms.Button KnightButton;
        private System.Windows.Forms.Button BishopButton;
    }
}