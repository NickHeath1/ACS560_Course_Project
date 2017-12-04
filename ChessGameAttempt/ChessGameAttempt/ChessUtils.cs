using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // To connect to the server
        public static string IPAddress = "localhost";
        public static int Port = 2345;
        public static string IPAddressWithPort = "http://localhost:2345/";

        public static class Settings
        {
            public static MoveLogic.Coordinates GetCoordinatesOfButton(Button button)
            {
                MoveLogic.Coordinates c;
                string buttonName = button.Name;
                int x = Convert.ToInt16(buttonName[buttonName.Length - 2].ToString());
                int y = Convert.ToInt16(buttonName[buttonName.Length - 1].ToString());

                c.X = x;
                c.Y = y;

                return c;
            }

            public class Color
            {
                // Selection colors
                public static System.Drawing.Color selectedHighlight = System.Drawing.Color.Blue;
                public static System.Drawing.Color darkSquareHighlight = System.Drawing.Color.Green;
                public static System.Drawing.Color lightSquareHighlight = System.Drawing.Color.LimeGreen;

                // Square colors
                public static System.Drawing.Color darkSquareColor;
                public static System.Drawing.Color lightSquareColor;

                public static void UpdateChessBoardColors(Button[,] board)
                {
                    foreach (Button square in board)
                    {
                        MoveLogic.Coordinates c = GetCoordinatesOfButton(square);
                        square.BackColor = (c.X + c.Y) % 2 == 0 ?
                                ChessUtils.Settings.Color.darkSquareColor :
                                ChessUtils.Settings.Color.lightSquareColor;
                    }
                }
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

                public static void UpdateBoardImages(Button[,] board)
                {
                    foreach (Button square in board)
                    {
                        square.Image = GetImageForTag(square.Tag.ToString());
                    }
                }

                public static System.Drawing.Image GetImageForTag(string tag)
                {
                    switch (tag)
                    {
                        case "wKing":
                            return WhiteKing;
                        case "wQueen":
                            return WhiteQueen;
                        case "wRook":
                            return WhiteRook;
                        case "wBishop":
                            return WhiteBishop;
                        case "wKnight":
                            return WhiteKnight;
                        case "wPawn":
                            return WhitePawn;
                        case "bKing":
                            return BlackKing;
                        case "bQueen":
                            return BlackQueen;
                        case "bRook":
                            return BlackRook;
                        case "bBishop":
                            return BlackBishop;
                        case "bKnight":
                            return BlackKnight;
                        case "bPawn":
                            return BlackPawn;
                        default:
                            return null;
                    }
                }
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
