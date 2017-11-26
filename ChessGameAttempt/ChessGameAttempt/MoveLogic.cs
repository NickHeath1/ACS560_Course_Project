using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    /* *************FUNCTIONS*************
     * 
     */


    public partial class MoveLogic
    {
        ///////////////////////////////////////////////////START DECLARATIONS///////////////////////////////////////////////////
        // bool to determine which player's turn it is
        public bool whiteTurn = true;
        public bool myTurn;

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

        public enum CheckCondition
        {
            NoCheck = 0,
            Check = 1,
            Checkmate = 2,
            Stalemate = 4
        }

        // corresponding strings for enums
        string[] pieceString =
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
            public int X;
            public int Y;
        }

        public struct Piece
        {
            public pieceColor color;
            public string piece;
            public Button button;
            public Coordinates coords;
        }

        public Color colorHighlight1 = Color.LimeGreen;
        public Color colorHighlight2 = Color.Green;
        public Color currentHighlight = Color.Blue;

        ///////////////////////////////////////////////////END OF DECLARATIONS///////////////////////////////////////////////////
        public Button GetButtonOn(int x, int y, Button[,] board)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7) // On the board
            {
                return board[x, y];
            }

            return null;
        }

        public bool IsMyPiece(Piece piece)
        {
            return
                (piece.color == pieceColor.white && whiteTurn) ||
                (piece.color == pieceColor.black && !whiteTurn);
        }

        public bool IsPieceOn(int x, int y, Button[,] board)
        {
            return GetPieceStringOn(x, y, board) != "";
        }

        public string GetPieceStringOn(int x, int y, Button[,] board)
        {
            if (x > 7 || y > 7 || x < 0 || y < 0)
            {
                return pieceString[(int)BoardPiece.NoPiece]; // No piece
            }

            else
            {
                if (board[x, y].Tag != null)
                {
                    return board[x, y].Tag.ToString();
                }

                else return "";
            }
        }

        public Piece GetPieceOnSquare(int x, int y, Button[,] board)
        {
            Piece piece = new Piece();
            if (IsPieceOn(x, y, board))
            {
                piece.button = board[x, y];
                piece.color = board[x, y].Tag.ToString()[0] == 'w' ? pieceColor.white : pieceColor.black;
                piece.coords.X = x;
                piece.coords.Y = y;
                piece.piece = board[x, y].Tag.ToString().Substring(1);
            }
            else
            {
                piece.piece = pieceString[(int)BoardPiece.NoPiece];
            }
            return piece;
        }

        // Used for King/Knight movements (DRY)
        public void CheckOffset(Piece piece, int xInc, int yInc, Button[,] board, List<Button> locations)
        {
            int newX, newY;
            Button square;
            Coordinates c = piece.coords;

            newX = c.X + xInc;
            newY = c.Y + yInc;
            square = GetButtonOn(newX, newY, board);
            if (square != null && !IsMyPiece(GetPieceOnSquare(newX, newY, board)))
            {
                locations.Add(square);
            }
        }

        public void ClearChessBoardColors(Button[,] board)
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    board[i, j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
                }
            }
        }

        public void HighlightButtons(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                Coordinates c = GetCoordinatesOfButton(button);
                button.BackColor = (c.X + c.Y) % 2 == 0 ? colorHighlight1 : colorHighlight2;
            }
        }

        public void HighlightSelectedButton(Button button)
        {
            button.BackColor = currentHighlight;
        }

        public Coordinates GetCoordinatesOfButton(Button button)
        {
            Coordinates c;
            string buttonName = button.Name;
            int x = Convert.ToInt16(buttonName[buttonName.Length - 2].ToString());
            int y = Convert.ToInt16(buttonName[buttonName.Length - 1].ToString());

            c.X = x;
            c.Y = y;

            return c;
        }

        public List<Button> GetPossibleMovesForPiece(Button button, Button[,] board)
        {
            // Clear potential moves from before
            ClearChessBoardColors(board);

            // Highlight new locations
            List<Button> locations = new List<Button>();
            Coordinates c = GetCoordinatesOfButton(button);
            Piece piece = GetPieceOnSquare(c.X, c.Y, board);

            // If there is a piece on the selected square that is the color of the current player...
            if (piece.piece != pieceString[(int)BoardPiece.NoPiece] && IsMyPiece(piece))
            {
                // Highlight the clicked button
                HighlightSelectedButton(button);

                string stringOfPiece = GetPieceStringOn(c.X, c.Y, board);

                switch (stringOfPiece.Substring(1))
                {
                    case "Pawn":
                        PawnMoveLogic(piece, locations, board);
                        break;

                    case "Bishop":
                        BishopMoveLogic(piece, locations, board);
                        break;

                    case "Rook":
                        RookMoveLogic(piece, locations, board);
                        break;

                    case "Queen":
                        QueenMoveLogic(piece, locations, board);
                        break;

                    case "Knight":
                        KnightMoveLogic(piece, locations, board);
                        break;

                    case "King":
                        KingMoveLogic(piece, locations, board);
                        break;
                }
            }

            return locations;
        }

        public List<Button> UpdateAttackedSquares(Button[,] board)
        {
            List<Button> attackedSquares = new List<Button>();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    Piece piece = GetPieceOnSquare(i, j, board);

                    switch (piece.piece)
                    {
                        case "Pawn":
                            PawnMoveLogic(piece, attackedSquares, board);
                            break;

                        case "Knight":
                            KnightMoveLogic(piece, attackedSquares, board);
                            break;

                        case "Bishop":
                            BishopMoveLogic(piece, attackedSquares, board);
                            break;

                        case "Rook":
                            RookMoveLogic(piece, attackedSquares, board);
                            break;

                        case "Queen":
                            QueenMoveLogic(piece, attackedSquares, board);
                            break;

                        case "King":
                            KingMoveLogic(piece, attackedSquares, board);
                            break;
                    }
                }
            }
            return attackedSquares;
        }

        public void KingMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    CheckOffset(piece, i, j, board, moveLocations);
                }
            }
        }

        public void KnightMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            for (int i = -2; i <= 2; ++i)
            {
                for (int j = -2; j <= 2; ++j)
                {
                    // Knights move 2, then 1, cannot move 1 then 1 or 2 then 2
                    // 0 in either direction is also invalid
                    if (i == j || i == -j || i == 0 || j == 0)
                    {
                        continue;
                    }

                    //2, then 1 (or 1, then 2) --- valid case
                    else
                    {
                        // Verify the square does not have MY piece on it
                        CheckOffset(piece, i, j, board, moveLocations);
                    }
                }
            }
        }

        public void QueenMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            RookMoveLogic(piece, moveLocations, board);
            BishopMoveLogic(piece, moveLocations, board);
        }

        public void RookMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            Coordinates c = piece.coords;
            int i, j, newX, newY;

            // DRY cannot be applied here
            newX = c.X + 1;
            newY = c.Y;
            for (i = newX; i < 8; ++i)
            {
                if (GetButtonOn(i, newY, board) != null)
                {
                    // Piece on square is black = add to attacks, but cannot go past that piece
                    if (GetPieceOnSquare(i, newY, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    // Piece on square is my piece = cannot move to that spot or beyond
                    else if (IsMyPiece(GetPieceOnSquare(i, newY, board)))
                    {
                        break;
                    }

                    // No piece on square, add to attacks and keep checking
                    else
                    {
                        moveLocations.Add(board[i, newY]);
                    }
                }
            }

            newX = c.X - 1;
            for (i = newX; i >= 0; --i)
            {
                if (GetButtonOn(i, newY, board) != null)
                {
                    if (GetPieceOnSquare(i, newY, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, newY, board)))
                    {
                        break;
                    }

                    else // No piece on square
                    {
                        moveLocations.Add(board[i, newY]);
                    }
                }
            }

            newX = c.X;
            newY = c.Y + 1;
            for (j = newY; j < 8; ++j)
            {
                if (GetButtonOn(newX, j, board) != null)
                {
                    if (GetPieceOnSquare(newX, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, j, board)))
                    {
                        break;
                    }

                    else // No piece on square
                    {
                        moveLocations.Add(board[newX, j]);
                    }
                }
            }

            newY = c.Y - 1;
            for (j = newY; j >= 0; --j)
            {
                if (GetButtonOn(newX, j, board) != null)
                {
                    if (GetPieceOnSquare(newX, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, j, board)))
                    {
                        break;
                    }

                    else // No piece on square
                    {
                        moveLocations.Add(board[newX, j]);
                    }
                }
            }

            // Capture logic
            newX = c.X + 1;
            newY = c.Y;
            for (i = newX; i < 8; ++i)
            {
                if (GetButtonOn(i, newY, board) != null)
                {
                    if (GetPieceOnSquare(i, newY, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, newY, board)))
                    {
                        break;
                    }
                }
            }

            newX = c.X - 1;
            for (i = newX; i >= 0; --i)
            {
                if (GetButtonOn(i, newY, board) != null)
                {
                    if (GetPieceOnSquare(i, newY, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, newY, board)))
                    {
                        break;
                    }
                }
            }

            newX = c.X;
            newY = c.Y + 1;
            for (j = newY; j < 8; ++j)
            {
                if (GetButtonOn(newX, j, board) != null)
                {
                    if (GetPieceOnSquare(newX, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, j, board)))
                    {
                        break;
                    }
                }
            }

            newY = c.Y - 1;
            for (j = newY; j >= 0; --j)
            {
                if (GetButtonOn(newX, j, board) != null)
                {
                    if (GetPieceOnSquare(newX, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, newY, board)))
                    {
                        break;
                    }
                }
            }
        }

        public void BishopMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            Coordinates c = piece.coords;
            int i, j, newX, newY;
            // Move logic...
            // Bishops move diagonally
            newX = c.X + 1;
            newY = c.Y + 1;
            for (i = newX, j = newY; i < 8 && j < 8; ++i, ++j)
            {
                if (GetButtonOn(i, j, board) != null &&
                    GetPieceOnSquare(i, j, board).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(board[i, j]);
                }
                else break;
            }
            newX = c.X - 1;
            newY = c.Y - 1;
            for (i = newX, j = newY; i >= 0 && j >= 0; --i, --j)
            {
                if (GetButtonOn(i, j, board) != null &&
                    GetPieceOnSquare(i, j, board).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(board[i, j]);
                }
                else break;
            }
            newX = c.X + 1;
            newY = c.Y - 1;
            for (i = newX, j = newY; i < 8 && j >= 0; ++i, --j)
            {
                if (GetButtonOn(i, j, board) != null &&
                    GetPieceOnSquare(i, j, board).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(board[i, j]);
                }
                else break;
            }
            newX = c.X - 1;
            newY = c.Y + 1;
            for (i = newX, j = newY; i >= 0 && j < 8; --i, ++j)
            {
                if (GetButtonOn(i, j, board) != null &&
                    GetPieceOnSquare(i, j, board).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(board[i, j]);
                }
                else break;
            }

            // Capture logic
            newX = c.X + 1;
            newY = c.Y + 1;
            for (i = newX, j = newY; i < 8 && j < 8; ++i, ++j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (GetPieceOnSquare(i, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }
                }
            }

            newX = c.X - 1;
            newY = c.Y - 1;
            for (i = newX, j = newY; i >= 0 && j >= 0; --i, --j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (GetPieceOnSquare(i, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }
                }
            }

            newX = c.X + 1;
            newY = c.Y - 1;
            for (i = newX, j = newY; i < 8 && j >= 0; ++i, --j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (GetPieceOnSquare(i, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }
                }
            }

            newX = c.X - 1;
            newY = c.Y + 1;
            for (i = newX, j = newY; i >= 0 && j < 8; --i, ++j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (GetPieceOnSquare(i, j, board).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }
                }
            }
        }

        // TODO : Add En Passant
        public void PawnMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            Coordinates c = piece.coords;
            // First move for pawn
            if (piece.color == pieceColor.white && whiteTurn)
            {
                // Move logic...
                if (GetButtonOn(c.X - 1, c.Y, board) != null &&
                    GetPieceOnSquare(c.X - 1, c.Y, board).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(board[c.X - 1, c.Y]);

                    // First position only
                    if (c.X == 6 &&
                    GetButtonOn(c.X - 2, c.Y, board) != null &&
                    GetPieceOnSquare(c.X - 2, c.Y, board).piece == pieceString[(int)BoardPiece.NoPiece])
                    {
                        moveLocations.Add(board[c.X - 2, c.Y]);
                    }
                }

                // Capture logic
                if (GetButtonOn(c.X - 1, c.Y - 1, board) != null &&
                    GetPieceOnSquare(c.X - 1, c.Y - 1, board).color == pieceColor.black)
                {
                    moveLocations.Add(board[c.X - 1, c.Y - 1]);
                }
                if (GetButtonOn(c.X - 1, c.Y + 1, board) != null &&
                    GetPieceOnSquare(c.X - 1, c.Y + 1, board).color == pieceColor.black)
                {
                    moveLocations.Add(board[c.X - 1, c.Y + 1]);
                }

            }

            else if (piece.color == pieceColor.black && !whiteTurn)
            {
                // Move logic
                if (GetButtonOn(c.X + 1, c.Y, board) != null &&
                    GetPieceOnSquare(c.X + 1, c.Y, board).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(board[c.X + 1, c.Y]);

                    if (c.X == 1 &&
                    GetButtonOn(c.X + 2, c.Y, board) != null &&
                    GetPieceOnSquare(c.X + 2, c.Y, board).piece == pieceString[(int)BoardPiece.NoPiece])
                    {
                        moveLocations.Add(board[c.X + 2, c.Y]);
                    }
                }

                // Capture logic
                if (GetButtonOn(c.X + 1, c.Y - 1, board) != null &&
                    GetPieceOnSquare(c.X + 1, c.Y - 1, board).color == pieceColor.white)
                {
                    moveLocations.Add(board[c.X + 1, c.Y - 1]);
                }
                if (GetButtonOn(c.X + 1, c.Y + 1, board) != null &&
                    GetPieceOnSquare(c.X + 1, c.Y + 1, board).color == pieceColor.white)
                {
                    moveLocations.Add(board[c.X + 1, c.Y + 1]);
                }
            }
        }
    }
}
