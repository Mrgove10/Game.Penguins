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

        /// <summary>
        /// Calculates the possible Cells to move to from a certain possition
        /// </summary>
        /// <param name="TheOriginalCell">The original cell you want to calculate the posibilities from</param>
        /// <returns>A list of all the possible cells the player can move to relative to teh origine cell</returns>
        public List<Cell> WhereCanIMove(Cell TheOriginalCell)
        {
            int x = TheOriginalCell.XPos;
            int y = TheOriginalCell.YPos;
            List<Cell> possibleCells = new List<Cell>();

            possibleCells.AddRange(VerifyMovementv2(TheOriginalCell, Direction.Left)); //left movement
            possibleCells.AddRange(VerifyMovementv2(TheOriginalCell, Direction.TopLeft)); //left top movement
            possibleCells.AddRange(VerifyMovementv2(TheOriginalCell, Direction.TopRight)); //right top movement
            possibleCells.AddRange(VerifyMovementv2(TheOriginalCell, Direction.Right)); //right movement
            possibleCells.AddRange(VerifyMovementv2(TheOriginalCell, Direction.BottomRight)); //right bottom movement
            possibleCells.AddRange(VerifyMovementv2(TheOriginalCell, Direction.BottomLeft)); //left bottom movement

            _log.Debug("total possible movement cells = " + possibleCells.Count);
            return possibleCells;
        }

        /// <summary>
        /// Verrifies the celll possible to move to
        /// </summary>
        /// <param name="originCell"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public List<Cell> VerifyMovementv2(Cell originCell, Direction dir)
        {
            List<Cell> possibleCells = new List<Cell>();

            int xMove = 0;
            int yMove = 0;

            if (originCell.XPos % 2 == 0) //means this is even (pair) in the Y axis
            {
                switch (dir)
                {
                    case Direction.Right:
                        xMove = 0;
                        yMove = +1;
                        _log.Debug("2 Droite");
                        break;

                    case Direction.BottomRight:
                        xMove = +1;
                        yMove = 0;
                        _log.Debug("2 bas droite");
                        break;

                    case Direction.BottomLeft:
                        xMove = +1;
                        yMove = -1;
                        _log.Debug("2 bas gauche");
                        break;

                    case Direction.Left:
                        xMove = 0;
                        yMove = -1;
                        _log.Debug("2 gauche");
                        break;

                    case Direction.TopLeft:
                        xMove = -1;
                        yMove = -1;
                        _log.Debug("2 haut gauche");
                        break;

                    case Direction.TopRight:
                        xMove = -1;
                        yMove = 0;
                        _log.Debug("2 haut droite");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
                }
            }
            else if (originCell.XPos % 2 != 0) //means this is Odd in the Y axis
            {
                switch (dir)
                {
                    case Direction.Right:
                        xMove = 0;
                        yMove = +1;
                        _log.Debug("1 Droite");
                        break;

                    case Direction.BottomRight:
                        xMove = +1;
                        yMove = +1;
                        _log.Debug("1 bas Droite");
                        break;

                    case Direction.BottomLeft:
                        xMove = +1;
                        yMove = 0;
                        _log.Debug("1 bas gauche");
                        break;

                    case Direction.Left:
                        xMove = 0;
                        yMove = -1;
                        _log.Debug("1 gauche");
                        break;

                    case Direction.TopLeft:
                        xMove = -1;
                        yMove = 0;
                        _log.Debug("1 haut gauche");
                        break;

                    case Direction.TopRight:
                        xMove = -1;
                        yMove = +1;
                        _log.Debug("1 haut droite");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
                }
            }

            // if the next move is still in the board
            if (originCell.XPos + xMove >= 0 && originCell.XPos + xMove <= _gameBoard.Board.GetUpperBound(0) &&
                originCell.YPos + yMove >= 0 && originCell.YPos + yMove <= _gameBoard.Board.GetUpperBound(0))
            {
                Cell nextCell = (Cell)_gameBoard.Board[originCell.XPos + xMove, originCell.YPos + yMove];
                if (nextCell != originCell)
                {
                    if (nextCell.CellType != CellType.Fish)
                    {
                        //log.Debug("stopping to add cells");
                        // possibleCells.Add(null);
                        //VerifyMovementv2(nextCell, dir);
                        // //todo : not possible to return null (cause recurtion)
                    }
                    else
                    {
                        _log.Debug("Adding cell");
                        possibleCells.Add(nextCell);
                        possibleCells.AddRange(VerifyMovementv2(nextCell, dir)); //recursive function
                    }
                }
                else
                {
                    _log.Warn("this is the same cell as the origin cell");
                }
            }
            else
            {
                _log.Warn("cell is out of range");
            }

            return possibleCells;
        }
    }

    /// <summary>
    /// Direction possible by the player
    /// </summary>
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