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
        public string user;
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

        public LobbyForm(string username)
        {
            InitializeComponent();
            user = username;
            client = new TcpClient("localhost", 2346);
            stream = client.GetStream();

            DataTable table = new DataTable("table");
            AddGameToLobbyTable(1, true, "username", 10, 80);
            AddGameToLobbyTable(2, false, "cake", 26, 90);
            AddGameToLobbyTable(3, false, "corn", 2, 110);
            AddGameToLobbyTable(4, true, "user123", 40, 70);
            AddGameToLobbyTable(5, true, "pineapple", 31, 105);

            // Prevent first row from being deletable by non-owners
            if ((string)lobbyTable.Rows[0].Cells[2].Value != user)
            {
                remove.Enabled = false;
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
                if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[2].Value != user)
                {
                    remove.Enabled = false;
                }
            }

            // Send a signal to other clients to also remove this row
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (lobbyTable.SelectedRows.Count > 0)
            {
                RemoveGameFromLobbyTable(lobbyTable.SelectedRows[0].Index);
                if (lobbyTable.SelectedRows.Count > 0)
                {
                    if ((string)lobbyTable.Rows[lobbyTable.SelectedRows[0].Index].Cells[2].Value != user)
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

            if ((string)lobbyTable.Rows[index].Cells[2].Value == user)
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
                if ((string)lobbyTable.Rows[i].Cells[2].Value == user)
                {
                    
                }
            }

        }

        private void addStandardGame_Click(object sender, EventArgs e)
        {
            // Init host and guest pieces
            //Piece[][] boardPieces = new Piece[8][];
            //for (int i = 0; i < 8; ++i)
            //{
            //    boardPieces[i] = new Piece[8];
            //}

            // setup piece layout on board
            //for (int i = 0; i < 8; ++i)
            //{
            //    for (int j = 0; i < 8; ++j)
            //    {
            //        Piece piece = GetPieceOnSquare(i, j);
            //        boardPieces[i][j] = piece;
            //    }
            //}
            List<Session> sessions = new List<Session>();

            Session mySession = new Session()
            {
                HostPlayer = user,
                GuestPlayer = "",
                GameTimerSeconds = 1200,
                MoveTimerSeconds = 120,
                CustomGameMode = 0
            };

            TCPSignal signal = new TCPSignal()
            {
                SignalType = Signal.CreateSession,
                NewSession = mySession
            };

            string json = JsonConvert.SerializeObject(signal) + "\r";
            byte[] jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
            byte[] readBuffer = new byte[65536];
            stream.Write(jsonBytes, 0, jsonBytes.Length);

            signal.SignalType = Signal.GetSessions;
            json = JsonConvert.SerializeObject(signal) + "\r";
            jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
            stream.Write(jsonBytes, 0, jsonBytes.Length);
            json = "";
            stream.Read(readBuffer, 0, readBuffer.Length);
            json += ASCIIEncoding.ASCII.GetString(readBuffer).Replace("\0", "");
            while (stream.DataAvailable)
            {
                stream.Read(readBuffer, 0, readBuffer.Length);
                json += ASCIIEncoding.ASCII.GetString(readBuffer).Replace("\0", "");
            }
            sessions = (List<Session>)JsonConvert.DeserializeObject(json, typeof(List<Session>));

            clearTable();
            foreach(Session session in sessions)
            {
                bool isCustomGame = (session.CustomGameMode == 3);
                string username = session.HostPlayer;
                int gameTime = session.GameTimerSeconds;
                int moveTime = session.MoveTimerSeconds;
                int sessionID = session.SessionID;

                // Create the row in the table
                AddGameToLobbyTable(sessionID, isCustomGame, username, moveTime, gameTime);
            }
        }

        private Piece GetPieceOnSquare(int x, int y)
        {
            Piece piece = new Piece();
            if (IsPieceOn(x, y))
            {
                piece.button = buttons[x, y];
                piece.color = buttons[x, y].Tag.ToString()[0] == 'w' ? pieceColor.white : pieceColor.black;
                piece.coords.X = x;
                piece.coords.Y = y;
                piece.piece = buttons[x, y].Tag.ToString().Substring(1);
            }
            else
            {
                piece.piece = pieceString[(int)BoardPiece.NoPiece];
            }
            return piece;
        }

        private bool IsPieceOn(int x, int y)
        {
            return GetPieceStringOn(x, y) != "";
        }

        private string GetPieceStringOn(int x, int y)
        {
            if (x > 7 || y > 7 || x < 0 || y < 0)
            {
                return pieceString[(int)BoardPiece.NoPiece]; // No piece
            }

            else
            {
                if (buttons[x, y].Tag != null)
                {
                    return buttons[x, y].Tag.ToString();
                }

                else return "";
            }
        }


    }
}
