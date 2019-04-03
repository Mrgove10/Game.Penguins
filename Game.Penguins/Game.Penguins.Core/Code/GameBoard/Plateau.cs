using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Plateau : IBoard
    {
        private int TotalCells = 64;
        private int nb1fish = 34;
        private int nb2fish = 20;
        private int nb3fish = 10;
        private List<Cell> AllCells = new List<Cell>();
        private List<Cell> AllCellsRandom = new List<Cell>();
        public ICell[,] Board { get; }

        /// <summary>
        /// Board construtor, randomly generates a board
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Plateau(int sizeX, int sizeY)
        {
            Board = new ICell[sizeX, sizeY];

            shuffle();

            // places shuffled cells in the main board
            var n = 0;
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    Board[i, j] = AllCellsRandom[n];
                    n++;
                }
            }
        }

        /// <summary>
        /// Shuffles the listof fish to be random
        /// </summary>
        private void shuffle()
        {
            for (int i = 0; i < nb1fish; i++)
            {
                AllCells.Add(new Cell(CellType.Fish, 1));
            }
            for (int o = 0; o < nb2fish; o++)
            {
                AllCells.Add(new Cell(CellType.Fish, 2));
            }
            for (int p = 0; p < nb3fish; p++)
            {
                AllCells.Add(new Cell(CellType.Fish, 3));
            }

            #region Randomise List

            //Randomises the liste of fishes
            Random r = new Random();
            int randomIndex = 0;
            while (AllCells.Count > 0)
            {
                randomIndex = r.Next(0, AllCells.Count);
                AllCellsRandom.Add(AllCells[randomIndex]);
                AllCells.RemoveAt(randomIndex);
            }

            #endregion Randomise List
        }
    }
}