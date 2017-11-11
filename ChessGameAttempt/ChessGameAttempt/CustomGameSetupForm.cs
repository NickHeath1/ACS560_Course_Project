using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameAttempt
{
    public partial class CustomGameSetupForm : Form
    {
        // Used for storing the custom pictures for pieces (at least temporarily)
        byte[] wKing;
        byte[] bKing;
        byte[] wQueen;
        byte[] bQueen;
        byte[] wRook;
        byte[] bRook;
        byte[] wKnight;
        byte[] bKnight;
        byte[] wBishop;
        byte[] bBishop;
        byte[] wPawn;
        byte[] bPawn;



        List<Button> board = new List<Button>
        {
        };

        public CustomGameSetupForm()
        {
            InitializeComponent();

            // Initialize pieces with custom images
            // If no custom images, initialize with default images

        }

        private void InitializePieceImages()
        {

        }

        public void ButtonClicked(PictureBox pb, byte[] ba)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // TODO: determine if we want a max size for images to send to server

                // Previous image for piece
                Image defImage = pb.BackgroundImage;

                // The image to represent the piece
                Image img = Image.FromFile(dlg.FileName);

                // Set this image to the back image of this square
                pb.BackgroundImage = img;

                // Set the byte array of the appropriate variable
                ba = ImageToByteArray(img);
            }
        }

        public void updateBoardWithNewImages()
        {

        }

        public byte[] ImageToByteArray(Image img)
        {
            return new byte[1];
        }

        public Image ByteArrayToImage(byte[] ary)
        {
            return null;
        }

        private void wKingPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(wKingPic, wKing);
        }

        private void wQueenPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(wQueenPic, wQueen);
        }

        private void wRookPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(wRookPic, wRook);
        }

        private void wPawnPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(wPawnPic, wPawn);
        }

        private void wBishopPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(wBishopPic, wBishop);
        }

        private void wKnightPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(wKnightPic, wKnight);
        }

        private void bRookPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(bRookPic, bRook);
        }

        private void bQueenPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(bQueenPic, bQueen);
        }

        private void bKingPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(bKingPic, bKing);
        }

        private void bPawnPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(bPawnPic, bPawn);
        }

        private void bBishopPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(bBishopPic, bBishop);
        }

        private void bKnightPic_Click(object sender, EventArgs e)
        {
            ButtonClicked(bKnightPic, bKnight);
        }
    }
}
