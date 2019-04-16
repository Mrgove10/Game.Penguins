using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.Core.Code.GameBoard
{
    public class Cell : ICell
    {
        private CellType cellu;

        public CellType CellType
        {
            get => cellu;
            set
            {
                cellu = value; //value = access the object created by set
                StateChanged?.Invoke(this, null);
            }
        }

        public int FishCount { get; }

        private IPenguin pengu;

        public IPenguin CurrentPenguin
        {
            get => pengu;
            set
            {
                pengu = value; //value = access the object created by set
                StateChanged?.Invoke(this, null);
            }
        }

        public event EventHandler StateChanged;

        public int xPos;
        public int yPos;

        /// <summary>
        /// Cell Constructor for fish cells
        /// </summary>
        /// <param name="type"></param>
        /// <param name="numberOfFish"></param>
        public Cell(CellType type, int numberOfFish,int xPosParam,int yPosParam)
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
    }
}