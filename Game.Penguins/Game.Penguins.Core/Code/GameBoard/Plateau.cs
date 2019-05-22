using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Plateau : IBoard
    {
        private int nb1fish = 34;
        private int nb2fish = 20;
        private int nb3fish = 10;
        private readonly List<Cell> allCells = new List<Cell>();
        private readonly List<Cell> allCellsRandom = new List<Cell>();
        public ICell[,] Board { get; }

        /// <summary>
        /// Board constructor, randomly generates a board
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Plateau(int sizeX, int sizeY)
        {
            Board = new ICell[sizeX, sizeY];

            Shuffle();

            // places shuffled cells in the main board
            var n = 0;
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    Board[i, j] = allCellsRandom[n];
                    allCellsRandom[n].XPos = i;
                    allCellsRandom[n].YPos = j;
                    n++;
                }
            }
        }

        /// <summary>
        /// Shuffles the list of fish to be random
        /// </summary>
        private void Shuffle()
        {
            for (int i = 0; i < nb1fish; i++)
            {
                allCells.Add(new Cell(CellType.Fish, 1));
            }
            for (int o = 0; o < nb2fish; o++)
            {
                allCells.Add(new Cell(CellType.Fish, 2));
            }
            for (int p = 0; p < nb3fish; p++)
            {
                allCells.Add(new Cell(CellType.Fish, 3));
            }

            #region Randomise List

            //Randomizes the list of fishes
            Random r = new Random();
            while (allCells.Count > 0)
            {
                var randomIndex = r.Next(0, allCells.Count);
                allCellsRandom.Add(allCells[randomIndex]);
                allCells.RemoveAt(randomIndex);
            }

            #endregion Randomise List
        }
    }
}