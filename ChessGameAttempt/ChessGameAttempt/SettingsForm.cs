using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    public partial class SettingsForm : Form
    {
        public User me;

        int redLightComponent;
        int greenLightComponent;
        int blueLightComponent;
        int redDarkComponent;
        int greenDarkComponent;
        int blueDarkComponent;

        Image BlackKing;
        Image BlackQueen;
        Image BlackRook;
        Image BlackBishop;
        Image BlackKnight;
        Image BlackPawn;
        Image WhiteKing;
        Image WhiteQueen;
        Image WhiteRook;
        Image WhiteBishop;
        Image WhiteKnight;
        Image WhitePawn;

        public SettingsForm(User user)
        {
            me = user;
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpUI()
        {
            darkColor.BackColor = ChessUtils.Settings.Color.darkSquareColor;
            lightColor.BackColor = ChessUtils.Settings.Color.lightSquareColor;
            WhiteKingImage.Image = WhiteKing = ChessUtils.Settings.Image.WhiteKing;
            WhiteQueenImage.Image = WhiteQueen = ChessUtils.Settings.Image.WhiteQueen;
            WhiteRookImage.Image = WhiteRook = ChessUtils.Settings.Image.WhiteRook;
            WhiteBishopImage.Image = WhiteBishop = ChessUtils.Settings.Image.WhiteBishop;
            WhiteKnightImage.Image = WhiteKnight = ChessUtils.Settings.Image.WhiteKnight;
            WhitePawnImage.Image = WhitePawn = ChessUtils.Settings.Image.WhitePawn;
            BlackKingImage.Image = BlackKing = ChessUtils.Settings.Image.BlackKing;
            BlackQueenImage.Image = BlackQueen = ChessUtils.Settings.Image.BlackQueen;
            BlackRookImage.Image = BlackRook = ChessUtils.Settings.Image.BlackRook;
            BlackBishopImage.Image = BlackBishop = ChessUtils.Settings.Image.BlackBishop;
            BlackKnightImage.Image = BlackKnight = ChessUtils.Settings.Image.BlackKnight;
            BlackPawnImage.Image = BlackPawn = ChessUtils.Settings.Image.BlackPawn;

            redLightComponent = lightColor.BackColor.R;
            greenLightComponent = lightColor.BackColor.G;
            blueLightComponent = lightColor.BackColor.B;
            redDarkComponent = darkColor.BackColor.R;
            greenDarkComponent = darkColor.BackColor.G;
            blueDarkComponent = darkColor.BackColor.B;

            DR.Value = redDarkComponent;
            DG.Value = greenDarkComponent;
            DB.Value = blueDarkComponent;
            LR.Value = redLightComponent;
            LG.Value = greenLightComponent;
            LB.Value = blueLightComponent;
        }

        private void DR_ValueChanged(object sender, EventArgs e)
        {
            darkColor.BackColor = Color.FromArgb((int)DR.Value, (int)DG.Value, (int)DB.Value);
        }

        private void DG_ValueChanged(object sender, EventArgs e)
        {
            darkColor.BackColor = Color.FromArgb((int)DR.Value, (int)DG.Value, (int)DB.Value);
        }

        private void DB_ValueChanged(object sender, EventArgs e)
        {
            darkColor.BackColor = Color.FromArgb((int)DR.Value, (int)DG.Value, (int)DB.Value);
        }

        private void LR_ValueChanged(object sender, EventArgs e)
        {
            lightColor.BackColor = Color.FromArgb((int)LR.Value, (int)LG.Value, (int)LB.Value);
        }

        private void LG_ValueChanged(object sender, EventArgs e)
        {
            lightColor.BackColor = Color.FromArgb((int)LR.Value, (int)LG.Value, (int)LB.Value);
        }

        private void LB_ValueChanged(object sender, EventArgs e)
        {
            lightColor.BackColor = Color.FromArgb((int)LR.Value, (int)LG.Value, (int)LB.Value);
        }

        private Image GetImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Picture Files (*.PNG)|*.PNG";
            dlg.FilterIndex = 1;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filename = dlg.FileName;

                // Get the image from this filename
                Image image = Image.FromFile(filename);
                if (image.Size.Width != 60 ||
                    image.Size.Height != 60)
                {
                    MessageBox.Show("Image is not of correct size. Please select a 60x60 image.");
                    return null;
                }

                return image;
            }

            return null;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Update the local settings
            ChessUtils.Settings.Color.darkSquareColor = darkColor.BackColor;
            ChessUtils.Settings.Color.lightSquareColor = lightColor.BackColor;

            redLightComponent = lightColor.BackColor.R;
            greenLightComponent = lightColor.BackColor.G;
            blueLightComponent = lightColor.BackColor.B;
            redDarkComponent = darkColor.BackColor.R;
            greenDarkComponent = darkColor.BackColor.G;
            blueDarkComponent = darkColor.BackColor.B;

            // Send the settings to the database
            SquareColorSettings settings = new SquareColorSettings()
            {
                Username = me.Username,
                Red1 = redDarkComponent,
                Green1 = greenDarkComponent,
                Blue1 = blueDarkComponent,
                Red2 = redLightComponent,
                Green2 = greenLightComponent,
                Blue2 = blueLightComponent
            };
            DataApiController<SquareColorSettings>.PostData("http://localhost:2345/EditCustomChessboard", settings);

            ChessUtils.Settings.Image.BlackKing   = BlackKing;
            ChessUtils.Settings.Image.BlackQueen  = BlackQueen;
            ChessUtils.Settings.Image.BlackRook   = BlackRook;
            ChessUtils.Settings.Image.BlackBishop = BlackBishop;
            ChessUtils.Settings.Image.BlackKnight = BlackKnight;
            ChessUtils.Settings.Image.BlackPawn   = BlackPawn;
            ChessUtils.Settings.Image.WhiteKing   = WhiteKing; 
            ChessUtils.Settings.Image.WhiteQueen  = WhiteQueen;
            ChessUtils.Settings.Image.WhiteRook   = WhiteRook;
            ChessUtils.Settings.Image.WhiteBishop = WhiteBishop;
            ChessUtils.Settings.Image.WhiteKnight = WhiteKnight;
            ChessUtils.Settings.Image.WhitePawn   = WhitePawn;

            List<PieceImageSettings> images = new List<PieceImageSettings>()
            {
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "wKing",
                    Image = ImageToByteArray(WhiteKing)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "wQueen",
                    Image = ImageToByteArray(WhiteQueen)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "wRook",
                    Image = ImageToByteArray(WhiteRook)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "wBishop",
                    Image = ImageToByteArray(WhiteBishop)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "wKnight",
                    Image = ImageToByteArray(WhiteKnight)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "wPawn",
                    Image = ImageToByteArray(WhitePawn)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "bKing",
                    Image = ImageToByteArray(BlackKing)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "bQueen",
                    Image = ImageToByteArray(BlackQueen)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "bRook",
                    Image = ImageToByteArray(BlackRook)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "bBishop",
                    Image = ImageToByteArray(BlackBishop)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "bKnight",
                    Image = ImageToByteArray(BlackKnight)
                },
                new PieceImageSettings()
                {
                    Username = me.Username,
                    PieceName = "bPawn",
                    Image = ImageToByteArray(BlackPawn)
                }
            };

            // Send the pictures to the database
            DataApiController<object>.PostData("http://localhost:2345/DeleteUsersCustomPieceImages/" + me.Username, null);
            DataApiController<List<PieceImageSettings>>.PostData("http://localhost:2345/AddCustomPieceImages", images);

            // Close this form
            Close();
        }

        private byte[] ImageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        private void whiteKingButton_Click(object sender, EventArgs e)
        {
            WhiteKing = GetImage();
            WhiteKingImage.Image = WhiteKing;
        }

        private void whiteQueenButton_Click(object sender, EventArgs e)
        {
            WhiteQueen = GetImage();
            WhiteQueenImage.Image = WhiteQueen;
        }

        private void whiteRookButton_Click(object sender, EventArgs e)
        {
            WhiteRook = GetImage();
            WhiteRookImage.Image = WhiteRook;
        }

        private void whiteKnightButton_Click(object sender, EventArgs e)
        {
            WhiteKnight = GetImage();
            WhiteKnightImage.Image = WhiteKnight;
        }

        private void whiteBishopButton_Click(object sender, EventArgs e)
        {
            WhiteBishop = GetImage();
            WhiteBishopImage.Image = WhiteBishop;
        }

        private void whitePawnButton_Click(object sender, EventArgs e)
        {
            WhitePawn = GetImage();
            WhitePawnImage.Image = WhitePawn;
        }

        private void blackKingButton_Click(object sender, EventArgs e)
        {
            BlackKing = GetImage();
            BlackKingImage.Image = BlackKing;
        }

        private void blackQueenButton_Click(object sender, EventArgs e)
        {
            BlackQueen = GetImage();
            BlackQueenImage.Image = BlackQueen;
        }

        private void blackRookButton_Click(object sender, EventArgs e)
        {
            BlackRook = GetImage();
            BlackRookImage.Image = BlackRook;
        }

        private void blackKnightButton_Click(object sender, EventArgs e)
        {
            BlackKnight = GetImage();
            BlackKnightImage.Image = BlackKnight;
        }

        private void blackBishopButton_Click(object sender, EventArgs e)
        {
            BlackBishop = GetImage();
            BlackBishopImage.Image = BlackBishop;
        }

        private void blackPawnButton_Click(object sender, EventArgs e)
        {
            BlackPawn = GetImage();
            BlackPawnImage.Image = BlackPawn;
        }
    }
}
