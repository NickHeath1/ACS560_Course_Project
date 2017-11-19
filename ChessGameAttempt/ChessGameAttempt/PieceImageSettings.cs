using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAttempt
{
    public class PieceImageSettings
    {
        public int ID;
        public string Username;
        public string PieceName;
        public byte[] Image;
    }

    public class AllPieceImages
    {
        public PieceImageSettings whitePawn;
        public PieceImageSettings blackPawn;
        public PieceImageSettings whiteKnight;
        public PieceImageSettings blackKnight;
        public PieceImageSettings whiteBishop;
        public PieceImageSettings blackBishop;
        public PieceImageSettings whiteRook;
        public PieceImageSettings blackRook;
        public PieceImageSettings whiteQueen;
        public PieceImageSettings blackQueen;
        public PieceImageSettings whiteKing;
        public PieceImageSettings blackKing;
    }
}
