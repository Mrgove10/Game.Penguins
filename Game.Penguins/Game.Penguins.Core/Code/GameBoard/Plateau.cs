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
            if (TotalCells > 0)
            {
                var n = 0;
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        Board[i, j] = AllCellsRandom[n];
                        n++;
                    }
                }

                TotalCells = TotalCells - 1;
            }
        }


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

            Random r = new Random();
            int randomIndex = 0;
            while (AllCells.Count > 0)
            {
                randomIndex = r.Next(0, AllCells.Count); //Choose a random object in the list
                AllCellsRandom.Add(AllCells[randomIndex]); //add it to the new, random list
                AllCells.RemoveAt(randomIndex); //remove to avoid duplicates
            }


        }

        /// <summary>
        /// Generates a random cell type (water or whit 1 or 2 or 3 fish on it)
        /// </summary>
        /// <returns>Random Cell type</returns>
        [Obsolete]
        private Cell ChooseRandomCell()
        {
            Random rand = new Random();
            var value = rand.Next(0, 3);

            switch (value)
            {
                case 0:
                    if (nb1fish > 0)
                    {
                        nb1fish = nb1fish - 1;
                        return new Cell(CellType.Fish, 1);
                    }
                    break;

                case 1:
                    if (nb2fish > 0)
                    {
                        nb2fish = nb2fish - 1;
                        return new Cell(CellType.Fish, 2);
                    }
                    break;

                case 2:
                    if (nb3fish > 0)
                    {
                        nb3fish = nb3fish - 1;
                        return new Cell(CellType.Fish, 3);
                    }
                    break;
            }
            return new Cell(CellType.Empty);
        }
    }
}