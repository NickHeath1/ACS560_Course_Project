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
        // bool to determine which player's turn it is
        public bool whiteTurn = true;

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
        List<PlayerPiece> whitePieces = new List<PlayerPiece>();
        List<PlayerPiece> blackPieces = new List<PlayerPiece>();

        Button selectedButton, previousButton;
        Color colorHighlight1 = Color.LimeGreen;
        Color colorHighlight2 = Color.Green;
        Color currentHighlight = Color.Blue;

        // List of buttons (squares) that white and black are attacking currently
        List<Button> whiteAttacks = new List<Button>();
        List<Button> blackAttacks = new List<Button>();
        List<Button> selectedPieceAttacks = new List<Button>();

        // All of the squares on the board
        public static Button[,] buttons;

        // enum for all kinds of pieces
        public enum pieceColor
        {
            noColor = 0,
            white = 1,
            black = 2
        }
        public enum BoardPiece
        {
            King = 0,
            Queen = 1,
            Rook = 2,
            Bishop = 3,
            Knight = 4,
            Pawn = 5,
            NoPiece = 6
        }

        // corresponding strings for enums
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

        // Coordinate struct used for passing coordinates of a piece
        public struct Coordinates
        {
            public int x;
            public int y;
        }
        public struct PlayerPiece
        {
            public pieceColor color;
            public string piece;
            public Button button;
            public Coordinates coords;
        }

        public GameSession()
        {
            int squareSize = 65;
            int offset = 50;
            InitializeComponent();

            // set up the array of buttons (chess grid) into an array
            buttons = new Button[,]
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

                    buttons[i, j].Size = s;
                    buttons[i, j].Location = p;
                }
            }

            UpdatePlayerPieces();
            UpdateAttackedSquares();

            SetUpButtons();
            ClearChessBoardColors();
        }

        private void SetUpButtons()
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    buttons[i, j].Click += ButtonClicked;
                }
            }
        }

        private void ClearChessBoardColors()
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    buttons[i, j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
                }
            }
        }

        private void UpdatePlayerPieces()
        {
            whitePieces.Clear();
            blackPieces.Clear();

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    Button button = buttons[i, j];
                    if (IsPieceOn(i, j))
                    {
                        PlayerPiece piece = GetPieceOnSquare(i, j);

                        if (piece.color == pieceColor.white)
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

        private void UpdateAttackedSquares()
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    PlayerPiece piece = GetPieceOnSquare(i, j);
                    List<Button> attackedSquares = new List<Button>();

                    switch (piece.piece)
                    {
                        case "Pawn":
                            PawnMoveLogic(piece, attackedSquares);
                            break;

                        case "Knight":
                            KnightMoveLogic(piece, attackedSquares);
                            break;

                        case "Bishop":
                            BishopMoveLogic(piece, attackedSquares);
                            break;

                        case "Rook":
                            RookMoveLogic(piece, attackedSquares);
                            break;

                        case "Queen":
                            QueenMoveLogic(piece, attackedSquares);
                            break;

                        case "King":
                            KingMoveLogic(piece, attackedSquares);
                            break;
                    }
                }
            }
        }

        private void HighlightButtons(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                Coordinates c = GetCoordinatesOfButton(button);
                button.BackColor = (c.x + c.y) % 2 == 0 ? colorHighlight1 : colorHighlight2;
            }
        }

        private void HighlightSelectedButton(Button button)
        {
            button.BackColor = currentHighlight;
        }

        private void ButtonClicked(object sender, EventArgs args)
        {
            Button currentButton = sender as Button;
            selectedButton = currentButton;
            Coordinates c = GetCoordinatesOfButton(currentButton);
            PlayerPiece piece = GetPieceOnSquare(c.x, c.y);

            // If there is a piece on the selected square that is current player's color
            if (IsMyPiece(piece))
            {
                List<Button> moves = GetPossibleMovesForPiece(currentButton);
                HighlightButtons(moves);
            }

            // Potential square to move to from a selected piece
            else
            {
                if (SelectedPieceCanMoveTo(currentButton))
                {
                    Coordinates pc = GetCoordinatesOfButton(previousButton);
                    Coordinates cc = GetCoordinatesOfButton(currentButton);
                    PlayerPiece thisPiece = GetPieceOnSquare(pc.x, pc.y);
                    if (IsMyPiece(thisPiece))
                    {
                        // Move piece to new square
                        currentButton.Tag = previousButton.Tag;
                        currentButton.Image = previousButton.Image;

                        // Take piece off selected square
                        previousButton.Tag = "";
                        previousButton.Image = null;

                        // Check for pawn promotion
                        if (cc.x % 7 == 0 && currentButton.Tag.ToString().Contains("Pawn"))
                        {
                            // White promotion
                            if (thisPiece.color == pieceColor.white)
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
                        whiteTurn = !whiteTurn;

                        // Clear highlights
                        ClearChessBoardColors();
                    }
                }
                else
                {
                    ClearChessBoardColors();
                }
            }

            previousButton = currentButton;
        }

        private List<Button> GetPossibleMovesForPiece(Button button)
        {
            // Clear potential moves from before
            ClearChessBoardColors();

            // Highlight new locations
            List<Button> locations = new List<Button>();
            Coordinates c = GetCoordinatesOfButton(button);
            PlayerPiece piece = GetPieceOnSquare(c.x, c.y);

            // If there is a piece on the selected square that is the color of the current player...
            if (piece.piece != pieceString[(int)BoardPiece.NoPiece] && IsMyPiece(piece))
            {
                // Highlight the clicked button
                HighlightSelectedButton(button);

                string stringOfPiece = GetPieceStringOn(c.x, c.y);

                switch (stringOfPiece.Substring(1))
                {
                    case "Pawn":
                        PawnMoveLogic(piece, locations);
                        break;

                    case "Bishop":
                        BishopMoveLogic(piece, locations);
                        break;

                    case "Rook":
                        RookMoveLogic(piece, locations);
                        break;

                    case "Queen":
                        QueenMoveLogic(piece, locations);
                        break;

                    case "Knight":
                        KnightMoveLogic(piece, locations);
                        break;

                    case "King":
                        KingMoveLogic(piece, locations);
                        break;
                }
            }

            return locations;
        }

        private Button GetButtonOn(int x, int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7) // On the board
            {
                return buttons[x, y];
            }

            return null;
        }

        private PlayerPiece GetPieceOnSquare(int x, int y)
        {
            PlayerPiece piece = new PlayerPiece();
            if (IsPieceOn(x, y))
            {
                piece.button = buttons[x, y];
                piece.color = buttons[x, y].Tag.ToString()[0] == 'w' ? pieceColor.white : pieceColor.black;
                piece.coords.x = x;
                piece.coords.y = y;
                piece.piece = buttons[x, y].Tag.ToString().Substring(1);
            }
            else
            {
                piece.piece = pieceString[(int)BoardPiece.NoPiece];
            }
            return piece;
        }

        private Coordinates GetCoordinatesOfButton(Button button)
        {
            Coordinates c;
            string buttonName = button.Name;
            int x = Convert.ToInt16(buttonName[buttonName.Length - 2].ToString());
            int y = Convert.ToInt16(buttonName[buttonName.Length - 1].ToString());

            c.x = x;
            c.y = y;

            return c;
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

        private bool SelectedPieceCanMoveTo(Button button)
        {
            return (button.BackColor == colorHighlight1 || button.BackColor == colorHighlight2);
        }

        private bool IsMyPiece(PlayerPiece piece)
        {
            return
                (piece.color == pieceColor.white && whiteTurn) ||
                (piece.color == pieceColor.black && !whiteTurn);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private bool IsPieceOn(int x, int y)
        {
            return GetPieceStringOn(x, y) != "";
        }
    }
}
