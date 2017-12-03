using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessGameAttempt.MoveLogic;

namespace ChessGameAttempt
{
    public class CheckLogic
    {
        Button[,] checkBoard;

        public CheckLogic(Button[,] board)
        {
            checkBoard = new Button[8, 8];
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    // Instantiate the new button on the board
                    checkBoard[i, j] = new Button();

                    // Copy the board
                    CopyControl(board[i,j], checkBoard[i,j]);
                }
            }
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

        public List<Button> GetCheckedMoves(List<Button> moves)
        {
            return new List<Button>();
        }

        public Button GetTheTopLeftButton()
        {
            return checkBoard[0, 0];
        }
    }
}
