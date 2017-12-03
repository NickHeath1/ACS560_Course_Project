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
        LobbyForm lobby;
        List<CustomGame> gameList;

        public AddCustomGameForm(User user, LobbyForm form)
        {
            me = user;
            lobby = form;
            InitializeComponent();
            GetGames();
        }

        private void GetGames()
        {
            gameList = DataApiController<List<CustomGame>>.GetData("http://localhost:2345/GetCustomGamesForUser/" + me.Username);
            if (gameList != null)
            {
                foreach (CustomGame game in gameList)
                {
                    AddGameToList(game);
                }
            }
            else
            {
                totalTime.Text =
                turnTime.Text = 
                color.Text = "No data available";
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
            if (gamesList.SelectedRows.Count > 0)
            {
                // Get the game ID from the row
                int gameId = (int)gamesList.SelectedRows[0].Cells[0].Value;

                // Match the game ID with the appropriate CustomGame in the list
                CustomGame game = gameList.Single(x => x.GameID == gameId);

                // Convert the list of pieces into a double array (board)
                MoveLogic.Piece[][] pieces = new MoveLogic.Piece[8][];
                for (int i = 0; i < 8; ++i) pieces[i] = new MoveLogic.Piece[8];
                foreach (MoveLogic.Piece piece in game.Pieces)
                {
                    MoveLogic.Coordinates c = piece.Coordinates;
                    pieces[c.X][c.Y] = new MoveLogic.Piece();
                    pieces[c.X][c.Y] = piece;
                }

                // Create the new session to add to the lobby table
                Session mySession = new Session()
                {
                    HostPlayer = game.Username,
                    GuestPlayer = "",
                    GameTimerSeconds = game.GameTimer,
                    MoveTimerSeconds = game.MoveTimer,
                    CustomGameMode = 3 /* Custom game */,
                    BoardPieces = pieces,
                    GameID = gameId
                };

                // Create the signal to send
                TCPSignal signal = new TCPSignal()
                {
                    SignalType = Signal.CreateSession,
                    NewSession = mySession
                };

                // Send the json over to the server
                string json = JsonConvert.SerializeObject(signal) + "\r";
                byte[] jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
                byte[] readBuffer = new byte[65536];
                lobby.stream.Write(jsonBytes, 0, jsonBytes.Length);

                lobby.RefreshTable();

                Close();
            }
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

                DataApiController<List<CustomGame>>.PostData("http://localhost:2345/DeleteCustomGame/" + gameId.ToString(), null);

                // Delete the game from the data grid view
                gamesList.Rows.RemoveAt(gamesList.SelectedRows[0].Index);
            }
        }

        private void viewDetailsButton_Click(object sender, EventArgs e)
        {
            if (gamesList.SelectedRows.Count > 0)
            {
                ViewGameDetailsForm vgdf = new ViewGameDetailsForm();

                int gameId = (int)gamesList.SelectedRows[0].Cells[0].Value;

                List<CustomGame> games = DataApiController<List<CustomGame>>.GetData("http://localhost:2345/GetCustomGamesForUser/" + me.Username);
                CustomGame game = games.Single(x => x.GameID == gameId);
                if (game != null)
                {
                    vgdf.SetGameTime(ChessUtils.ConvertSecondsToTimeString(game.GameTimer));
                    vgdf.SetTurnTime(ChessUtils.ConvertSecondsToTimeString(game.MoveTimer));
                    vgdf.SetOpponentName(me.Username);
                    vgdf.SetToMove(game.WhiteMovesFirst ? "White" : "Black");
                    vgdf.SetBoard(game.Pieces);
                    vgdf.Show();
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gamesList_SelectionChanged(object sender, EventArgs e)
        {
            // Update the stuff on the right of the form
            if (gamesList.SelectedRows.Count > 0)
            {
                // Get the game ID from the row
                int gameId = (int)gamesList.SelectedRows[0].Cells[0].Value;

                // Match the game ID with the appropriate CustomGame in the list
                CustomGame game = gameList.Single(x => x.GameID == gameId);

                totalTime.Text = ChessUtils.ConvertSecondsToTimeString(game.GameTimer);
                turnTime.Text = ChessUtils.ConvertSecondsToTimeString(game.MoveTimer);
                color.Text = game.WhiteMovesFirst ?
                    "White" :
                    "Black";
            }
        }
    }
}
