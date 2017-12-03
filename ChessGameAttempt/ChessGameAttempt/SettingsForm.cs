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
    public partial class SettingsForm : Form
    {
        public User me;

        public SettingsForm(User user)
        {
            me = user;
            InitializeComponent();
            darkColor.BackColor = ChessUtils.Settings.Color.darkSquareColor;
            lightColor.BackColor = ChessUtils.Settings.Color.lightSquareColor;

            int redLightComponent = lightColor.BackColor.R;
            int greenLightComponent = lightColor.BackColor.G;
            int blueLightComponent = lightColor.BackColor.B;
            int redDarkComponent = darkColor.BackColor.R;
            int greenDarkComponent = darkColor.BackColor.G;
            int blueDarkComponent = darkColor.BackColor.B;

            DR.Value = redDarkComponent;
            DG.Value = greenDarkComponent;
            DB.Value = blueDarkComponent;
            LR.Value = redLightComponent;
            LG.Value = greenLightComponent;
            LB.Value = blueLightComponent;
        }

        private void DR_ValueChanged(object sender, EventArgs e)
        {
            darkColor.BackColor = Color.FromArgb((int)DR.Value, (int)DG.Value, (int)DB.Value);
        }

        private void DG_ValueChanged(object sender, EventArgs e)
        {
            darkColor.BackColor = Color.FromArgb((int)DR.Value, (int)DG.Value, (int)DB.Value);
        }

        private void DB_ValueChanged(object sender, EventArgs e)
        {
            darkColor.BackColor = Color.FromArgb((int)DR.Value, (int)DG.Value, (int)DB.Value);
        }

        private void LR_ValueChanged(object sender, EventArgs e)
        {
            lightColor.BackColor = Color.FromArgb((int)LR.Value, (int)LG.Value, (int)LB.Value);
        }

        private void LG_ValueChanged(object sender, EventArgs e)
        {
            lightColor.BackColor = Color.FromArgb((int)LR.Value, (int)LG.Value, (int)LB.Value);
        }

        private void LB_ValueChanged(object sender, EventArgs e)
        {
            lightColor.BackColor = Color.FromArgb((int)LR.Value, (int)LG.Value, (int)LB.Value);
        }

        private void whiteKingButton_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Update the local settings
            ChessUtils.Settings.Color.darkSquareColor = darkColor.BackColor;
            ChessUtils.Settings.Color.lightSquareColor = lightColor.BackColor;

            // Send the settings to the database
            //TODO
        }
    }
}
