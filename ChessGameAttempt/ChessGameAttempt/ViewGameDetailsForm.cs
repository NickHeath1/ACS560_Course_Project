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
    public partial class ViewGameDetailsForm : Form
    {
        MoveLogic moveLogic;
        Button[,] board;
        public ViewGameDetailsForm()
        {
            InitializeComponent();
            moveLogic = new MoveLogic();
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

            // Set up the customizations of the board
            foreach (Button square in board)
            {
                MoveLogic.Coordinates c = moveLogic.GetCoordinatesOfButton(square);
                square.BackColor = (c.X + c.Y) % 2 == 0 ? 
                    ChessUtils.Settings.Color.darkSquareColor : 
                    ChessUtils.Settings.Color.lightSquareColor;


            }
        }

        public void SetOpponentName(string name)
        {
            opponentName.Text = name;
        }

        public void SetTurnTime(string time)
        {
            playerTurnTime.Text = time;
        }

        public void SetGameTime(string time)
        {
            playerTotalTime.Text = time;
        }

        public void SetToMove(string color)
        {
            colorToMove.Text = color;
        }

        public void SetBoard(List<MoveLogic.Piece> pieces)
        {
            foreach (MoveLogic.Piece piece in pieces)
            {
                MoveLogic.Coordinates c = piece.Coordinates;
                board[c.X, c.Y].Tag = piece.Name;

                switch (piece.Name)
                {
                    case "wPawn":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.WhitePawn;
                        break;
                    case "wKnight":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.WhiteKnight;
                        break;
                    case "wBishop":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.WhiteBishop;
                        break;
                    case "wRook":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.WhiteRook;
                        break;
                    case "wQueen":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.WhiteQueen;
                        break;
                    case "wKing":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.WhiteKing;
                        break;
                    case "bPawn":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.BlackPawn;
                        break;
                    case "bKnight":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.BlackKnight;
                        break;
                    case "bBishop":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.BlackBishop;
                        break;
                    case "bRook":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.BlackRook;
                        break;
                    case "bQueen":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.BlackQueen;
                        break;
                    case "bKing":
                        board[c.X, c.Y].Image = ChessUtils.Settings.Image.BlackKing;
                        break;
                    case "NoPiece":
                        board[c.X, c.Y].Image = null;
                        break;
                }
            }
        }
    }
}
