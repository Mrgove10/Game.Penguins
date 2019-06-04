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
        private readonly List<Cell> _allCells = new List<Cell>();
        private readonly List<Cell> _allCellsRandom = new List<Cell>();
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
                    Board[i, j] = _allCellsRandom[n];
                    _allCellsRandom[n].XPos = i;
                    _allCellsRandom[n].YPos = j;
                    n++;
                }
            }
        }

        public Plateau(ICell[,] board)
        {
            Board = board;
        }

        /// <summary>
        /// Shuffles the list of fish to be random
        /// </summary>
        private void Shuffle()
        {
            for (int i = 0; i < nb1fish; i++)
            {
                _allCells.Add(new Cell(CellType.Fish, 1));
            }
            for (int o = 0; o < nb2fish; o++)
            {
                _allCells.Add(new Cell(CellType.Fish, 2));
            }
            for (int p = 0; p < nb3fish; p++)
            {
                _allCells.Add(new Cell(CellType.Fish, 3));
            }

            #region Randomise List

            //Randomizes the list of fishes
            Random r = new Random();
            while (_allCells.Count > 0)
            {
                var randomIndex = r.Next(0, _allCells.Count);
                _allCellsRandom.Add(_allCells[randomIndex]);
                _allCells.RemoveAt(randomIndex);
            }

            #endregion Randomise List
        }
    }
}