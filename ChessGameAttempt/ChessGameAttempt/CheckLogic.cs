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
        Button[,] originalBoard;
        MoveLogic logic = new MoveLogic();

        public CheckLogic(Button[,] board)
        {
            checkBoard = new Button[8, 8];
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    // Instantiate the new button on the board, and copy to the board
                    originalBoard = board;
                    checkBoard[i, j] = new Button();
                    CopyControl(board[i,j], checkBoard[i,j]);
                }
            }
        }

        public ChessUtils.CheckState GetCustomGameCheckState()
        {
            List<Button> attackedSquares = logic.UpdateAttackedSquares(originalBoard);
            // Will only return check or noCheck, as that is all we care about in this case
            // Iterate through the attacked squares, and see if a king is on one of the squares
            foreach (Button square in attackedSquares)
            {
                if (square.Tag.ToString().Contains("King"))
                {
                    return ChessUtils.CheckState.Check;
                }
            }

            // No king was found, board appears to be good
            return ChessUtils.CheckState.NoCheck;
        }

        public List<Button> GetCheckedMoves(List<Button> moves)
        {
            foreach (Button move in moves)
            {

            }
            return new List<Button>();
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
    }
}
