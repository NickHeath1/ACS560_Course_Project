using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    /* *************FUNCTIONS*************
     * 
     */

    public partial class GameSession : Form
    {
        // Used for King/Knight movements (DRY)
        public void CheckOffset(PlayerPiece piece, int xInc, int yInc, List<Button> locations)
        {
            int newX, newY;
            Button square;
            Coordinates c = piece.coords;

            newX = c.x + xInc;
            newY = c.y + yInc;
            square = GetButtonOn(newX, newY);
            if (square != null && !IsMyPiece(GetPieceOnSquare(newX, newY)))
            {
                locations.Add(square);
            }
        }

        public void KingMoveLogic(PlayerPiece piece, List<Button> moveLocations)
        {
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    CheckOffset(piece, i, j, moveLocations);
                }
            }
        }

        public void KnightMoveLogic(PlayerPiece piece, List<Button> moveLocations)
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
                        CheckOffset(piece, i, j, moveLocations);
                    }
                }
            }
        }

        public void QueenMoveLogic(PlayerPiece piece, List<Button> moveLocations)
        {
            RookMoveLogic(piece, moveLocations);
            BishopMoveLogic(piece, moveLocations);
        }

        public void RookMoveLogic(PlayerPiece piece, List<Button> moveLocations)
        {
            Coordinates c = piece.coords;
            int i, j, newX, newY;

            // DRY cannot be applied here
            newX = c.x + 1;
            newY = c.y;
            for (i = newX; i < 8; ++i)
            {
                if (GetButtonOn(i, newY) != null)
                {
                    // Piece on square is black = add to attacks, but cannot go past that piece
                    if (GetPieceOnSquare(i, newY).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, newY]);
                        break;
                    }

                    // Piece on square is my piece = cannot move to that spot or beyond
                    else if (IsMyPiece(GetPieceOnSquare(i, newY)))
                    {
                        break;
                    }

                    // No piece on square, add to attacks and keep checking
                    else
                    {
                        moveLocations.Add(buttons[i, newY]);
                    }
                }
            }

            newX = c.x - 1;
            for (i = newX; i >= 0; --i)
            {
                if (GetButtonOn(i, newY) != null)
                {
                    if (GetPieceOnSquare(i, newY).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, newY)))
                    {
                        break;
                    }

                    else // No piece on square
                    {
                        moveLocations.Add(buttons[i, newY]);
                    }
                }
            }

            newX = c.x;
            newY = c.y + 1;
            for (j = newY; j < 8; ++j)
            {
                if (GetButtonOn(newX, j) != null)
                {
                    if (GetPieceOnSquare(newX, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, j)))
                    {
                        break;
                    }

                    else // No piece on square
                    {
                        moveLocations.Add(buttons[newX, j]);
                    }
                }
            }

            newY = c.y - 1;
            for (j = newY; j >= 0; --j)
            {
                if (GetButtonOn(newX, j) != null)
                {
                    if (GetPieceOnSquare(newX, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, j)))
                    {
                        break;
                    }

                    else // No piece on square
                    {
                        moveLocations.Add(buttons[newX, j]);
                    }
                }
            }

            // Capture logic
            newX = c.x + 1;
            newY = c.y;
            for (i = newX; i < 8; ++i)
            {
                if (GetButtonOn(i, newY) != null)
                {
                    if (GetPieceOnSquare(i, newY).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, newY)))
                    {
                        break;
                    }
                }
            }

            newX = c.x - 1;
            for (i = newX; i >= 0; --i)
            {
                if (GetButtonOn(i, newY) != null)
                {
                    if (GetPieceOnSquare(i, newY).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, newY)))
                    {
                        break;
                    }
                }
            }

            newX = c.x;
            newY = c.y + 1;
            for (j = newY; j < 8; ++j)
            {
                if (GetButtonOn(newX, j) != null)
                {
                    if (GetPieceOnSquare(newX, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, j)))
                    {
                        break;
                    }
                }
            }

            newY = c.y - 1;
            for (j = newY; j >= 0; --j)
            {
                if (GetButtonOn(newX, j) != null)
                {
                    if (GetPieceOnSquare(newX, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(newX, newY)))
                    {
                        break;
                    }
                }
            }
        }

        public void BishopMoveLogic(PlayerPiece piece, List<Button> moveLocations)
        {
            Coordinates c = piece.coords;
            int i, j, newX, newY;
            // Move logic...
            // Bishops move diagonally
            newX = c.x + 1;
            newY = c.y + 1;
            for (i = newX, j = newY; i < 8 && j < 8; ++i, ++j)
            {
                if (GetButtonOn(i, j) != null &&
                    GetPieceOnSquare(i, j).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(buttons[i, j]);
                }
                else break;
            }
            newX = c.x - 1;
            newY = c.y - 1;
            for (i = newX, j = newY; i >= 0 && j >= 0; --i, --j)
            {
                if (GetButtonOn(i, j) != null &&
                    GetPieceOnSquare(i, j).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(buttons[i, j]);
                }
                else break;
            }
            newX = c.x + 1;
            newY = c.y - 1;
            for (i = newX, j = newY; i < 8 && j >= 0; ++i, --j)
            {
                if (GetButtonOn(i, j) != null &&
                    GetPieceOnSquare(i, j).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(buttons[i, j]);
                }
                else break;
            }
            newX = c.x - 1;
            newY = c.y + 1;
            for (i = newX, j = newY; i >= 0 && j < 8; --i, ++j)
            {
                if (GetButtonOn(i, j) != null &&
                    GetPieceOnSquare(i, j).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(buttons[i, j]);
                }
                else break;
            }

            // Capture logic
            newX = c.x + 1;
            newY = c.y + 1;
            for (i = newX, j = newY; i < 8 && j < 8; ++i, ++j)
            {
                if (GetButtonOn(i, j) != null)
                {
                    if (GetPieceOnSquare(i, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j)))
                    {
                        break;
                    }
                }
            }

            newX = c.x - 1;
            newY = c.y - 1;
            for (i = newX, j = newY; i >= 0 && j >= 0; --i, --j)
            {
                if (GetButtonOn(i, j) != null)
                {
                    if (GetPieceOnSquare(i, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j)))
                    {
                        break;
                    }
                }
            }

            newX = c.x + 1;
            newY = c.y - 1;
            for (i = newX, j = newY; i < 8 && j >= 0; ++i, --j)
            {
                if (GetButtonOn(i, j) != null)
                {
                    if (GetPieceOnSquare(i, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j)))
                    {
                        break;
                    }
                }
            }

            newX = c.x - 1;
            newY = c.y + 1;
            for (i = newX, j = newY; i >= 0 && j < 8; --i, ++j)
            {
                if (GetButtonOn(i, j) != null)
                {
                    if (GetPieceOnSquare(i, j).color == (whiteTurn ? pieceColor.black : pieceColor.white))
                    {
                        moveLocations.Add(buttons[i, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(i, j)))
                    {
                        break;
                    }
                }
            }
        }

        // TODO : Add En Passant
        public void PawnMoveLogic(PlayerPiece piece, List<Button> moveLocations)
        {
            Coordinates c = piece.coords;
            // First move for pawn
            if (piece.color == pieceColor.white && whiteTurn)
            {
                // Move logic...
                if (GetButtonOn(c.x - 1, c.y) != null &&
                    GetPieceOnSquare(c.x - 1, c.y).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(buttons[c.x - 1, c.y]);

                    // First position only
                    if (c.x == 6 &&
                    GetButtonOn(c.x - 2, c.y) != null &&
                    GetPieceOnSquare(c.x - 2, c.y).piece == pieceString[(int)BoardPiece.NoPiece])
                    {
                        moveLocations.Add(buttons[c.x - 2, c.y]);
                    }
                }

                // Capture logic
                if (GetButtonOn(c.x - 1, c.y - 1) != null &&
                    GetPieceOnSquare(c.x - 1, c.y - 1).color == pieceColor.black)
                {
                    moveLocations.Add(buttons[c.x - 1, c.y - 1]);
                }
                if (GetButtonOn(c.x - 1, c.y + 1) != null &&
                    GetPieceOnSquare(c.x - 1, c.y + 1).color == pieceColor.black)
                {
                    moveLocations.Add(buttons[c.x - 1, c.y + 1]);
                }

            }

            else if (piece.color == pieceColor.black && !whiteTurn)
            {
                // Move logic
                if (GetButtonOn(c.x + 1, c.y) != null &&
                    GetPieceOnSquare(c.x + 1, c.y).piece == pieceString[(int)BoardPiece.NoPiece])
                {
                    moveLocations.Add(buttons[c.x + 1, c.y]);

                    if (c.x == 1 &&
                    GetButtonOn(c.x + 2, c.y) != null &&
                    GetPieceOnSquare(c.x + 2, c.y).piece == pieceString[(int)BoardPiece.NoPiece])
                    {
                        moveLocations.Add(buttons[c.x + 2, c.y]);
                    }
                }

                // Capture logic
                if (GetButtonOn(c.x + 1, c.y - 1) != null &&
                    GetPieceOnSquare(c.x + 1, c.y - 1).color == pieceColor.white)
                {
                    moveLocations.Add(buttons[c.x + 1, c.y - 1]);
                }
                if (GetButtonOn(c.x + 1, c.y + 1) != null &&
                    GetPieceOnSquare(c.x + 1, c.y + 1).color == pieceColor.white)
                {
                    moveLocations.Add(buttons[c.x + 1, c.y + 1]);
                }
            }
        }
    }
}
