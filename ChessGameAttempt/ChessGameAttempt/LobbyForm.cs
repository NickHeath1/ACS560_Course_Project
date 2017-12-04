using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

            InitializeComponent();
            SetupSettings();
            client = new TcpClient(ChessUtils.IPAddress, ChessUtils.Port + 1);
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
            //tcpClientWorker.RunWorkerAsync();
        }

        private void SetupSettings()
        {
            SquareColorSettings colors = DataApiController<SquareColorSettings>.GetData(ChessUtils.IPAddressWithPort + "GetCustomChessboardForUser/" + me.Username);
            ChessUtils.Settings.Color.darkSquareColor = Color.FromArgb(colors.Red1, colors.Green1, colors.Blue1);
            ChessUtils.Settings.Color.lightSquareColor = Color.FromArgb(colors.Red2, colors.Green2, colors.Blue2);

            List<PieceImageSettings> images = DataApiController<List<PieceImageSettings>>.GetData(ChessUtils.IPAddressWithPort + "GetCustomPieceImagesForUser/" + me.Username);
            if (images != null)
            {
                foreach (PieceImageSettings image in images)
                {
                    switch (image.PieceName)
                    {
                        case "wKing":
                            ChessUtils.Settings.Image.WhiteKing = GetImageFromByteArray(image.Image);
                            break;
                        case "wQueen":
                            ChessUtils.Settings.Image.WhiteQueen = GetImageFromByteArray(image.Image);
                            break;
                        case "wRook":
                            ChessUtils.Settings.Image.WhiteRook = GetImageFromByteArray(image.Image);
                            break;
                        case "wBishop":
                            ChessUtils.Settings.Image.WhiteBishop = GetImageFromByteArray(image.Image);
                            break;
                        case "wKnight":
                            ChessUtils.Settings.Image.WhiteKnight = GetImageFromByteArray(image.Image);
                            break;
                        case "wPawn":
                            ChessUtils.Settings.Image.WhitePawn = GetImageFromByteArray(image.Image);
                            break;
                        case "bKing":
                            ChessUtils.Settings.Image.BlackKing = GetImageFromByteArray(image.Image);
                            break;
                        case "bQueen":
                            ChessUtils.Settings.Image.BlackQueen = GetImageFromByteArray(image.Image);
                            break;
                        case "bRook":
                            ChessUtils.Settings.Image.BlackRook = GetImageFromByteArray(image.Image);
                            break;
                        case "bBishop":
                            ChessUtils.Settings.Image.BlackBishop = GetImageFromByteArray(image.Image);
                            break;
                        case "bKnight":
                            ChessUtils.Settings.Image.BlackKnight = GetImageFromByteArray(image.Image);
                            break;
                        case "bPawn":
                            ChessUtils.Settings.Image.BlackPawn = GetImageFromByteArray(image.Image);
                            break;
                    }
                }
            }
        }

        private Image GetImageFromByteArray(byte[] ba)
        {
            MemoryStream ms = new MemoryStream(ba);
            return Image.FromStream(ms);
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
            GameSession session = new GameSession(me, new User("", ""), stream);
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
                    TCPSignal signal = new TCPSignal
                    {
                        SignalType = Signal.JoinSession,
                        GuestPlayerName = me.Username,
                        HostPlayerName = (string)lobbyTable.SelectedRows[0].Cells[2].Value,
                        SessionID = (int)lobbyTable.SelectedRows[0].Cells[0].Value
                    };
                    string json = JsonConvert.SerializeObject(signal) + "\r";
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(json);

                    stream.Write(data, 0, data.Length);

                    byte[] joined = new byte[1];
                    stream.Read(joined, 0, 1);

                    // Opponent accepted match
                    if (joined[0] == 1)
                    {
                        byte[] data2 = new byte[65535];
                        stream.Read(data2, 0, data2.Length);
                        json = ASCIIEncoding.ASCII.GetString(data2).Replace("\0", "");
                        TCPSignal signal2 = (TCPSignal)JsonConvert.DeserializeObject(json, typeof(TCPSignal));
                        GameSession session = new GameSession(me, new User(signal2.NewSession.HostPlayer, ""), stream);
                        session.ShowDialog();
                    }
                }
            }
        }

        private void details_Click(object sender, EventArgs e)
        {
            if (lobbyTable.SelectedRows.Count > 0)
            {
                ViewGameDetailsForm vgdf = new ViewGameDetailsForm();

                bool isCustomGame = (bool)lobbyTable.SelectedRows[0].Cells[1].Value;
                if (isCustomGame)
                {
                    string hostUsername = (string)lobbyTable.SelectedRows[0].Cells[2].Value;
                    int gameId = (int)lobbyTable.SelectedRows[0].Cells[5].Value;

                    List<CustomGame> games = DataApiController<List<CustomGame>>.GetData(ChessUtils.IPAddressWithPort + "GetCustomGamesForUser/" + hostUsername);
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
                else
                {
                    string gameTime = (string)lobbyTable.SelectedRows[0].Cells[4].Value;
                    string turnTime = (string)lobbyTable.SelectedRows[0].Cells[3].Value;
                    string host = (string)lobbyTable.SelectedRows[0].Cells[2].Value;
                    vgdf.SetGameTime(gameTime);
                    vgdf.SetTurnTime(turnTime);
                    vgdf.SetOpponentName(host);
                    vgdf.SetToMove("White");
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

            AddCustomGameForm form = new AddCustomGameForm(me, this, stream);
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
            settings.ShowDialog();
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
            GameSession session = new GameSession(me, new User("", ""), stream);
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
            GameSession session = new GameSession(me, new User("", ""), stream);
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

        private void tcpClientWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!tcpClientWorker.CancellationPending)
            {
                byte[] data = new byte[65535];
                if (lobbyTable.Rows.Count == 0)
                {
                    Thread.Sleep(10);
                    continue;
                }
                stream.Read(data, 0, data.Length);
                string json = ASCIIEncoding.ASCII.GetString(data).Replace("\0", "");
                if (!json.Contains("SignalType")) continue;
                TCPSignal signal = (TCPSignal)JsonConvert.DeserializeObject(json, typeof(TCPSignal));
                if (signal.SignalType == Signal.StartSession)
                {
                    tcpClientWorker2.RunWorkerAsync(new object[] { signal, stream });
                    while (!tcpClientWorker2.CancellationPending)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }

        private void tcpClientWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!tcpClientWorker2.CancellationPending)
            {
                object[] parameters = (object[])e.Argument;
                TCPSignal signal = (TCPSignal)parameters[0];
                NetworkStream gameStream = (NetworkStream)parameters[1];
                GameSession session = new GameSession(me, new User(signal.GuestPlayerName == me.Username ? signal.HostPlayerName : signal.GuestPlayerName, ""), gameStream);
                session.ShowDialog();
                tcpClientWorker2.CancelAsync();
            }
        }
    }
}
