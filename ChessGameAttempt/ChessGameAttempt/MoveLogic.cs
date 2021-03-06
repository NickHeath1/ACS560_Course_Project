﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        // bools to determine which player's turn it is
        public pieceColor myColor = pieceColor.noColor;
        public bool myTurn = false;
        public bool gameStarted = false;

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
            public pieceColor Color;
            public string Name;
            public Button Button;
            public Coordinates Coordinates;
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

        public bool IsMyPiece(Piece piece1, Piece piece2)
        {
            return piece1.Color == piece2.Color;
        }

        public bool IsEnemyPiece(Piece piece1, Piece piece2)
        {
            return
            piece1.Color == pieceColor.white && piece2.Color == pieceColor.black ||
            piece1.Color == pieceColor.black && piece2.Color == pieceColor.white;
        }

        public bool IsPieceOn(int x, int y, Button[,] board)
        {
            return GetPieceStringOn(x, y, board) != "NoPiece" &&
                GetPieceStringOn(x, y, board) != "";
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
                piece.Button = board[x, y];
                piece.Color = board[x, y].Tag.ToString()[0] == 'w' ? pieceColor.white : pieceColor.black;
                piece.Coordinates.X = x;
                piece.Coordinates.Y = y;
                piece.Name = board[x, y].Tag.ToString().Substring(1);
            }
            else
            {
                piece.Name = pieceString[(int)BoardPiece.NoPiece];
            }
            return piece;
        }

        // Used for King/Knight movements (DRY)
        public void CheckOffset(Piece piece, int xInc, int yInc, Button[,] board, List<Button> locations)
        {
            int newX, newY;
            Button square;
            Coordinates c = piece.Coordinates;

            newX = c.X + xInc;
            newY = c.Y + yInc;
            square = GetButtonOn(newX, newY, board);
            if (square != null && !IsMyPiece(GetPieceOnSquare(c.X, c.Y, board), GetPieceOnSquare(newX, newY, board)))
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
                    board[i, j].BackColor = (i + j) % 2 == 0 ?
                        ChessUtils.Settings.Color.darkSquareColor :
                        ChessUtils.Settings.Color.lightSquareColor;
                }
            }
        }

        public void HighlightAttackedSquares(Button[,] board)
        {
            List<Button> attackedSquares = UpdateAttackedSquares(board);
            HighlightButtons(attackedSquares);
        }

        public void HighlightButtons(List<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                Coordinates c = ChessUtils.Settings.GetCoordinatesOfButton(button);
                button.BackColor = (c.X + c.Y) % 2 == 0 ? colorHighlight1 : colorHighlight2;
            }
        }

        public void HighlightSelectedButton(Button button)
        {
            button.BackColor = currentHighlight;
        }

        public List<Button> GetAllAvailableMovesForColor(pieceColor color, Button[,] board)
        {
            List<Button> locations = new List<Button>();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    Piece piece = GetPieceOnSquare(i, j, board);
                    if (piece.Color == color)
                    {
                        switch (piece.Name)
                        {
                            case "Pawn":
                                PawnAttackLogic(piece, locations, board);
                                PawnAttackLogic(piece, locations, board);
                                break;

                            case "Knight":
                                KnightMoveLogic(piece, locations, board);
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

                            case "King":
                                KingMoveLogic(piece, locations, board);
                                break;
                        }
                    }
                }
            }
            return locations;
        }

        public List<Button> GetAllPossibleCheckMovesForColor(pieceColor color, Button[,] board)
        {
            List<Button> locations = new List<Button>();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (GetPieceOnSquare(i, j, board).Color == color)
                    {
                        GetPossibleCheckedMovesForPiece(board[i, j], board);
                    }
                }
            }

            return locations;
        }

        public List<Button> GetPossibleCheckedMovesForPiece(Button button, Button[,] board)
        {
            // Get all possible moves for piece, even if in check
            List<Button> locations = GetPossibleMovesForPiece(button, board);
            Coordinates c = ChessUtils.Settings.GetCoordinatesOfButton(button);
            CheckLogic checkLogic;
            Piece piece = GetPieceOnSquare(c.X, c.Y, board);

            // Go through and remove moves that would put the king in check
            foreach (Button location in locations)
            {
                // Get coordinates of new location
                Coordinates c2 = ChessUtils.Settings.GetCoordinatesOfButton(button);

                // Copy the board over to my new board...
                Button[,] newBoard = CopyBoard(board);

                Button source = newBoard[c.X, c.Y];
                Button destination = newBoard[c2.X, c2.Y];

                // Get the check state if I move the piece to the new location
                MovePieceToNewSquare(source, destination, newBoard);

                // Check logic for updated board
                checkLogic = new CheckLogic(newBoard);
                ChessUtils.CheckState checkState = checkLogic.GetCustomGameCheckState();

                // Determine whether or not to keep this location
                if (checkState == ChessUtils.CheckState.Check)
                {
                    locations.Remove(location);
                }
            }

            return locations;
        }

        private void MovePieceToNewSquare(Button source, Button destination, Button[,] board)
        {
            destination.Image = source.Image;
            destination.Tag = source.Tag;

            source.Image = null;
            source.Tag = "NoPiece";
        }

        private Button[,] CopyBoard(Button[,] sourceBoard)
        {
            Button[,] newBoard = new Button[8, 8];
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    CopyControl(sourceBoard[i, j], newBoard[i,j]);
                }
            }

            return newBoard;
        }

        private void CopyControl(Control sourceControl, Control targetControl)
        {
            // make sure these are the same
            if (sourceControl.GetType() != targetControl.GetType())
            {
                throw new Exception("Incorrect control types");
            }

            foreach (PropertyInfo sourceProperty in sourceControl.GetType().GetProperties())
            {
                object newValue = sourceProperty.GetValue(sourceControl, null);

                MethodInfo mi = sourceProperty.GetSetMethod(true);
                if (mi != null)
                {
                    sourceProperty.SetValue(targetControl, newValue, null);
                }
            }
        }

        public List<Button> GetPossibleMovesForPiece(Button button, Button[,] board)
        {
            List<Button> locations = new List<Button>();
            if (myTurn && gameStarted)
            {
                // Clear potential moves from before
                ClearChessBoardColors(board);

                // Highlight new locations
                Coordinates c = ChessUtils.Settings.GetCoordinatesOfButton(button);
                Piece piece = GetPieceOnSquare(c.X, c.Y, board);

                // If there is a piece on the selected square that is the color of the current player...
                bool isMyPiece = myColor == piece.Color;

                if (piece.Name != pieceString[(int)BoardPiece.NoPiece] && isMyPiece)
                {
                    // Highlight the clicked button
                    HighlightSelectedButton(button);

                    string stringOfPiece = GetPieceStringOn(c.X, c.Y, board);

                    switch (stringOfPiece.Substring(1))
                    {
                        case "Pawn":
                            PawnMoveLogic(piece, locations, board);
                            PawnAttackLogic(piece, locations, board);
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

                    switch (piece.Name)
                    {
                        case "Pawn":
                            PawnAttackLogic(piece, attackedSquares, board);
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
            bool isMyPiece;
            Coordinates c = piece.Coordinates;
            int i, j, newX, newY;

            // DRY cannot be applied here
            newX = c.X + 1;
            newY = c.Y;
            for (i = newX; i < 8; ++i)
            {
                if (GetButtonOn(i, newY, board) != null)
                {
                    // Piece on square is black = add to attacks, but cannot go past that piece
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, newY, board)))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    // Piece on square is my piece = cannot move to that spot or beyond
                    else if (IsMyPiece(piece, GetPieceOnSquare(i, newY, board)))
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
                    // Piece on square is black = add to attacks, but cannot go past that piece
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, newY, board)))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    // Piece on square is my piece = cannot move to that spot or beyond
                    else if (IsMyPiece(piece, GetPieceOnSquare(i, newY, board)))
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
                    // Piece on square is black = add to attacks, but cannot go past that piece
                    if (IsEnemyPiece(piece, GetPieceOnSquare(newX, j, board)))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    // Piece on square is my piece = cannot move to that spot or beyond
                    else if (IsMyPiece(piece, GetPieceOnSquare(newX, j, board)))
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
                    // Piece on square is black = add to attacks, but cannot go past that piece
                    if (IsEnemyPiece(piece, GetPieceOnSquare(newX, j, board)))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    // Piece on square is my piece = cannot move to that spot or beyond
                    else if (IsMyPiece(piece, GetPieceOnSquare(newX, j, board)))
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
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, newY, board)))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(piece, GetPieceOnSquare(i, newY, board)))
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
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, newY, board)))
                    {
                        moveLocations.Add(board[i, newY]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(c.X, c.Y, board), GetPieceOnSquare(i, newY, board)))
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
                    if (IsEnemyPiece(piece, GetPieceOnSquare(newX, j, board)))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(c.X, c.Y, board), GetPieceOnSquare(newX, j, board)))
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
                    if (IsEnemyPiece(piece, GetPieceOnSquare(newX, j, board)))
                    {
                        moveLocations.Add(board[newX, j]);
                        break;
                    }

                    else if (IsMyPiece(GetPieceOnSquare(c.X, c.Y, board), GetPieceOnSquare(newX, j, board)))
                    {
                        break;
                    }
                }
            }
        }

        public void BishopMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            Coordinates c = piece.Coordinates;
            int i, j, newX, newY;
            // Move logic...
            // Bishops move diagonally
            newX = c.X + 1;
            newY = c.Y + 1;
            for (i = newX, j = newY; i < 8 && j < 8; ++i, ++j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }

                    else
                    {
                        moveLocations.Add(board[i, j]);
                    }
                }
                else
                {
                    break;
                }
            }
            newX = c.X - 1;
            newY = c.Y - 1;
            for (i = newX, j = newY; i >= 0 && j >= 0; --i, --j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }

                    else
                    {
                        moveLocations.Add(board[i, j]);
                    }
                }
                else
                {
                    break;
                }
            }
            newX = c.X + 1;
            newY = c.Y - 1;
            for (i = newX, j = newY; i < 8 && j >= 0; ++i, --j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }

                    else
                    {
                        moveLocations.Add(board[i, j]);
                    }
                }
                else
                {
                    break;
                }
            }
            newX = c.X - 1;
            newY = c.Y + 1;
            for (i = newX, j = newY; i >= 0 && j < 8; --i, ++j)
            {
                if (GetButtonOn(i, j, board) != null)
                {
                    if (IsEnemyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        moveLocations.Add(board[i, j]);
                        break;
                    }

                    else if (IsMyPiece(piece, GetPieceOnSquare(i, j, board)))
                    {
                        break;
                    }

                    else
                    {
                        moveLocations.Add(board[i, j]);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void PawnAttackLogic(Piece piece, List<Button> attackLocations, Button[,] board)
        {
            Coordinates c = piece.Coordinates;
            if (piece.Color == pieceColor.white)
            {
                if (GetButtonOn(c.X - 1, c.Y - 1, board) != null)
                {
                    if (IsPieceOn(c.X - 1, c.Y - 1, board) &&
                        IsEnemyPiece(piece, GetPieceOnSquare(c.X - 1, c.Y - 1, board)))
                    {
                        // There is an attackable enemy piece, add it
                        attackLocations.Add(board[c.X - 1, c.Y - 1]);
                    }
                }
                if (GetButtonOn(c.X - 1, c.Y + 1, board) != null)
                {
                    if (IsPieceOn(c.X - 1, c.Y + 1, board) &&
                        IsEnemyPiece(piece, GetPieceOnSquare(c.X - 1, c.Y + 1, board)))
                    {
                        // There is an attackable enemy piece, add it
                        attackLocations.Add(board[c.X - 1, c.Y + 1]);
                    }
                }
            }
            else if (piece.Color == pieceColor.black)
            {
                if (GetButtonOn(c.X + 1, c.Y - 1, board) != null)
                {
                    if (IsPieceOn(c.X + 1, c.Y - 1, board) &&
                        IsEnemyPiece(piece, GetPieceOnSquare(c.X + 1, c.Y - 1, board)))
                    {
                        // There is an attackable enemy piece, add it
                        attackLocations.Add(board[c.X + 1, c.Y - 1]);
                    }
                }
                if (GetButtonOn(c.X + 1, c.Y + 1, board) != null)
                {
                    if (IsPieceOn(c.X + 1, c.Y + 1, board) &&
                        IsEnemyPiece(piece, GetPieceOnSquare(c.X + 1, c.Y + 1, board)))
                    {
                        // There is an attackable enemy piece, add it
                        attackLocations.Add(board[c.X + 1, c.Y + 1]);
                    }
                }
            }
        }

        public void PawnMoveLogic(Piece piece, List<Button> moveLocations, Button[,] board)
        {
            Coordinates c = piece.Coordinates;
            if (piece.Color == pieceColor.white)
            {
                // Move logic...
                if (GetButtonOn(c.X - 1, c.Y, board) != null &&
                    !IsPieceOn(c.X - 1, c.Y, board))
                {
                    moveLocations.Add(board[c.X - 1, c.Y]);

                    // First position only
                    if (c.X == 6 &&
                    GetButtonOn(c.X - 2, c.Y, board) != null &&
                    !IsPieceOn(c.X - 2, c.Y, board))
                    {
                        moveLocations.Add(board[c.X - 2, c.Y]);
                    }
                }
            }

            else if (piece.Color == pieceColor.black)
            {
                // Move logic
                if (GetButtonOn(c.X + 1, c.Y, board) != null &&
                    !IsPieceOn(c.X + 1, c.Y, board))
                {
                    moveLocations.Add(board[c.X + 1, c.Y]);

                    // First move
                    if (c.X == 1 &&
                    GetButtonOn(c.X + 2, c.Y, board) != null &&
                    !IsPieceOn(c.X + 2, c.Y, board))
                    {
                        moveLocations.Add(board[c.X + 2, c.Y]);
                    }
                }
            }
        }
    }
}
