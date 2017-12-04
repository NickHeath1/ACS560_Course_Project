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
using System.Threading;
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

        MoveLogic moveLogic = new MoveLogic();
        //CheckLogic check = new CheckLogic(board);

        static User me;

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
        {
            null,
            ChessUtils.Settings.Image.WhiteKing,
            ChessUtils.Settings.Image.WhiteQueen,
            ChessUtils.Settings.Image.WhiteRook,
            ChessUtils.Settings.Image.WhiteKnight,
            ChessUtils.Settings.Image.WhiteBishop,
            ChessUtils.Settings.Image.WhitePawn,
            ChessUtils.Settings.Image.BlackKing,
            ChessUtils.Settings.Image.BlackQueen,
            ChessUtils.Settings.Image.BlackRook,
            ChessUtils.Settings.Image.BlackKnight,
            ChessUtils.Settings.Image.BlackBishop,
            ChessUtils.Settings.Image.BlackPawn
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
            whiteKingButton.Image =   Images[(int)PieceEnum.WhiteKing];
            whiteQueenButton.Image =  Images[(int)PieceEnum.WhiteQueen];
            whiteRookButton.Image =   Images[(int)PieceEnum.WhiteRook];
            whiteKnightButton.Image = Images[(int)PieceEnum.WhiteKnight];
            whiteBishopButton.Image = Images[(int)PieceEnum.WhiteBishop];
            whitePawnButton.Image =   Images[(int)PieceEnum.WhitePawn];
            blackKingButton.Image =   Images[(int)PieceEnum.BlackKing];
            blackQueenButton.Image =  Images[(int)PieceEnum.BlackQueen];
            blackRookButton.Image =   Images[(int)PieceEnum.BlackRook];
            blackKnightButton.Image = Images[(int)PieceEnum.BlackKnight];
            blackBishopButton.Image = Images[(int)PieceEnum.BlackBishop];
            blackPawnButton.Image =   Images[(int)PieceEnum.BlackPawn];

            // Set up standard layout images
            Image[,] SLI = 
            {
                { Images[(int)PieceEnum.BlackRook], Images[(int)PieceEnum.BlackKnight], Images[(int)PieceEnum.BlackBishop], Images[(int)PieceEnum.BlackQueen], Images[(int)PieceEnum.BlackKing], Images[(int)PieceEnum.BlackBishop], Images[(int)PieceEnum.BlackKnight], Images[(int)PieceEnum.BlackRook] },
                { Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn], Images[(int)PieceEnum.BlackPawn] },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn], Images[(int)PieceEnum.WhitePawn] },
                { Images[(int)PieceEnum.WhiteRook], Images[(int)PieceEnum.WhiteKnight], Images[(int)PieceEnum.WhiteBishop], Images[(int)PieceEnum.WhiteQueen], Images[(int)PieceEnum.WhiteKing], Images[(int)PieceEnum.WhiteBishop], Images[(int)PieceEnum.WhiteKnight], Images[(int)PieceEnum.WhiteRook] }
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
            ChessUtils.Settings.Color.UpdateChessBoardColors(board);

            // Set the no piece button to selected by default
            SetSelectedBorderInvisible();
            selectedButton = PieceEnum.None;
            GetSelectedButtonBorder().Visible = true;
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

        private void whiteKingButton_Click(object sender, EventArgs e)
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
            MoveLogic logic = new MoveLogic();
            logic.ClearChessBoardColors(board);

            foreach (Button square in board)
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
            gdf.ShowDialog();

            ChessUtils.Settings.Image.UpdateBoardImages(board);
            UpdateSelectionButtonImages();
            ChessUtils.Settings.Color.UpdateChessBoardColors(board);
        }

        private void UpdateSelectionButtonImages()
        {
            foreach (Button button in selectionButtons)
            {
                button.Image = ChessUtils.Settings.Image.GetImageForTag(button.Tag.ToString());
            }
        }

        private void saveAndClose_Click(object sender, EventArgs e)
        {
            bool isTurnSelected = false;
            bool isNameEmpty = true;
            bool hasBothKings = false;
            bool areKingsInCheck = true;
            /// Verify form has all valid fields and no kings are in check (turn times are automatically taken care of)
            // Check if the custom game has a name
            isNameEmpty = gameNameBox.Text == "";
            if (isNameEmpty)
            {
                ShowErrorMessage("You must enter a name for this game mode.");
                return;
            }

            // Check if the turns are selected
            isTurnSelected = whiteToMove.Checked || blackToMove.Checked;
            if (!isTurnSelected)
            {
                ShowErrorMessage("You must first select which color moves first on game startup.");
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
                ShowErrorMessage(missingKings);
                return;
            }

            //Verify no kings are in check
            CheckLogic logic = new CheckLogic(board);
            areKingsInCheck = logic.GetCustomGameCheckState() == ChessUtils.CheckState.Check;
            if (areKingsInCheck)
            {
                ShowErrorMessage("One of your kings is in check!");
                return;
            }

            // Add the settings to the database
            List<Piece> pieces = new List<Piece>();
            foreach (Button square in board)
            {
                Piece piece = new Piece();
                Coordinates c = ChessUtils.Settings.GetCoordinatesOfButton(square);
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

            bool success = DataApiController<CustomGame>.PostData(ChessUtils.IPAddressWithPort + "AddCustomGame", game);
            if (!success)
            {
                ShowErrorMessage("Error while adding custom game to database.");
            }
            else
            {
                Close();
            }
        }

        private void ShowErrorMessage(string message)
        {
            // This is a dirty fix for a bug that FOR SOME REASON the messagebox shows behind the form
            // I blame Microsoft
            Visible = false;
            MessageBox.Show(this, message, "Incomplete", MessageBoxButtons.OK);
            Visible = true;
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

        private void updateAttacksButton_Click(object sender, EventArgs e)
        {
            MoveLogic logic = new MoveLogic();
            logic.ClearChessBoardColors(board);
            logic.HighlightAttackedSquares(board);
        }

        private void hideAttackButton_Click(object sender, EventArgs e)
        {
            MoveLogic logic = new MoveLogic();
            logic.ClearChessBoardColors(board);
        }
    }
}
