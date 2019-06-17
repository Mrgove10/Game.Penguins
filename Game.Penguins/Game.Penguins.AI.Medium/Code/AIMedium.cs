using Common.Logging;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Code.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;
using System.Linq;

namespace Game.Penguins.AI.Medium.Code
{
    public class AiMedium : IAi
    {
        #region Definitions

        //tries to go th where there is the most penguins
        private readonly ILog Log = LogManager.GetLogger<AiMedium>();

        public int PlacementPenguinX { get; set; }
        public int PlacementPenguinY { get; set; }

        public IBoard MainBoard { get; }
        public IPenguin Penguin { get; }

        private readonly int[] _tabDirection = new int[6];
        private readonly MovementVerificationHelper _movementManager;

        public AiMedium(IBoard plateauParam)
        {
            MainBoard = plateauParam;
            _movementManager = new MovementVerificationHelper(MainBoard);
        }

        #endregion Definitions

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
                PlacementPenguinY = rnd.Next(8);
                ICell c = MainBoard.Board[PlacementPenguinX, PlacementPenguinY];

                if (c.CellType == CellType.Fish && c.FishCount == 1 && c.CurrentPenguin == null)
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
            var possibleCells = _movementManager.WhereCanIMove((Cell)MainBoard.Board[posX, posY]);
            if (possibleCells.Any())
            {
                var chosenCell = (Cell)possibleCells[new Random().Next(possibleCells.Count)];
                return new Coordinates()
                {
                    X = chosenCell.XPos,
                    Y = chosenCell.YPos
                };
            }
            else
            {
                return null;
            }
        }
    }
}