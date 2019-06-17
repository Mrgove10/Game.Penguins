using Common.Logging;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Code.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;
using System.Linq;

namespace Game.Penguins.AI.Easy.Code
{
    public class AiEasy : IAi
    {
        private readonly ILog Log = LogManager.GetLogger<AiEasy>();

        //coordinates for placement in the first turn
        public int PlacementPenguinX { get; set; }

        public int PlacementPenguinY { get; set; }

        //board
        public IBoard MainBoard { get; }

        //penguins
        public IPenguin Penguin { get; }

        //for movements
        private readonly int[] _tabDirection = new int[6];

        private readonly MovementVerificationHelper _movementManager;

        public AiEasy(IBoard plateauParam)
        {
            MainBoard = plateauParam;
            _movementManager = new MovementVerificationHelper(MainBoard);
        }

        /// <summary>
        /// Places a penguin randomly on the board
        /// </summary>
        public Coordinates PlacementPenguin()
        {
            Random rnd = new Random();

            while (true) //while it is in a searching state
            {
                Log.Debug("starting the search of a suitable case");
                PlacementPenguinX = rnd.Next(8);
                PlacementPenguinY = rnd.Next(8); //the coordinates are choosen randomly
                ICell c = MainBoard.Board[PlacementPenguinX, PlacementPenguinY];

                if (c.CellType == CellType.Fish && c.FishCount == 1 && c.CurrentPenguin == null) //checks that the placement cell has one fish on it and no penguin
                {
                    Log.Debug("AI will place itself at x: " + PlacementPenguinX + " , y: " + PlacementPenguinY);
                    return new Coordinates()
                    {
                        X = PlacementPenguinX,
                        Y = PlacementPenguinY
                    };
                }
            }
            Log.Error("no cell found");

            return null; //TODO: change this
        }

        /// <summary>
        /// Determines where a penguin can move
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Coordinates ChoseFinalDestinationCell(int posX, int posY)
        {
            var possibleCells = _movementManager.WhereCanIMove((Cell)MainBoard.Board[posX, posY]); //searches for an eligible cell to move to
            if (possibleCells.Any()) //...if there's any, it is immediately choosen...
            {
                while (true)
                {
                    Cell chosenCell = (Cell)possibleCells[new Random().Next(possibleCells.Count)];
                    if (chosenCell.CellType == CellType.Fish)
                    {
                        return new Coordinates() //...and its coordinates replace the origin cell's coordinates.
                        {
                            X = chosenCell.XPos,
                            Y = chosenCell.YPos
                        };
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}