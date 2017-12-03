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
    public partial class AchievementsForm : Form
    {
        public User me;
        public List<Achievement> AllAchievements;
        public List<Achievement> UsersAchievements;
        public AchievementsForm(User user)
        {
            me = user;
            InitializeComponent();
            AllAchievements = DataApiController<List<Achievement>>.GetData("http://localhost:2345/GetAllAchievements");
            UsersAchievements = DataApiController<List<Achievement>>.GetData("http://localhost:2345/GetAchievementsForUser/" + me.Username);
            foreach (Achievement achievement in AllAchievements)
            {
                bool achieved = UsersAchievements.Where(x => x.AchievementID == achievement.AchievementID).Any();
                dgvAchievements.Rows.Add(achievement.AchievementID, achievement.Title, achievement.Difficulty, achieved);
                if (achieved)
                {
                    dgvAchievements.Rows[dgvAchievements.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void dgvAchievements_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAchievements.SelectedRows.Count > 0)
            {
                Achievement achievement = AllAchievements.Single(x => x.AchievementID == (int)dgvAchievements.SelectedRows[0].Cells[0].Value);
                lblTitle.Text = achievement.Title;
                lblDescription.Text = achievement.Description;
                lblDifficulty.Text = achievement.Difficulty;
                lblAchievementEarned.Visible = (bool)dgvAchievements.SelectedRows[0].Cells[3].Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
