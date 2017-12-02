using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    public partial class AddCustomGameForm : Form
    {
        public User me;
        List<CustomGame> gameList;

        public AddCustomGameForm(User user)
        {
            me = user;
            InitializeComponent();
            GetGames();
        }

        private void GetGames()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2345/GetCustomGamesForUser/" + me.Username);
            request.Method = "GET";

            request.ContentType = @"application/json";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    gameList = (List<CustomGame>)JsonConvert.DeserializeObject(json, typeof(List<CustomGame>));

                    foreach (CustomGame game in gameList)
                    {
                        AddGameToList(game);
                    }
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response != null)
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                    Close();
                    return;
                }
            }
        }

        private void AddGameToList(CustomGame game)
        {
            int gameId = game.GameID;
            string gameName = game.CustomGameName;
            gamesList.Rows.Add(gameId, gameName);
        }

        private void addGameButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (gamesList.SelectedRows.Count > 0)
            {
                // Confirm that the user wants to delete the selected custom game!
                DialogResult result = MessageBox.Show("Are you sure you would like to delete the game \"" + 
                    (string)gamesList.SelectedRows[0].Cells[1].Value + "\"?", "Warning - Delete custom game", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) return;

                // The user has selected to delete the custom game, continue...
                int gameId = (int)gamesList.SelectedRows[0].Cells[0].Value;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2345/DeleteCustomGame/" + gameId.ToString());
                request.Method = "POST";

                try
                {
                    using (Stream dataStream = request.GetRequestStream())
                    {
                    }

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        // Catch will be called if this is the case
                    }
                }
                catch (WebException ex)
                {
                    var response = (HttpWebResponse)ex.Response;
                    if (response != null)
                    {
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                        Close();
                        return;
                    }
                }
            }
        }

        private void viewDetailsButton_Click(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
