using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessGameAttempt.GameSession;

namespace ChessGameAttempt
{
    public partial class LobbyForm : Form
    {
        public User me;
        public TcpClient client;
        public NetworkStream stream;

        public string[] pieceString = new string[]
        {
            "King",
            "Queen",
            "Rook",
            "Bishop",
            "Knight",
            "Pawn",
            ""
        };

        public LobbyForm(User user)
        {
            me = user;
            SquareColorSettings colors = DataApiController<SquareColorSettings>.GetData("http://localhost:2345/GetCustomChessboardForUser/" + me.Username);
            ChessUtils.Settings.Color.darkSquareColor = Color.FromArgb(colors.Red1, colors.Green1, colors.Blue1);
            ChessUtils.Settings.Color.lightSquareColor = Color.FromArgb(colors.Red2, colors.Green2, colors.Blue2);
            InitializeComponent();
            client = new TcpClient("localhost", 2346);
            stream = client.GetStream();

            join.Enabled = remove.Enabled = false;

            RefreshTable();

            // Prevent first row from being deletable by non-owners
            if (lobbyTable.Rows.Count > 0)
            {
                if ((string)lobbyTable.Rows[0].Cells[2].Value != me.Username)
                {
                    remove.Enabled = false;
                }
            }
        }

        public DataGridView GetLobbyTable()
        {
            return lobbyTable;
        }

        private void clearTable()
        {
            lobbyTable.Rows.Clear();
        }

        private void AddGameToLobbyTable(int sessionId, bool customCheck, string username, int turnTime, int gameTime, int gameId)
        {
            string turnString, gameString;
            turnString = ChessUtils.ConvertSecondsToTimeString(turnTime);
            gameString = ChessUtils.ConvertSecondsToTimeString(gameTime);
            lobbyTable.Rows.Add(sessionId, customCheck, username, turnString, gameString, gameId);

            // Send a signal to other clients to add this game to the list
        }

        private void RemoveGameFromLobbyTable(int rowIndex)
        {
            lobbyTable.Rows.RemoveAt(rowIndex);
            if (lobbyTable.SelectedRows.Count > 0)
            {
                if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[2].Value != me.Username)
                {
                    remove.Enabled = false;
                    join.Enabled = true;
                }
            }

            // Send a signal to other clients to also remove this row
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (lobbyTable.SelectedRows.Count > 0)
            {
                // Remove the game from the lobby table
                RemoveSessionFromTable();

                // Refresh the table
                RefreshTable();

                if (lobbyTable.SelectedRows.Count > 0)
                {
                    if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[2].Value != me.Username)
                    {
                        remove.Enabled = false;
                    }
                }
            }
        }

        private void lobbyTable_SelectionChanged(object sender, EventArgs e)
        {
            int index = (lobbyTable.SelectedRows.Count > 0) ? lobbyTable.SelectedRows[0].Index : -1;
            if (index == -1)
            {
                return;
            }

            if ((string)lobbyTable.Rows[index].Cells[2].Value == me.Username)
            {
                remove.Enabled = true;
                join.Enabled = false;
            }
            else
            {
                remove.Enabled = false;
                join.Enabled = true;
            }
        }

        private void LobbyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Remove my games from the lobby
            for (int i = 0; i < lobbyTable.Rows.Count; ++i)
            {
                if ((string)lobbyTable.Rows[i].Cells[2].Value == me.Username)
                {
                    RemoveSessionFromTable();
                }
            }

        }

        private void addStandardGame_Click(object sender, EventArgs e)
        {
            if (UserHasActiveGame())
            {
                MessageBox.Show("Error: You already have a game queued in the lobby table!\nDelete this game and try again.");
                return;
            }

            // Create the new session to add to the lobby table
            Session mySession = new Session()
            {
                HostPlayer = me.Username,
                GuestPlayer = "",
                GameTimerSeconds = 1200,
                MoveTimerSeconds = 120,
                CustomGameMode = 0
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
            stream.Write(jsonBytes, 0, jsonBytes.Length);

            RefreshTable();
        }

        private void RemoveSessionFromTable()
        {
            // When the lobby form first comes up, we want to fill the table with the active sessions from the server
            //Get the session id for my user
            int sessionID = -1;
            for (int i = 0; i < lobbyTable.Rows.Count; ++i)
            {
                if ((string)lobbyTable.Rows[i].Cells[2].Value == me.Username)
                {
                    sessionID = (int)lobbyTable.Rows[i].Cells[0].Value;
                    break;
                }
            }

            // Session not found. Return.
            if (sessionID == -1) return;

            // Create the signal to send
            TCPSignal signal = new TCPSignal()
            {
                SignalType = Signal.DeleteSession,
                SessionID = sessionID
            };

            // Send the json over to the server
            string json = JsonConvert.SerializeObject(signal) + "\r";
            byte[] jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
            stream.Write(jsonBytes, 0, jsonBytes.Length);
        }

        public void RefreshTable()
        {
            // When the lobby form first comes up, we want to fill the table with the active sessions from the server
            // Create the signal to send
            TCPSignal signal = new TCPSignal()
            {
                SignalType = Signal.GetSessions
            };

            // Send the json over to the server
            string json = JsonConvert.SerializeObject(signal) + "\r";
            byte[] jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
            byte[] readBuffer = new byte[65536];
            stream.Write(jsonBytes, 0, jsonBytes.Length);

            json = "";
            stream.Read(readBuffer, 0, readBuffer.Length);
            json += ASCIIEncoding.ASCII.GetString(readBuffer).Replace("\0", "");
            while (stream.DataAvailable)
            {
                stream.Read(readBuffer, 0, readBuffer.Length);
                json += ASCIIEncoding.ASCII.GetString(readBuffer).Replace("\0", "");
            }

            // Add the sessions to the table
            List<Session> sessions = new List<Session>();
            sessions = (List<Session>)JsonConvert.DeserializeObject(json, typeof(List<Session>));
            clearTable();
            if (sessions != null)
            {
                foreach (Session session in sessions)
                {
                    bool isCustomGame = (session.CustomGameMode == 3);
                    string un = session.HostPlayer;
                    int gameTime = session.GameTimerSeconds;
                    int moveTime = session.MoveTimerSeconds;
                    int sessionID = session.SessionID;
                    int gameID = session.GameID;

                    // Create the row in the table
                    AddGameToLobbyTable(sessionID, isCustomGame, un, moveTime, gameTime, gameID);
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void join_Click(object sender, EventArgs e)
        {
            if (lobbyTable.SelectedRows.Count > 0)
            {
                // Pop up to ask the user if they are sure that they want to join X's game.
                // Get the game text
                string userGame = (string)lobbyTable.SelectedRows[0].Cells[2].Value + "'s game?";
                DialogResult result = MessageBox.Show("Are you sure you would like to join " + userGame, "Join " + userGame, MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    // Verify this game can be joined. Send the signal to the other user.
                }
            }
        }

        private void details_Click(object sender, EventArgs e)
        {
            if (lobbyTable.SelectedRows.Count > 0)
            {
                ViewGameDetailsForm vgdf = new ViewGameDetailsForm();

                string hostUsername = (string)lobbyTable.SelectedRows[0].Cells[2].Value;
                int gameId = (int)lobbyTable.SelectedRows[0].Cells[5].Value;

                List<CustomGame> games = DataApiController<List<CustomGame>>.GetData("http://localhost:2345/GetCustomGamesForUser/" + hostUsername);
                CustomGame game = games.Single(x => x.GameID == gameId);
                if (game != null)
                {
                    vgdf.SetGameTime(ChessUtils.ConvertSecondsToTimeString(game.GameTimer));
                    vgdf.SetTurnTime(ChessUtils.ConvertSecondsToTimeString(game.MoveTimer));
                    vgdf.SetOpponentName(hostUsername);
                    vgdf.SetToMove(game.WhiteMovesFirst ? "White" : "Black");
                    vgdf.SetBoard(game.Pieces);
                    vgdf.Show();
                }
            }
        }

        private void createCustomGameButton_Click(object sender, EventArgs e)
        {
            CreateCustomGameForm ccgf = new CreateCustomGameForm(me);
            ccgf.ShowDialog();
        }

        private void addCustomButton_Click(object sender, EventArgs e)
        {
            if (UserHasActiveGame())
            {
                MessageBox.Show("Error: You already have a game queued in the lobby table!\nDelete this game and try again.");
                return;
            }

            AddCustomGameForm form = new AddCustomGameForm(me, this);
            form.ShowDialog();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            // Log out just closes this form.
            client.Close();
            Close(); 
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(me);

        }

        private void oneMinBlitz_Click(object sender, EventArgs e)
        {
            if (UserHasActiveGame())
            {
                MessageBox.Show("Error: You already have a game queued in the lobby table!\nDelete this game and try again.");
                return;
            }

            // Create the new session to add to the lobby table
            Session mySession = new Session()
            {
                HostPlayer = me.Username,
                GuestPlayer = "",
                GameTimerSeconds = 60,
                MoveTimerSeconds = 0,
                CustomGameMode = 0
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
            stream.Write(jsonBytes, 0, jsonBytes.Length);

            RefreshTable();
        }

        private void twoMinBlitz_Click(object sender, EventArgs e)
        {
            if (UserHasActiveGame())
            {
                MessageBox.Show("Error: You already have a game queued in the lobby table!\nDelete this game and try again.");
                return;
            }

            // Create the new session to add to the lobby table
            Session mySession = new Session()
            {
                HostPlayer = me.Username,
                GuestPlayer = "",
                GameTimerSeconds = 120,
                MoveTimerSeconds = 0,
                CustomGameMode = 0
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
            stream.Write(jsonBytes, 0, jsonBytes.Length);

            RefreshTable();
        }

        private bool UserHasActiveGame()
        {
            // Ensure this user does not already have an active session
            for (int i = 0; i < lobbyTable.Rows.Count; ++i)
            {
                if ((string)lobbyTable.Rows[i].Cells[2].Value == me.Username)
                {
                    return true;
                }
            }

            return false;
        }

        private void viewAchievementsButton_Click(object sender, EventArgs e)
        {
            AchievementsForm form = new AchievementsForm(me);
            form.Show();
        }
    }
}
