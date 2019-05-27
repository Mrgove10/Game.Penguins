using Common.Logging;
using Game.Penguins.Core;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;
using System.Collections.Generic;

namespace Game.Penguins.AI.Code
{
    public class AiEasy : IAI
    {
        private readonly ILog Log = LogManager.GetLogger<AiEasy>();
        public int PlacementPenguinX { get; set; }
        public int PlacementPenguinY { get; set; }

        public IBoard MainBoard { get; }
        public IPenguin Penguin { get; }

        private readonly int[] _tabDirection = new int[6];
        private MovementVerificationHelper _movementManager;

        public AiEasy(IBoard plateauParam)
        {
            MainBoard = plateauParam;
            _movementManager = new MovementVerificationHelper(MainBoard);
        }

        /// <summary>
        /// Places a penguin randomly on the board
        /// </summary>
        public List<int> PlacementPenguin()
        {
            Random rnd = new Random();
            bool search = true;

            while (search) //while it is in a searching state
            {
                Log.Debug("starting the search of a suitable case");
                PlacementPenguinX = rnd.Next(7);
                PlacementPenguinY = rnd.Next(7);
                ICell c = MainBoard.Board[PlacementPenguinX, PlacementPenguinY];

                if (c.CellType == CellType.Fish && c.FishCount == 1 && c.CurrentPenguin == null)
                {
                    List<int> tab = new List<int>(2)
                    {
                        [0] = PlacementPenguinX,
                        [1] = PlacementPenguinY
                    };
                    search = false;
                    Log.Debug("AI will place itself at x: " + PlacementPenguinX + " , y: " + PlacementPenguinY);
                    return tab;
                }
            }
            Log.Error("no cell found");
            return null; //TODO: change this
        }

        /// <summary>
        /// Determines wheere a penguin can move
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public List<int> DetectionCases(int posX, int posY)
        {
            var possibleCells = _movementManager.WhereCanIMove((Cell)MainBoard.Board[posX, posY]);
            Cell ChosenCell = possibleCells[new Random().Next(possibleCells.Count)];

            List<int> tab = new List<int>(2)
            {
                [0] = PlacementPenguinX,
                [1] = PlacementPenguinY
            };
            return tab;
        }
    }
}