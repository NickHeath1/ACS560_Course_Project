using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAttempt
{
    public static class ChessUtils
    {
        public enum CheckState
        {
            NoCheck = -1,
            Check = 0,
            Checkmate = 1,
            Stalemate = 2
        }

        public static class Settings
        {
            public class Color
            {
                // Selection colors
                public static System.Drawing.Color selectedHighlight = System.Drawing.Color.Blue;
                public static System.Drawing.Color darkSquareHighlight = System.Drawing.Color.Green;
                public static System.Drawing.Color lightSquareHighlight = System.Drawing.Color.LimeGreen;

                // Square colors
                public static System.Drawing.Color darkSquareColor = System.Drawing.Color.White;
                public static System.Drawing.Color lightSquareColor = System.Drawing.Color.Gray;
            }         
                      
            public class Image
            {         
                public static System.Drawing.Image WhiteKing = ChessGameAttempt.Properties.Resources.wKing;
                public static System.Drawing.Image WhiteQueen = ChessGameAttempt.Properties.Resources.wQueen;
                public static System.Drawing.Image WhiteRook = ChessGameAttempt.Properties.Resources.wRook;
                public static System.Drawing.Image WhiteKnight = ChessGameAttempt.Properties.Resources.wKnight;
                public static System.Drawing.Image WhiteBishop = ChessGameAttempt.Properties.Resources.wBishop;
                public static System.Drawing.Image WhitePawn = ChessGameAttempt.Properties.Resources.wPawn;
                public static System.Drawing.Image BlackKing = ChessGameAttempt.Properties.Resources.bKing;
                public static System.Drawing.Image BlackQueen = ChessGameAttempt.Properties.Resources.bQueen;
                public static System.Drawing.Image BlackRook = ChessGameAttempt.Properties.Resources.bRook;
                public static System.Drawing.Image BlackKnight = ChessGameAttempt.Properties.Resources.bKnight;
                public static System.Drawing.Image BlackBishop = ChessGameAttempt.Properties.Resources.bBishop;
                public static System.Drawing.Image BlackPawn = ChessGameAttempt.Properties.Resources.bPawn;
            }
        }

        public static string ConvertSecondsToTimeString(int seconds)
        {
            return (seconds > 0) ?
                TimeSpan.FromSeconds(seconds).ToString("mm':'ss") :
                "No set time";

        }
    }
}
