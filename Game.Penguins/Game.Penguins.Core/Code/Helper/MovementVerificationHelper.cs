using Common.Logging;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Helper
{
    public class MovementVerificationHelper
    {
        private readonly ILog _log = LogManager.GetLogger<MovementVerificationHelper>();

        private readonly IBoard gameBoard = null;

        public MovementVerificationHelper(IBoard gb)
        {
            gameBoard = gb;
        }

        public List<Cell> WhereCanIMove(Cell originCell)
        {
            int x = originCell.XPos;
            int y = originCell.YPos;
            List<Cell> possibleCells = new List<Cell>();

            if (y % 2 == 0)//means this is even in the Y axis
            {
                possibleCells.AddRange(VerifyMovement(originCell, 0, -1)); //left movement
                possibleCells.AddRange(VerifyMovement(originCell, -1, 0)); //left top movement
                possibleCells.AddRange(VerifyMovement(originCell, -1, +1)); //right top movement
                possibleCells.AddRange(VerifyMovement(originCell, 0, +1)); //right movement
                possibleCells.AddRange(VerifyMovement(originCell, +1, +1)); //right bottom movement
                possibleCells.AddRange(VerifyMovement(originCell, +1, 0)); //left bottom movement
            }
            else if (y % 2 != 0) //means this is Odd in the Y axis
            {
                possibleCells.AddRange(VerifyMovement(originCell, 0, -1)); //left movement
                possibleCells.AddRange(VerifyMovement(originCell, -1, -1)); //left top movement
                possibleCells.AddRange(VerifyMovement(originCell, -1, 0)); //right top movement
                possibleCells.AddRange(VerifyMovement(originCell, 0, +1)); //right movement
                possibleCells.AddRange(VerifyMovement(originCell, +1, 0)); //right bottom movement
                possibleCells.AddRange(VerifyMovement(originCell, +1, -1)); //left bottom movement
                possibleCells.AddRange(VerifyMovement(originCell, +1, -1)); //left bottom movement
            }
            _log.Debug("total possible movement cells = " + possibleCells.Count);
            return possibleCells;
        }

        public List<Cell> VerifyMovement(Cell originCell, int xMove, int yMove)
        {
            List<Cell> possibleCellsRight = new List<Cell>();
            if (originCell.XPos + xMove >= 0 && originCell.XPos + xMove <= 7 && originCell.YPos + yMove >= 0 && originCell.YPos + yMove <= 7)
            {
                Cell nextCell = (Cell)gameBoard.Board[originCell.XPos + xMove, originCell.YPos + yMove];

                if (nextCell.CellType == CellType.Fish)
                {
                    possibleCellsRight.Add(nextCell);
                    possibleCellsRight.AddRange(VerifyMovement(nextCell, xMove, yMove)); //recursive function
                }
            }
            return possibleCellsRight;
        }

        //todo: make a fucntion that can do and X and Y  whit a certain X and y , i dont understand shit 
    }
}