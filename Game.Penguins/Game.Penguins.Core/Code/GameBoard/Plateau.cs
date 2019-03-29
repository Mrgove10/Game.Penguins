using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Plateau : IBoard
    {
        private int TotalCells = 64;
        private int nb1fish = 10;
        private int nb2fish = 20;
        private int nb3fish = 34;
        public ICell[,] Board { get; }

        /// <summary>
        /// Board construtor, randomly generates a board
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Plateau(int sizeX, int sizeY)
        {
            Board = new ICell[sizeX, sizeY];

            if (TotalCells > 0)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        Board[i, j] = ChooseRandomCell();
                    }
                }

                TotalCells = TotalCells - 1;
            }
        }

        /// <summary>
        /// Generates a random cell type (water or whit 1 or 2 or 3 fish on it)
        /// </summary>
        /// <returns>Random Cell type</returns>
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