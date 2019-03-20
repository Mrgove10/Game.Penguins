using System;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Plateau : IBoard
    {
        public ICell[,] Board { get; }

        /// <summary>
        /// Board construtor
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Plateau(int sizeX, int sizeY)
        {
            Board = new ICell[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    Board[i,j] = new Cell(CellType.Fish, 3);
                }
            }
        }
    }
}