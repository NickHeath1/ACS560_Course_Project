using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    class CheckConditions : GameSession
    {
        public CheckConditions(string username, string opponentName, TcpClient client) : base(username, opponentName, client)
        {
        }

        CheckCondition getCheckCondition(pieceColor myColor)
        {
            // Check each piece on the board
            foreach(Button square in buttons)
            {
                Coordinates c = GetCoordinatesOfButton(square);
                // If there is actually a piece on this square, AND
                // if the piece is NOT my piece, see if it is attacking my king
                if (GetPieceStringOn(c.X, c.Y) != pieceString[(int)BoardPiece.NoPiece])
                {
                    List<Button> possibleMoves = GetPossibleMovesForPiece(square);
                    foreach (Button move in possibleMoves)
                    {
                        //if ()
                    }
                }
            }
            return CheckCondition.check;
        }
    }
}
