using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessGameAttempt.MoveLogic;

namespace ChessGameAttempt
{
    public class CustomGame
    {
        public int GameID;
        public string Username;
        public int GameTimer;
        public int MoveTimer;
        public bool WhiteMovesFirst;
        public string CustomGameName;
        public List<Piece> Pieces;
    }
}
