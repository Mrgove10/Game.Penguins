using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Cell : ICell
    {
        private CellType _cell;

        public CellType CellType
        {
            get => _cell;
            set
            {
                _cell = value; //value = access the object created by set
                StateChanged?.Invoke(this, null);
            }
        }

        public int FishCount { get; set; }

        private IPenguin _penguin;

        public IPenguin CurrentPenguin
        {
            get => _penguin;
            set
            {
                _penguin = value; //value = access the object created by set
                StateChanged?.Invoke(this, null);
            }
        }

        public event EventHandler StateChanged;

        public int XPos; //X position of the cell
        public int YPos; //Y position of the cell

        /// <summary>
        /// Cell Constructor for fish cells
        /// </summary>
        /// <param name="type"></param>
        /// <param name="numberOfFish"></param>
        public Cell(CellType type, int numberOfFish)
        {
            CellType = type;
            if (type != CellType.Fish)
            {
                throw new Exception();
            }
            else
            {
                if (numberOfFish < 0 || numberOfFish > 3)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    FishCount = numberOfFish;
                }
            }
        }

        /// <summary>
        /// Cell constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="numberOfFish"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Cell(CellType type, int numberOfFish, int posX, int posY)
        {
            CellType = type;
            XPos = posX;
            YPos = posY;
        }

        /// <summary>
        /// Empty cell contructor
        /// </summary>
        public Cell() { }

        /// <summary>
        /// Deletes the cell by setting all its values to zero
        /// </summary>
        public void DeleteCell()
        {
            CurrentPenguin = null;
            FishCount = 0;
            CellType = CellType.Water;
        }
    }
}