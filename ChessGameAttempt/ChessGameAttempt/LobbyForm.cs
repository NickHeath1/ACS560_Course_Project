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
    public partial class LobbyForm : Form
    {
        public string user;

        public LobbyForm(string username)
        {
            InitializeComponent();
            user = username;

            DataTable table = new DataTable("table");
            AddGameToLobbyTable(true, "username", 10, 80);
            AddGameToLobbyTable(false, "cake", 26, 90);
            AddGameToLobbyTable(false, "corn", 2, 110);
            AddGameToLobbyTable(true, "user123", 40, 70);
            AddGameToLobbyTable(true, "pineapple", 31, 105);

            // Prevent first row from being deletable by non-owners
            if ((string)lobbyTable.Rows[0].Cells[1].Value != user)
            {
                remove.Enabled = false;
            }
        }

        private void AddGameToLobbyTable(bool customCheck, string username, int turnTime, int gameTime)
        {
            lobbyTable.Rows.Add(customCheck, username, turnTime, gameTime);

            // Send a signal to other clients to add this game to the list
        }

        private void RemoveGameFromLobbyTable(int rowIndex)
        {
            lobbyTable.Rows.RemoveAt(rowIndex);
            if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[1].Value != user)
            {
                remove.Enabled = false;
            }

            // Send a signal to other clients to also remove this row
        }

        private void remove_Click(object sender, EventArgs e)
        {
            RemoveGameFromLobbyTable(lobbyTable.SelectedRows[0].Index);
            if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[1].Value != user)
            {
                remove.Enabled = false;
            }
        }

        private void lobbyTable_SelectionChanged(object sender, EventArgs e)
        {
            int index = lobbyTable.SelectedRows[0].Index;
            if (index == -1)
            {
                return;
            }

            if ((string)lobbyTable.Rows[index].Cells[1].Value == user)
            {
                remove.Enabled = true;
            }
            else
            {
                remove.Enabled = false;
            }
        }

        private void LobbyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Remove my games from the lobby
            for (int i = 0; i < lobbyTable.Rows.Count; ++i)
            {
                if ((string)lobbyTable.Rows[i].Cells[1].Value == user)
                {
                    
                }
            }

        }
    }
}
