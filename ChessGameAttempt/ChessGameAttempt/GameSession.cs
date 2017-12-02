using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessGameAttempt.MoveLogic;

namespace ChessGameAttempt
{

    /*      *************FUNCTIONS*************
     *      
     *      void ClearChessBoardColors()
     *          Desc: Gets rid of any highlighting on the board
     *          
     *      void HighlightButtons(List<Button> buttons)
     *          In: Array of buttons (squares) to highlight on the board
     *          Desc: Highlights buttons on the board to display available moves
     *          
     *      void HighlightSelectedButton(Button button)
     *          In: The button on the board to be highlighted
     *          Desc: Highlights the button that is clicked (if there is a piece on that button)
     *          
     *      bool SelectedPieceCanMoveTo(Button button)
     *          In: The button in question to move to
     *          Out: True if the piece is logically able to move to that button, otherwise false
     *          Desc: Verifies that a piece is logically able to move to a specific button on the board
     *          
     *      void UpdatePlayerPieces()
     *          Desc: Updates the whitePieces and blackPieces lists with the current layout
     *          
     *      void UpdateAttackedSquares()
     *          Desc: Updates the attackedSquares list with the squares under attack by both colored pieces
     *          
     *      void KingLogicForSquare(pieceColor color, int x, int y)
     *          In: The piece's color, and the coordinates of the square that it is on
     *          Desc: Applies king move and attack logic to that square
     *          
     *      void QueenLogicForSquare(pieceColor color, int x, int y)
     *          In: The piece's color, and the coordinates of the square that it is on
     *          Desc: Applies queen move and attack logic to that square
     *          
     *      void RookLogicForSquare(pieceColor color, int x, int y)
     *          In: The piece's color, and the coordinates of the square that it is on
     *          Desc: Applies rook move and attack logic to that square
     *          
     *      void KnightLogicForSquare(pieceColor color, int x, int y)
     *          In: The piece's color, and the coordinates of the square that it is on
     *          Desc: Applies knight move and attack logic to that square
     *          
     *      void BishopLogicForSquare(pieceColor color, int x, int y)
     *          In: The piece's color, and the coordinates of the square that it is on
     *          Desc: Applies bishop move and attack logic to that square
     *          
     *      void PawnLogicForSquare(pieceColor color, int x, int y)
     *          In: The piece's color, and the coordinates of the square that it is on
     *          Desc: Applies pawn move and attack logic to that square
     *          
     *      void AddAttacks(pieceColor color, List<Button> attacks)
     *          In: The piece's color and the list of buttons that that piece currently attacks
     *          Desc: Adds attacks to whiteAttacks or blackAttacks based on piece color
     *          
     *      void SetUpButtons()
     *          Desc: Adds the ButtonClicked functionality to all of the chess board buttons
     *          
     *      Button GetButtonOn(int x, int y)
     *          In: Coordinates of a button
     *          Out: Button that is at coordinates x, y
     *          Desc: Gets a button based on the coordinates of the button or NULL
     *          
     *      PlayerPiece GetPieceOnSquare(int x, int y)
     *          In: Coordinates of a button
     *          Out: a play piece on the coordinates x, y or playerpiece "NoPiece"
     *          
     *      void ButtonClicked(object sender, EventArgs args)
     *          In: Function called when a button is clicked
     *          Desc: Highlights a piece and potential moves for that piece if there is a piece
     *                OR moves a piece to a highlighted square, if there is a piece already selected
     *                
     *      string GetPieceStringOn(int x, int y)
     *          In: Coordinates of a button
     *          Out: string of a piece or NoString. (i.e. Pawn for white or black pawn)
     *          Desc: Returns a string for the piece on a button at x, y OR ""
     *          
     *      List<Button> GetPossibleMovesForPiece(Button button)
     *          In: A button to determine the possible moves if there is a piece on that button
     *          Out: The list of attackable buttons for that piece
     *          Desc: Gets all possible moves for a selected button at coordinates x, y
     *          NOTE: applies check logic
     *          
     *      bool IsPieceOn(int x, int y)
     *          In: Coordinates of a button
     *          Out: True if there is a piece on the button, otherwise false
     *          Desc: Determines if there is a piece on a selected tile
     *          
     *      Coordinates GetCoordinatesOfButton(Button button)
     *          In: A selected button
     *          Out: The x, y coordinates of that button
     *          Desc: Returns a coordinate object c with the x, y coordinates (c.x, c.y)
     */

    public partial class GameSession : Form
    {
        MoveLogic moves = new MoveLogic();
        string myName, opponentName;

        // The following are used for the castling conditions
        bool whiteKingMoved = false;
        bool blackKingMoved = false;
        bool blackLeftRookMoved = false;
        bool blackRightRookMoved = false;
        bool whiteLeftRookMoved = false;
        bool whiteRightRookMoved = false;

        // booleans for the chat window options
        bool isMuted = false;
        bool isChatVisible = true;

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

        // enum for all kinds of pieces


        public GameSession(User me, User them)
        {
            myName = me.Username;
            opponentName = them.Username;

            int squareSize = 65;
            int offset = 50;
            InitializeComponent();

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
                    Point p = new Point(j * squareSize + offset, i * squareSize + offset);
                    Size s = new Size(squareSize, squareSize);

                    board[i, j].Size = s;
                    board[i, j].Location = p;
                }
            }

            UpdatePlayerPieces(whitePieces, blackPieces);
            moves.UpdateAttackedSquares(board);

            SetUpButtons();
            moves.ClearChessBoardColors(board);
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
                    if (moves.IsPieceOn(i, j, board))
                    {
                        Piece piece = moves.GetPieceOnSquare(i, j, board);

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
            Coordinates c = moves.GetCoordinatesOfButton(currentButton);
            Piece piece = moves.GetPieceOnSquare(c.X, c.Y, board);

            // If there is a piece on the selected square that is current player's color
            if (moves.IsMyPiece(piece))
            {
                List<Button> possibleMoves = moves.GetPossibleMovesForPiece(currentButton, board);
                moves.HighlightButtons(possibleMoves);
            }

            // Potential square to move to from a selected piece
            else
            {
                if (SelectedPieceCanMoveTo(currentButton))
                {
                    Coordinates pc = moves.GetCoordinatesOfButton(previousButton);
                    Coordinates cc = moves.GetCoordinatesOfButton(currentButton);
                    Piece thisPiece = moves.GetPieceOnSquare(pc.X, pc.Y, board);
                    if (moves.IsMyPiece(thisPiece))
                    {
                        // Move piece to new square
                        currentButton.Tag = previousButton.Tag;
                        currentButton.Image = previousButton.Image;

                        // Take piece off selected square
                        previousButton.Tag = "";
                        previousButton.Image = null;

                        // Check for pawn promotion
                        if (cc.X % 7 == 0 && currentButton.Tag.ToString().Contains("Pawn"))
                        {
                            // White promotion
                            if (thisPiece.Color == pieceColor.white)
                            {
                                var whiteForm = new PawnPromotionFormWhite();
                                // Pick based off of button
                                if (whiteForm.ShowDialog() == DialogResult.OK)
                                {
                                    switch (whiteForm.GetValue())
                                    {
                                        case "Queen":
                                            currentButton.Tag = "wQueen";
                                            currentButton.Image = Properties.Resources.wQueen;
                                            break;
                                        case "Rook":
                                            currentButton.Tag = "wRook";
                                            currentButton.Image = Properties.Resources.wRook;
                                            break;
                                        case "Knight":
                                            currentButton.Tag = "wKnight";
                                            currentButton.Image = Properties.Resources.wKnight;
                                            break;
                                        case "Bishop":
                                            currentButton.Tag = "wBishop";
                                            currentButton.Image = Properties.Resources.wBishop;
                                            break;
                                    }
                                }

                                // If form is closed, default to a queen
                                else
                                {
                                    currentButton.Tag = "wQueen";
                                    currentButton.Image = Properties.Resources.wQueen;
                                }
                            }

                            // Black promotion
                            else
                            {
                                var blackForm = new PawnPromotionFormBlack();
                                // Pick based off of button
                                if (blackForm.ShowDialog() == DialogResult.OK)
                                {
                                    switch (blackForm.GetValue())
                                    {
                                        case "Queen":
                                            currentButton.Tag = "bQueen";
                                            currentButton.Image = Properties.Resources.bQueen;
                                            break;
                                        case "Rook":
                                            currentButton.Tag = "bRook";
                                            currentButton.Image = Properties.Resources.bRook;
                                            break;
                                        case "Knight":
                                            currentButton.Tag = "bKnight";
                                            currentButton.Image = Properties.Resources.bKnight;
                                            break;
                                        case "Bishop":
                                            currentButton.Tag = "bBishop";
                                            currentButton.Image = Properties.Resources.bBishop;
                                            break;
                                    }
                                }

                                // If form is closed, default to a queen
                                else
                                {
                                    currentButton.Tag = "bQueen";
                                    currentButton.Image = Properties.Resources.bQueen;
                                }
                            }
                        }

                        // switch turns
                        moves.whiteTurn = !moves.whiteTurn;

                        // Clear highlights
                        moves.ClearChessBoardColors(board);
                    }
                }
                else
                {
                    moves.ClearChessBoardColors(board);
                }
            }

            previousButton = currentButton;
        }

        private bool SelectedPieceCanMoveTo(Button button)
        {
            return (button.BackColor == moves.colorHighlight1 || button.BackColor == moves.colorHighlight2);
        }

        private void settingsIcon_Click(object sender, EventArgs e)
        {
            // Open settings
        
        }

        private void tieButton_Click(object sender, EventArgs e)
        {
            // Offer draw

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Forfeit
            
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            // Mute / unmute sounds
            isMuted = !isMuted;
            // Change icon

            muteButton.BackgroundImage = isMuted ?
                ChessGameAttempt.Properties.Resources.muteIcon :
                ChessGameAttempt.Properties.Resources.unmuteIcon;
        }

        private void hideChatButton_Click(object sender, EventArgs e)
        {
            // Hide / unhide chat window
            isChatVisible = !isChatVisible;

            chatWindow.Visible = isChatVisible;
            chatToSend.Visible = isChatVisible;
            sendButton.Visible = isChatVisible;
        }



        private void sendButton_Click(object sender, EventArgs e)
        {
            if (chatToSend.Text != "")
            {
                if(chatWindow.Text != "")
                {
                    chatWindow.Text += "\n";
                }

                chatWindow.Text += myName + ": " + chatToSend.Text;
                chatToSend.Text = "";
            }
        }

        public Button[,] GetBoard()
        {
            return board;
        }

        private void chatToSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == '\r')
            {
                sendButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}
