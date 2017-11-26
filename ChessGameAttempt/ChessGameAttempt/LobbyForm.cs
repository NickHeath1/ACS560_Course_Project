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
        public string me;
        TcpClient client;
        NetworkStream stream;

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
            me = user.Username;

            InitializeComponent();
            client = new TcpClient("localhost", 2346);
            stream = client.GetStream();

            join.Enabled = remove.Enabled = false;

            RefreshTable();

            // Prevent first row from being deletable by non-owners
            if (lobbyTable.Rows.Count > 0)
            {
                if ((string)lobbyTable.Rows[0].Cells[2].Value != me)
                {
                    remove.Enabled = false;
                }
            }
        }

        private void clearTable()
        {
            lobbyTable.Rows.Clear();
        }

        private void AddGameToLobbyTable(int sessionId, bool customCheck, string username, int turnTime, int gameTime)
        {
            lobbyTable.Rows.Add(sessionId, customCheck, username, turnTime, gameTime);

            // Send a signal to other clients to add this game to the list
        }

        private void RemoveGameFromLobbyTable(int rowIndex)
        {
            lobbyTable.Rows.RemoveAt(rowIndex);
            if (lobbyTable.SelectedRows.Count > 0)
            {
                if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[2].Value != me)
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
                    if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[2].Value != me)
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

            if ((string)lobbyTable.Rows[index].Cells[2].Value == me)
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
                if ((string)lobbyTable.Rows[i].Cells[2].Value == me)
                {

                }
            }

        }

        private void addStandardGame_Click(object sender, EventArgs e)
        {
            // Ensure this user does not already have an active session
            for (int i = 0; i < lobbyTable.Rows.Count; ++i)
            {
                if ((string)lobbyTable.Rows[i].Cells[2].Value == me)
                {
                    MessageBox.Show("Error: You already have a game queued in the lobby table!\nDelete this game and try again.");
                    return;
                }
            }

            // Create the new session to add to the lobby table
            Session mySession = new Session()
            {
                HostPlayer = me,
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
                if ((string)lobbyTable.Rows[i].Cells[2].Value == me)
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

        private void RefreshTable()
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

                    // Create the row in the table
                    AddGameToLobbyTable(sessionID, isCustomGame, un, moveTime, gameTime);
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
                string opptName, gameTime, turnTime, color2Move;
                int gTime, tTime, gTimeMins, gTimeSecs, tTimeMins, tTimeSecs;

                // Is this a custom game? Change the board to match
                if ((bool)lobbyTable.SelectedRows[0].Cells[1].Value)
                {
                    // TODO: Change the board
                }

                // Not a custom game, the board is already set up for normal mode
                else
                {
                    opptName = (string)lobbyTable.SelectedRows[0].Cells[2].Value;
                    color2Move = "White";
                    tTime = (int)lobbyTable.SelectedRows[0].Cells[3].Value;
                    gTime = (int)lobbyTable.SelectedRows[0].Cells[4].Value;

                    if (gTime != 0)
                    {
                        gTimeMins = (int)((float)gTime / 60.0);
                        gTimeSecs = gTime % 60;

                        gameTime = (gTimeSecs < 0) ?
                            gTimeMins.ToString() + ":0" + gTimeSecs.ToString() :
                            gTimeMins.ToString() + ":" + gTimeSecs.ToString();

                        gameTime += gTimeSecs == 0 ? "0" : "";
                    }
                    else
                    {
                        gameTime = "Infinite";
                    }

                    if (tTime != 0)
                    {
                        tTimeMins = (int)((float)tTime / 60.0);
                        tTimeSecs = tTime % 60;

                        turnTime = (tTimeSecs < 0) ?
                            tTimeMins.ToString() + ":0" + tTimeSecs.ToString() :
                            tTimeMins.ToString() + ":" + tTimeSecs.ToString();

                        turnTime += tTimeSecs == 0 ? "0" : "";
                    }
                    else
                    {
                        turnTime = "Infinite";
                    }

                    // Set up the details form
                    ViewGameDetailsForm vgdf = new ViewGameDetailsForm();
                    vgdf.SetGameTime(gameTime);
                    vgdf.SetTurnTime(turnTime);
                    vgdf.SetToMove(color2Move);
                    vgdf.SetOpponentName(opptName);

                    vgdf.Show();
                }
            }
        }
    }
}
