using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Helper
{
    public class MovementVerificationHelper
    {
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
                possibleCells.AddRange(VerifyMouvement(originCell, 0, -1)); //left mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, -1, 0)); //left top mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, -1, +1)); //right top mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, 0, +1)); //right mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, +1, +1)); //right botom mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, +1, 0)); //left botom mouvement
            }
            else if (y % 2 != 0) //means this is Odd in the Y axis
            {
                possibleCells.AddRange(VerifyMouvement(originCell, 0, -1)); //left mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, -1, -1)); //left top mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, -1, 0)); //right top mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, 0, +1)); //right mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, +1, 0)); //right botom mouvement
                possibleCells.AddRange(VerifyMouvement(originCell, +1, -1)); //left botom mouvement
            }

            return possibleCells;
        }

        public List<Cell> VerifyMouvement(Cell originCell, int xMove, int yMove)
        {
            List<Cell> possibleCellsRight = new List<Cell>();
            if (originCell.XPos + xMove > 0 && originCell.XPos + xMove < 7 && originCell.YPos + yMove > 0 && originCell.YPos + yMove < 7)
            {
                Cell nextCell = (Cell)gameBoard.Board[originCell.XPos + xMove, originCell.YPos + yMove];
                if (nextCell.CellType == CellType.Fish)
                {
                    possibleCellsRight.Add(nextCell);
                    possibleCellsRight.AddRange(VerifyMouvement(nextCell, xMove, yMove)); //recursive function
                }
            }
            return possibleCellsRight;
        }
    }
}