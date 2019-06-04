using Common.Logging;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Helper
{
    public class MovementVerificationHelper
    {
        private readonly ILog _log = LogManager.GetLogger<MovementVerificationHelper>();

        private readonly IBoard _gameBoard;

        public MovementVerificationHelper(IBoard gb)
        {
            _gameBoard = gb;
        }

        public List<Cell> WhereCanIMove(Cell originCell)
        {
            int x = originCell.XPos;
            int y = originCell.YPos;
            List<Cell> possibleCells = new List<Cell>();

            possibleCells.AddRange(VerifyMovementv2(originCell, Direction.Left)); //left movement
            //possibleCells.AddRange(VerifyMovementv2(originCell, Direction.TopLeft)); //left top movement
            //possibleCells.AddRange(VerifyMovementv2(originCell, Direction.TopRight)); //right top movement
            //works possibleCells.AddRange(VerifyMovementv2(originCell, Direction.Right)); //right movement
            //possibleCells.AddRange(VerifyMovementv2(originCell, Direction.BottomRight)); //right bottom movement
           // possibleCells.AddRange(VerifyMovementv2(originCell, Direction.BottomLeft)); //left bottom movement

            _log.Debug("total possible movement cells = " + possibleCells.Count);
            return possibleCells;
        }

        public List<Cell> VerifyMovementv2(Cell originCell, Direction dir)
        {
            List<Cell> possibleCells = new List<Cell>();

            int xMove = 0;
            int yMove = 0;

            if (originCell.YPos % 2 == 0)//means this is even (pair) in the Y axis
            {
                switch (dir)
                {
                    case Direction.Right:
                        xMove = 0;
                        yMove = +1;
                        break;

                    case Direction.BottomRight:
                        xMove = +1;
                        yMove = 0;
                        break;

                    case Direction.BottomLeft:
                        xMove = +1;
                        yMove = -1;
                        break;

                    case Direction.Left:
                        xMove = 0;
                        yMove = -1;
                        break;

                    case Direction.TopLeft:
                        xMove = -1;
                        yMove = -1;
                        break;

                    case Direction.TopRight:
                        xMove = -1;
                        yMove = 0;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
                }
            }
            else if (originCell.YPos % 2 != 0) //means this is Odd in the Y axis
            {
                switch (dir)
                {
                    case Direction.Right:
                        xMove = 0;
                        yMove = +1;
                        break;

                    case Direction.BottomRight:
                        xMove = +1;
                        yMove = +1;
                        break;

                    case Direction.BottomLeft:
                        xMove = +1;
                        yMove = 0;
                        break;

                    case Direction.Left:
                        xMove = 0;
                        yMove = 1;
                        break;

                    case Direction.TopLeft:
                        xMove = -1;
                        yMove = 0;
                        break;

                    case Direction.TopRight:
                        xMove = -1;
                        yMove = +1;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
                }
            }
            // if the next move is still in the board
            if (originCell.XPos + xMove >= 0 && originCell.XPos + xMove <= 7 && originCell.YPos + yMove >= 0 && originCell.YPos + yMove <= 7)
            {
                Cell nextCell = (Cell)_gameBoard.Board[originCell.XPos + xMove, originCell.YPos + yMove];

                if (nextCell.CellType == CellType.Fish)
                {
                    _log.Debug("Adding cell " + nextCell.XPos + "|" + nextCell.YPos);
                    possibleCells.Add(nextCell);
                    possibleCells.AddRange(VerifyMovementv2(nextCell, dir)); //recursive function
                }
                else
                {
                    _log.Debug("Not valid cell");
                }
            }
            else
            {
                _log.Debug("cell is out of range");
            }
            return possibleCells;
        }
    }

    public enum Direction
    {
        Right,
        BottomRight,
        BottomLeft,
        Left,
        TopLeft,
        TopRight
    }
}