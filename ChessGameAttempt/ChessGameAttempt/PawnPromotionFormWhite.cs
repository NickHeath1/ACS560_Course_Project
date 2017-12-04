using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    public partial class PawnPromotionFormWhite : Form
    {
        public string returnString = "Queen";

        public string GetValue()
        {
            return returnString;
        }

        public PawnPromotionFormWhite()
        {
            InitializeComponent();
            UpdateImages();
        }

        private void QueenButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RookButton_Click(object sender, EventArgs e)
        {
            returnString = "Rook";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void KnightButton_Click(object sender, EventArgs e)
        {
            returnString = "Knight";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BishopButton_Click(object sender, EventArgs e)
        {
            returnString = "Bishop";
            DialogResult = DialogResult.OK;
            Close();
        }

        public void UpdateImages()
        {
            QueenButton.Image = ChessUtils.Settings.Image.WhiteQueen;
            RookButton.Image = ChessUtils.Settings.Image.WhiteRook;
            KnightButton.Image = ChessUtils.Settings.Image.WhiteKnight;
            BishopButton.Image = ChessUtils.Settings.Image.WhiteBishop;
        }
    }
}
