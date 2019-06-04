using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Code.Helper
{
    public class IsolementVerificationHelper
    {
        private bool Isolate = false;
        private readonly IBoard gameBoard = null;

        public IsolementVerificationHelper(IBoard gb)
        {
            gameBoard = gb;
        }

        public bool VerifyIsolate(Cell originCell)
        {
            int x = originCell.XPos;
            int y = originCell.YPos;

            if (y % 2 == 0)//means this is even in the Y axis
            {
                VerifyCells(originCell, 0, -1); //left movement
                VerifyCells(originCell, -1, 0); //left top movement
                VerifyCells(originCell, -1, +1); //right top movement
                VerifyCells(originCell, 0, +1); //right movement
                VerifyCells(originCell, +1, +1); //right bottom movement
                VerifyCells(originCell, +1, 0); //left bottom movement
            }
            else if (y % 2 != 0) //means this is Odd in the Y axis
            {
                VerifyCells(originCell, 0, -1); //left movement
                VerifyCells(originCell, -1, -1); //left top movement
                VerifyCells(originCell, -1, 0); //right top movement
                VerifyCells(originCell, 0, +1); //right movement
                VerifyCells(originCell, +1, 0); //right bottom movement
                VerifyCells(originCell, +1, -1); //left bottom movement
            }

            return Isolate;
        }

        public void VerifyCells(Cell originCell, int xMove, int yMove)
        {
            Cell testedCell = originCell;
            originCell.XPos += xMove;
            originCell.YPos += yMove;

            if (testedCell.CellType == CellType.Fish)
            {
                Isolate = true;
            }
        }
    }
}