using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessGameAttempt.MoveLogic;

namespace ChessGameAttempt
{
    public partial class CreateCustomGameForm : Form
    {
        static int MAX_TURN_TIME_SECONDS = 1200;   // 20 minutes max turn time
        static int MAX_TOTAL_TIME_SECONDS = 3600;  // 1 hour max total time
        static int MIN_TURN_TIME_SECONDS = 5;      // 5 seconds min turn time
        static int MIN_TOTAL_TIME_SECONDS = 60;    // 1 minute min total time

        int totalSeconds;
        int turnSeconds;

        static Button[,] board;
        List<Button> selectionBorders;
        List<Button> selectionButtons;

        MoveLogic moves = new MoveLogic();
        CheckLogic check = new CheckLogic();

        static User me;

        // DEBUG
        static Image WhiteKing = ChessGameAttempt.Properties.Resources.wKing;
        static Image WhiteQueen = ChessGameAttempt.Properties.Resources.wQueen;
        static Image WhiteRook = ChessGameAttempt.Properties.Resources.wRook;
        static Image WhiteKnight = ChessGameAttempt.Properties.Resources.wKnight;
        static Image WhiteBishop = ChessGameAttempt.Properties.Resources.wBishop;
        static Image WhitePawn = ChessGameAttempt.Properties.Resources.wPawn;
        static Image BlackKing = ChessGameAttempt.Properties.Resources.bKing;
        static Image BlackQueen = ChessGameAttempt.Properties.Resources.bQueen;
        static Image BlackRook = ChessGameAttempt.Properties.Resources.bRook;
        static Image BlackKnight = ChessGameAttempt.Properties.Resources.bKnight;
        static Image BlackBishop = ChessGameAttempt.Properties.Resources.bBishop;
        static Image BlackPawn = ChessGameAttempt.Properties.Resources.bPawn;

        enum PieceEnum
        {
            None = 0,
            WhiteKing = 1,
            WhiteQueen = 2,
            WhiteRook = 3,
            WhiteKnight = 4,
            WhiteBishop = 5,
            WhitePawn = 6,
            BlackKing = 7,
            BlackQueen = 8,
            BlackRook = 9,
            BlackKnight = 10,
            BlackBishop = 11,
            BlackPawn = 12
        }

        Image[] Images =
        {   null,
            WhiteKing,
            WhiteQueen,
            WhiteRook,
            WhiteKnight,
            WhiteBishop,
            WhitePawn,
            BlackKing,
            BlackQueen,
            BlackRook,
            BlackKnight,
            BlackBishop,
            BlackPawn
        };

        string[] TagStrings =
        {
            "NoPiece",
            "wKing",
            "wQueen",
            "wRook",
            "wKnight",
            "wBishop",
            "wPawn",
            "bKing",
            "bQueen",
            "bRook",
            "bKnight",
            "bBishop",
            "bPawn"
        };

        string[,] StandardLayoutTags =
        {
            {"bRook", "bKnight", "bBishop", "bQueen", "bKing", "bBishop", "bKnight", "bRook" },
            {"bPawn", "bPawn", "bPawn", "bPawn", "bPawn", "bPawn", "bPawn", "bPawn" },
            {"NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece"},
            {"NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece"},
            {"NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece"},
            {"NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece", "NoPiece"},
            {"wPawn", "wPawn", "wPawn", "wPawn", "wPawn", "wPawn", "wPawn", "wPawn"},
            {"wRook", "wKnight", "wBishop", "wQueen", "wKing", "wBishop", "wKnight", "wRook"}
        };

        Image[,] StandardLayoutImages = new Image[8, 8];

        PieceEnum selectedButton = PieceEnum.None;

        public CreateCustomGameForm(User user)
        {
            me = user;
            InitializeComponent();

            // Set up images to the piece replace buttons
            whiteKingButton.Image =   WhiteKing;
            whiteQueenButton.Image =  WhiteQueen;
            whiteRookButton.Image =   WhiteRook;
            whiteKnightButton.Image = WhiteKnight;
            whiteBishopButton.Image = WhiteBishop;
            whitePawnButton.Image =   WhitePawn;
            blackKingButton.Image =   BlackKing;
            blackQueenButton.Image =  BlackQueen;
            blackRookButton.Image =   BlackRook;
            blackKnightButton.Image = BlackKnight;
            blackBishopButton.Image = BlackBishop;
            blackPawnButton.Image =   BlackPawn;

            // Set up standard layout images
            Image[,] SLI = 
            {
                { BlackRook, BlackKnight, BlackBishop, BlackQueen, BlackKing, BlackBishop, BlackKnight, BlackRook },
                { BlackPawn, BlackPawn, BlackPawn, BlackPawn, BlackPawn, BlackPawn, BlackPawn, BlackPawn },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { WhitePawn, WhitePawn, WhitePawn, WhitePawn, WhitePawn, WhitePawn, WhitePawn, WhitePawn },
                { WhiteRook, WhiteKnight, WhiteBishop, WhiteQueen, WhiteKing, WhiteBishop, WhiteKnight, WhiteRook },
            };
            StandardLayoutImages = SLI;

            // Set up board variable
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

            // Set up borders
            selectionBorders = new List<Button>()
            {
                noPieceSelected,
                whiteKingSelected, whiteQueenSelected, whiteRookSelected, whiteKnightSelected, whiteBishopSelected, whitePawnSelected,
                blackKingSelected, blackQueenSelected, blackRookSelected, blackKnightSelected, blackBishopSelected, blackPawnSelected
            };

            // Set up piece replacement buttons
            selectionButtons = new List<Button>()
            {
                noPieceButton,
                whiteKingButton, whiteQueenButton, whiteRookButton, whiteKnightButton, whiteBishopButton, whitePawnButton,
                blackKingButton, blackQueenButton, blackRookButton, blackKnightButton, blackBishopButton, blackPawnButton
            };

            // Set up button connections on board
            foreach(Button square in board)
            {
                square.Click += new System.EventHandler(this.BoardButtonClicked);
            }

            // Set all "setter" button borders to not visible
            foreach (Button selection in selectionBorders)
            {
                selection.Visible = false;
            }

            setNormalLayout(board);
        }

        private void BoardButtonClicked(object sender, EventArgs e)
        {
            Button square = sender as Button;
            square.Tag = TagStrings[(int)selectedButton];
            square.Image = Images[(int)selectedButton];
        }

        private void SetSelectedBorderInvisible()
        {
            if (GetSelectedButton() != null)
            {
                GetSelectedButtonBorder().Visible = false;
            }
        }

        private void SetSelectedButtonBorderVisible()
        {
            if (GetSelectedButton() != null)
            {
                GetSelectedButtonBorder().Visible = true;
            }
        }

        private Button GetSelectedButton()
        {
            return selectionButtons[(int)selectedButton];
        }

        private Button GetSelectedButtonBorder()
        {
            return selectionBorders[(int)selectedButton];
        }

#pragma warning disable IDE1006 // Naming Styles
        private void whiteKingButton_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.WhiteKing;
            GetSelectedButtonBorder().Visible = true;
        }

        private void whiteQueenButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.WhiteQueen;
            GetSelectedButtonBorder().Visible = true;
        }

        private void whiteRookButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.WhiteRook;
            GetSelectedButtonBorder().Visible = true;
        }

        private void whiteKnightButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.WhiteKnight;
            GetSelectedButtonBorder().Visible = true;
        }

        private void whiteBishopButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.WhiteBishop;
            GetSelectedButtonBorder().Visible = true;
        }

        private void whitePawnButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.WhitePawn;
            GetSelectedButtonBorder().Visible = true;
        }

        private void blackKingButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.BlackKing;
            GetSelectedButtonBorder().Visible = true;
        }

        private void blackQueenButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.BlackQueen;
            GetSelectedButtonBorder().Visible = true;
        }

        private void blackRookButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.BlackRook;
            GetSelectedButtonBorder().Visible = true;
        }

        private void blackKnightButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.BlackKnight;
            GetSelectedButtonBorder().Visible = true;
        }

        private void blackBishopButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.BlackBishop;
            GetSelectedButtonBorder().Visible = true;
        }

        private void blackPawnButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.BlackPawn;
            GetSelectedButtonBorder().Visible = true;
        }

        private void noPieceButton_Click(object sender, EventArgs e)
        {
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.None;
            GetSelectedButtonBorder().Visible = true;
        }

        private void clearBoard_Click(object sender, EventArgs e)
        {
            foreach(Button square in board)
            {
                square.Image = null;
                square.Tag = TagStrings[(int)PieceEnum.None];
            }
        }


        private void setNormalLayout(Button[,] board)
        {
            if (board != null)
            {
                for (int i = 0; i < 8; ++i)
                {
                    for (int j = 0; j < 8; ++j)
                    {
                        board[i, j].Tag = StandardLayoutTags[i, j];
                        board[i, j].Image = StandardLayoutImages[i, j];
                    }
                }
            }
        }

        private void setToNormalLayout_Click(object sender, EventArgs e)
        {
            setNormalLayout(board);
        }

        private void changeSettings_Click(object sender, EventArgs e)
        {
            SettingsForm gdf = new SettingsForm(me);
            gdf.Show();
        }

        private void saveAndClose_Click(object sender, EventArgs e)
        {
            bool isTurnSelected = false;
            bool isNameEmpty = true;
            bool hasBothKings = false;
            bool areKingsNotInCheck = false;
            /// Verify form has all valid fields and no kings are in check (turn times are automatically taken care of)
            // Check if the custom game has a name
            isNameEmpty = gameNameBox.Text == "";
            if (isNameEmpty)
            {
                MessageBox.Show("You must enter a name for this game mode.", "Incomplete", MessageBoxButtons.OK);
                return;
            }

            // Check if the turns are selected
            isTurnSelected = whiteToMove.Checked || blackToMove.Checked;
            if (!isTurnSelected)
            {
                MessageBox.Show("You must first select which color moves first on game startup.", "Incomplete", MessageBoxButtons.OK);
                return;
            }

            // Check to make sure there are both black and white kings on the board
            bool hasWhiteKing = false, hasBlackKing = false;
            foreach (Button square in board)
            {
                if ((string)square.Tag == TagStrings[(int)PieceEnum.WhiteKing])
                {
                    hasWhiteKing = true;
                    if (hasBlackKing)
                    {
                        // Both kings found, exit loop
                        break;
                    }
                }
                else if ((string)square.Tag == TagStrings[(int)PieceEnum.BlackKing])
                {
                    hasBlackKing = true;
                    if (hasWhiteKing)
                    {
                        // Both kings found, exit loop
                        break;
                    }
                }
            }
            hasBothKings = hasBlackKing && hasWhiteKing;
            if (!hasBothKings)
            {
                string missingKings = "";
                if (!hasBlackKing)
                {
                    missingKings += "You are missing the black king";
                    // Both kings missing
                    if (!hasWhiteKing)
                    {
                        missingKings += " and the white king";
                    }
                }
                else if (!hasWhiteKing)
                {
                    missingKings += "You are missing the white king";
                }
                missingKings += ".";
                MessageBox.Show(missingKings, "Incomplete", MessageBoxButtons.OK);
                return;
            }

            //Verify no kings are in check
            // TODO

            // Add the settings to the database
            List<Piece> pieces = new List<Piece>();
            foreach (Button square in board)
            {
                Piece piece = new Piece();
                Coordinates c = GetCoordinates(square);
                string name = square.Tag.ToString();
                piece.Coordinates = c;
                piece.Name = name;

                pieces.Add(piece);
            }

            int gameTimerSeconds = IsPlayerTotalTime.Checked ?
                ((int)totalMins.Value * 60) + ((int)totalSecs.Value) : 0;

            int moveTimerSeconds = IsPlayerTurnTime.Checked ?
                ((int)turnMins.Value * 60) + ((int)turnSecs.Value) : 0;

            CustomGame game = new CustomGame
            {
                Username = me.Username,
                GameTimer = gameTimerSeconds,
                MoveTimer = moveTimerSeconds,
                WhiteMovesFirst = whiteToMove.Checked,
                CustomGameName = gameNameBox.Text,
                Pieces = pieces
            };

            string json = JsonConvert.SerializeObject(game);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:2345/AddCustomGame");
            request.SendChunked = true;
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(json);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                }

                // Close the form
                Close();
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response == null)
                {
                    MessageBox.Show("An error occurred while attempting to save your custom game.");
                }
                else
                {
                    MessageBox.Show("Error communicating with server. Please try again later.", "Server error");
                }
            }
        }

        private void closeWithoutSaving_Click(object sender, EventArgs e)
        {
            // Just close the window
            Close();
        }

        private void totalMins_ValueChanged(object sender, EventArgs e)
        {
            totalSeconds = (int)totalMins.Value * 60 + (int)totalSecs.Value;
            if (turnSeconds < totalSeconds)
            {
                if (totalSeconds > MAX_TOTAL_TIME_SECONDS)
                {
                    totalMins.Value = 60;
                    totalSecs.Value = 0;
                }
                else if (totalSeconds < MIN_TOTAL_TIME_SECONDS)
                {
                    totalMins.Value = 1;
                    totalSecs.Value = 0;
                }
            }
            else
            {
                turnSeconds = totalSeconds;
                int mins = (int)((float)turnSeconds / 60.0);
                int secs = turnSeconds % 60;
                turnMins.Value = mins;
                turnSecs.Value = secs;
            }
        }

        private void totalSecs_ValueChanged(object sender, EventArgs e)
        {
            totalSeconds = (int)totalMins.Value * 60 + (int)totalSecs.Value;
            if (turnSeconds < totalSeconds)
            {
                if (totalSeconds > MAX_TOTAL_TIME_SECONDS)
                {
                    totalMins.Value = 60;
                    totalSecs.Value = 0;
                }
                else if (totalSeconds < MIN_TOTAL_TIME_SECONDS)
                {
                    totalMins.Value = 1;
                    totalSecs.Value = 0;
                }
            }
            else
            {
                turnSeconds = totalSeconds;
                int mins = (int)((float)turnSeconds / 60.0);
                int secs = turnSeconds % 60;
                turnMins.Value = mins;
                turnSecs.Value = secs;
            }
        }

        private void turnMins_ValueChanged(object sender, EventArgs e)
        {
            turnSeconds = (int)turnMins.Value * 60 + (int)turnSecs.Value;
            if (turnSeconds < totalSeconds)
            {
                if (turnSeconds > MAX_TURN_TIME_SECONDS)
                {
                    turnMins.Value = 20;
                    turnSecs.Value = 0;
                }
                else if (turnSeconds < MIN_TOTAL_TIME_SECONDS)
                {
                    turnMins.Value = 0;
                    turnSecs.Value = 5;
                }
            }
            else
            {
                turnSeconds = totalSeconds;
                int mins = (int)((float)turnSeconds / 60.0);
                int secs = turnSeconds % 60;
                turnMins.Value = mins;
                turnSecs.Value = secs;
            }
        }

        private void turnSecs_ValueChanged(object sender, EventArgs e)
        {
            turnSeconds = (int)turnMins.Value * 60 + (int)turnSecs.Value;
            if (turnSeconds < totalSeconds)
            {
                if (turnSeconds > MAX_TURN_TIME_SECONDS)
                {
                    turnMins.Value = 60;
                    turnSecs.Value = 0;
                }
                else if (turnSeconds < MIN_TOTAL_TIME_SECONDS)
                {
                    turnMins.Value = 0;
                    turnSecs.Value = 5;
                }
            }
            else
            {
                turnSeconds = totalSeconds;
                int mins = (int)((float)turnSeconds / 60.0);
                int secs = turnSeconds % 60;
                turnMins.Value = mins;
                turnSecs.Value = secs;
            }
        }

        public Coordinates GetCoordinates(Button square)
        {
            Coordinates c;
            string buttonName = square.Name;
            int x = Convert.ToInt16(buttonName[buttonName.Length - 2].ToString());
            int y = Convert.ToInt16(buttonName[buttonName.Length - 1].ToString());

            c.X = x;
            c.Y = y;

            return c;
        }
    }
}
