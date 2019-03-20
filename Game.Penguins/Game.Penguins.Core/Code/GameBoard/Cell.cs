using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Cell : ICell
    {
        /// <summary>
        /// CellConsructor for non fish
        /// </summary>
        /// <param name="type"></param>
        public Cell(CellType type)
        {
            if (type == CellType.Fish)
            {
                throw new Exception();
            }
            else
            {
                CellType = type;
            }
        }

        /// <summary>
        /// Cell Constructor for fish
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nuberOfFish"></param>
        public Cell(CellType type, int nuberOfFish)
        {
            CellType = type;
            if (type != CellType.Fish)
            {
                throw new Exception();
            }
            else
            {
                if (nuberOfFish < 0 || nuberOfFish > 3)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    FishCount = nuberOfFish;
                }
            }
        }

        public CellType CellType { get; }
        public int FishCount { get; }
        public IPenguin CurrentPenguin { get; }

        public event EventHandler StateChanged;
    }
}