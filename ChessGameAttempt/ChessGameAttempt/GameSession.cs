using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessGameAttempt.MoveLogic;

namespace ChessGameAttempt
{
    public partial class GameSession : Form
    {
        User me;
        string myName, opponentName;
        NetworkStream stream;
        LobbyForm lobby;
        MoveLogic moveLogic;
        CheckLogic checkLogic;

        System.Timers.Timer timer;
        int totalTimeRemaining;
        int turnTimeRemaining;
        int eTotalTimeRemaining;
        int eTurnTimeRemaining;
        int originalTimeRemaining;
        int originalTotalTimeRemaining;

        // The following are used for the castling conditions
        bool whiteKingMoved = false;
        bool blackKingMoved = false;
        bool blackLeftRookMoved = false;
        bool blackRightRookMoved = false;
        bool whiteLeftRookMoved = false;
        bool whiteRightRookMoved = false;

        // List of squares that will be highlighted and are possible for a given piece to move to
        List<Button> movePieceTo = new List<Button>();

        // List of buttons for white and black players
        public List<Piece> whitePieces = new List<Piece>();
        public List<Piece> blackPieces = new List<Piece>();

        Button selectedButton, previousButton;

        // List of buttons (squares) that white and black are attacking currently
        List<Button> whiteAttacks = new List<Button>();
        List<Button> blackAttacks = new List<Button>();
        List<Button> selectedPieceAttacks = new List<Button>();

        // All of the squares on the board
        static Button[,] board;

        Session sessionInfo;
        CustomGame customGame;

        public GameSession(User Me, User Them, NetworkStream networkStream, Session session, LobbyForm Lobby, MoveLogic ml, CustomGame game = null)
        {
            me = Me;
            int squareSize = 65;
            int offset = 50;
            InitializeComponent();
            lobby = Lobby;
            moveLogic = ml;

            // Create timers
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += async (sender, e) => await Timer_Elapsed();

            stream = networkStream;
            sessionInfo = session;

            originalTotalTimeRemaining = eTotalTimeRemaining = totalTimeRemaining = sessionInfo.GameTimerSeconds;
            originalTimeRemaining = eTotalTimeRemaining = turnTimeRemaining = sessionInfo.MoveTimerSeconds;

            myTotalTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(totalTimeRemaining);
            myTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(turnTimeRemaining);
            enemyTotalTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(totalTimeRemaining);
            enemyTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(turnTimeRemaining);

            myName = myUsername.Text = me.Username;
            opponentName = enemyUsername.Text = Them.Username;
            customGame = game;
            // set up the array of buttons (chess grid) into an array
            board = new Button[,]
            {
                { square00, square01, square02, square03, square04, square05, square06, square07},
                { square10, square11, square12, square13, square14, square15, square16, square17},
                { square20, square21, square22, square23, square24, square25, square26, square27},
                { square30, square31, square32, square33, square34, square35, square36, square37},
                { square40, square41, square42, square43, square44, square45, square46, square47},
                { square50, square51, square52, square53, square54, square55, square56, square57},
                { square60, square61, square62, square63, square64, square65, square66, square67},
                { square70, square71, square72, square73, square74, square75, square76, square77}
            };

            // Set up the board to look pretty
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    Point p = new Point(j * squareSize + offset, i * squareSize + (offset * 2));
                    Size s = new Size(squareSize, squareSize);
                    Coordinates c = ChessUtils.Settings.GetCoordinatesOfButton(board[i,j]);
                    
                    board[i, j].Size = s;
                    board[i, j].Location = p;
                }
            }

            ChessUtils.Settings.Image.UpdateBoardImages(board);
            ChessUtils.Settings.Color.UpdateChessBoardColors(board);

            UpdatePlayerPieces(whitePieces, blackPieces);
            moveLogic.UpdateAttackedSquares(board);

            SetUpButtons();
            moveLogic.ClearChessBoardColors(board);

            // I am the guest, both people are in the game, now set the game to started
            if (Me.Username != "" && Them.Username != "")
            {
                moveLogic.gameStarted = true;
            }

            checkLabel.Hide();

            gameWorker.RunWorkerAsync();
        }

        private Task Timer_Elapsed()
        {
            Action action;
            Invoke(action = new Action(() =>
            {
                if (moveLogic.myTurn)
                {
                    totalTimeRemaining--;
                    turnTimeRemaining--;

                    myTotalTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(totalTimeRemaining);
                    myTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(turnTimeRemaining);
                }
                else
                {
                    eTotalTimeRemaining--;
                    eTurnTimeRemaining--;

                    enemyTotalTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(eTotalTimeRemaining);
                    enemyTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(eTurnTimeRemaining);
                }
            }));
            return new Task(action);
        }

        private void SetUpButtons()
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    board[i, j].Click += ButtonClicked;
                }
            }
        }

        private void UpdatePlayerPieces(List<Piece> whitePieces, List<Piece> blackPieces)
        {
            whitePieces.Clear();
            blackPieces.Clear();

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    Button button = board[i, j];
                    if (moveLogic.IsPieceOn(i, j, board))
                    {
                        Piece piece = moveLogic.GetPieceOnSquare(i, j, board);

                        if (piece.Color == pieceColor.white)
                        {
                            whitePieces.Add(piece);
                        }
                        else
                        {
                            blackPieces.Add(piece);
                        }
                    }
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs args)
        {
            Button currentButton = sender as Button;
            selectedButton = currentButton;
            Coordinates c = ChessUtils.Settings.GetCoordinatesOfButton(currentButton);
            Coordinates pc;
            Coordinates cc;
            Piece piece = moveLogic.GetPieceOnSquare(c.X, c.Y, board);
            bool isMyPiece = moveLogic.myColor == piece.Color;

            // If there is a piece on the selected square that is current player's color
            if (isMyPiece)
            {
                List<Button> possibleMoves = moveLogic.GetPossibleMovesForPiece(currentButton, board);
                moveLogic.HighlightButtons(possibleMoves);
            }

            // Potential square to move to from a selected piece
            else
            {
                if (SelectedPieceCanMoveTo(currentButton))
                {
                    pc = ChessUtils.Settings.GetCoordinatesOfButton(previousButton);
                    cc = ChessUtils.Settings.GetCoordinatesOfButton(currentButton);
                    Piece thisPiece = moveLogic.GetPieceOnSquare(pc.X, pc.Y, board);
                    isMyPiece = moveLogic.myColor == thisPiece.Color;

                    if (isMyPiece)
                    {
                        // Move piece to new square
                        currentButton.Tag = previousButton.Tag;
                        currentButton.Image = previousButton.Image;

                        // Take piece off selected square
                        previousButton.Tag = "";
                        previousButton.Image = null;

                        bool promoted = false;

                        // Check for pawn promotion
                        if (cc.X % 7 == 0 && currentButton.Tag.ToString().Contains("Pawn"))
                        {
                            // White promotion
                            if (thisPiece.Color == pieceColor.white)
                            {
                                promoted = true;
                                var whiteForm = new PawnPromotionFormWhite();

                                // Pick based off of button
                                if (whiteForm.ShowDialog() == DialogResult.OK)
                                {
                                    switch (whiteForm.GetValue())
                                    {
                                        case "Queen":
                                            currentButton.Tag = "wQueen";
                                            currentButton.Image = ChessUtils.Settings.Image.WhiteQueen;
                                            break;
                                        case "Rook":
                                            currentButton.Tag = "wRook";
                                            currentButton.Image = ChessUtils.Settings.Image.WhiteRook;
                                            break;
                                        case "Knight":
                                            currentButton.Tag = "wKnight";
                                            currentButton.Image = ChessUtils.Settings.Image.WhiteKnight;
                                            break;
                                        case "Bishop":
                                            currentButton.Tag = "wBishop";
                                            currentButton.Image = ChessUtils.Settings.Image.WhiteBishop;
                                            break;
                                    }
                                }

                                // If form is closed, default to a queen
                                else
                                {
                                    currentButton.Tag = "wQueen";
                                    currentButton.Image = ChessUtils.Settings.Image.WhiteQueen;
                                }
                            }

                            // Black promotion
                            else
                            {
                                promoted = true;
                                var blackForm = new PawnPromotionFormBlack();
                                // Pick based off of button
                                if (blackForm.ShowDialog() == DialogResult.OK)
                                {
                                    switch (blackForm.GetValue())
                                    {
                                        case "Queen":
                                            currentButton.Tag = "bQueen";
                                            currentButton.Image = ChessUtils.Settings.Image.BlackQueen;
                                            break;
                                        case "Rook":
                                            currentButton.Tag = "bRook";
                                            currentButton.Image = ChessUtils.Settings.Image.BlackRook;
                                            break;
                                        case "Knight":
                                            currentButton.Tag = "bKnight";
                                            currentButton.Image = ChessUtils.Settings.Image.BlackKnight;
                                            break;
                                        case "Bishop":
                                            currentButton.Tag = "bBishop";
                                            currentButton.Image = ChessUtils.Settings.Image.BlackBishop;
                                            break;
                                    }
                                }

                                // If form is closed, default to a queen
                                else
                                {
                                    currentButton.Tag = "bQueen";
                                    currentButton.Image = ChessUtils.Settings.Image.BlackQueen;
                                }
                            }
                        }

                        // switch turns
                        moveLogic.myTurn = !moveLogic.myTurn;

                        // Send the move to the server to send to the other client
                        Piece sendPieceLimitedInfo = new Piece();
                        sendPieceLimitedInfo.Coordinates = thisPiece.Coordinates;

                        string pieceName;
                        if (promoted)
                        {
                            pieceName = currentButton.Tag.ToString();
                        }
                        else
                        {
                            pieceName = (moveLogic.myColor == pieceColor.white) ?
                                "w" : "b";
                            pieceName += thisPiece.Name;
                        }
                        sendPieceLimitedInfo.Name = pieceName;

                        Move move = new Move()
                        {
                            Source = sendPieceLimitedInfo,
                            Destination = cc
                        };

                        TCPSignal signal = new TCPSignal()
                        {
                            SignalType = Signal.MakeAMove,
                            PlayerMove = move
                        };

                        // Send the json over to the server
                        string json = JsonConvert.SerializeObject(signal) + "\r";
                        byte[] jsonBytes = ASCIIEncoding.ASCII.GetBytes(json);
                        stream.Write(jsonBytes, 0, jsonBytes.Length);

                        // Clear highlights
                        moveLogic.ClearChessBoardColors(board);
                    }
                }
                else
                {
                    moveLogic.ClearChessBoardColors(board);
                }
            }

            previousButton = currentButton;
        }

        private bool SelectedPieceCanMoveTo(Button button)
        {
            return (button.BackColor == moveLogic.colorHighlight1 || button.BackColor == moveLogic.colorHighlight2);
        }

        private void settingsIcon_Click(object sender, EventArgs e)
        {
            // Open settings
            SettingsForm settings = new SettingsForm(me);
            settings.ShowDialog();

            ChessUtils.Settings.Color.UpdateChessBoardColors(board);
            ChessUtils.Settings.Image.UpdateBoardImages(board);
        }

        private void tieButton_Click(object sender, EventArgs e)
        {
            // Offer draw

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Forfeit
            Close();
        }

        private void GameSession_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (opponentName != "")
            {
                DialogResult result = MessageBox.Show("Are you sure you wish to forfeit?", "Forfeit", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Cancel this background worker
                    gameWorker.CancelAsync();
                    timer.Stop();

                    // Send loss info/win info

                    RemoveGameFromLobby();
                    lobby.RefreshTable();
                }
                else
                {
                    e.Cancel = false;
                }
            }
            else
            {
                // Cancel this background worker
                gameWorker.CancelAsync();

                RemoveGameFromLobby();
                lobby.RefreshTable();
            }
        }

        private void RemoveGameFromLobby()
        {
            // Remove the game from the lobby table
            int sessionID = sessionInfo.SessionID;

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
            Thread.Sleep(1000);
        }

        public Button[,] GetBoard()
        {
            return board;
        }

        private void gameWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!gameWorker.CancellationPending)
            {
                if (opponentName == "")
                {
                    byte[] data = new byte[65535];
                    try
                    {
                        stream.Read(data, 0, data.Length);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    string json = ASCIIEncoding.ASCII.GetString(data).Replace("\0", "");
                    TCPSignal signal = (TCPSignal)JsonConvert.DeserializeObject(json, typeof(TCPSignal));
                    if (signal.SignalType == Signal.StartSession)
                    {
                        // Host
                        Invoke(new Action(() =>
                        {
                            moveLogic.gameStarted = true;
                            //TODO add timers, etc
                            opponentName = enemyUsername.Text = signal.NewSession.GuestPlayer;
                        }));
                    }
                }
                else
                {
                    // TODO: Perform server move logic, timers, etc.

                    // Guest
                    Invoke(new Action(() =>
                    {
                        enemyUsername.Text = opponentName;
                        if (moveLogic.myTurn)
                        {
                            timer.Start();
                        }
                        
                    }));
                    byte[] data = new byte[65535];

                    try
                    {
                        stream.Read(data, 0, data.Length);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    string json = ASCIIEncoding.ASCII.GetString(data).Replace("\0", "");
                    TCPSignal signal = (TCPSignal)JsonConvert.DeserializeObject(json, typeof(TCPSignal));

                    if (signal.SignalType == Signal.MakeAMove)
                    {
                        Invoke(new Action(() =>
                        {
                            Coordinates destination = signal.PlayerMove.Destination;
                            Piece pieceMoved = signal.PlayerMove.Source;

                            string pieceTag = pieceMoved.Name;
                            Coordinates c = pieceMoved.Coordinates;

                            Button originSquare = moveLogic.GetButtonOn(c.X, c.Y, board);
                            Button newSquare = moveLogic.GetButtonOn(destination.X, destination.Y, board);

                            originSquare.Tag = "NoPiece";
                            originSquare.Image = null;
                            newSquare.Tag = pieceTag;
                            newSquare.Image = ChessUtils.Settings.Image.GetImageForTag(pieceTag);

                            // Update check logic...
                            checkLogic = new CheckLogic(board);
                            ChessUtils.CheckState checkState = checkLogic.CetCheckState();

                            if (checkState == ChessUtils.CheckState.Check)
                            {
                                checkLabel.Show();
                            }

                            else if (checkState == ChessUtils.CheckState.NoCheck)
                            {
                                checkLabel.Hide();
                            }

                            else if (checkState == ChessUtils.CheckState.Checkmate)
                            {
                                // I lost, send the lose signal
                                // They won, send the win signal
                            }

                            else if (checkState == ChessUtils.CheckState.Stalemate)
                            {
                                // We tied, send tie signal for both
                            }

                            // It is now my turn
                            moveLogic.myTurn = !moveLogic.myTurn;

                            if (totalTimeRemaining > originalTimeRemaining)
                            {
                                turnTimeRemaining = originalTimeRemaining;
                                
                            }
                            else
                            {
                                turnTimeRemaining = totalTimeRemaining;
                            }
                            myTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(turnTimeRemaining);
                            myTotalTimeRemaining.Text = ChessUtils.ConvertSecondsToTimeString(totalTimeRemaining);
                        }));
                    }
                }
            }
        }
    }
}
