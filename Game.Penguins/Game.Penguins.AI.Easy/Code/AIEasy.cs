﻿using Common.Logging;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Code.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

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
        public Coordinates PlacementPenguin()
        {
            Random rnd = new Random();
            bool search = true;

            while (search) //while it is in a searching state
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
        /// Determines wheere a penguin can move
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Coordinates ChoseFinalDestinationCell(int posX, int posY)
        {
            var possibleCells = _movementManager.WhereCanIMove((Cell)MainBoard.Board[posX, posY]);
            Cell ChosenCell = possibleCells[new Random().Next(possibleCells.Count)];

            return new Coordinates()
            {
                X = PlacementPenguinX,
                Y = PlacementPenguinY
            };
        }
    }
}