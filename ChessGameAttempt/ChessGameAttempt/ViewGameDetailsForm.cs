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
    public partial class ViewGameDetailsForm : Form
    {
        public ViewGameDetailsForm()
        {
            InitializeComponent();
        }

        public void SetOpponentName(string name)
        {
            opponentName.Text = name;
        }

        public void SetTurnTime(string time)
        {
            playerTurnTime.Text = time;
        }

        public void SetGameTime(string time)
        {
            playerTotalTime.Text = time;
        }

        public void SetToMove(string color)
        {
            colorToMove.Text = color;
        }
    }
}
