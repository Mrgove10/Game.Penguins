using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Cell : ICell
    {
        private CellType cellu;
        public CellType CellType {
            get
            {
                return cellu;
            }
            set
            {
                cellu = value; //value acces the object created by set
                StateChanged.Invoke(this, null);
            }
        }
        public int FishCount { get; }

        private IPenguin pengu;
        public IPenguin CurrentPenguin
        {
            get
            {
                return pengu;
            }
            set
            {
                pengu = value; //value acces the object created by set
                StateChanged.Invoke(this,null);
            }
        }

        public event EventHandler StateChanged;

        /// <summary>
        /// Cell Consructor for non fish cells
        /// </summary>
        /// <param name="type"></param>
        public Cell(CellType type)
        {
            if (type != CellType.Fish)
            {
                CellType = type;
            }
        }

        /// <summary>
        /// Cell Constructor for fish cells
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
        
    }
}