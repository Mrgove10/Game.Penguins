using Common.Logging;
using Game.Penguins.Core;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.AI.Code
{
    public class AiEasy : IAI
    {
        private readonly ILog Log = LogManager.GetLogger<AiEasy>();
        public int PlacementPenguinX { get; set; }
        public int PlacementPenguinY { get; set; }

        public IBoard MainBoard { get; }
        public IPenguin Penguin { get; }

        private readonly int[] _tabDirection = new int[6];

        public AiEasy(IBoard plateauParam)
        {
            MainBoard = plateauParam;
        }

        /// <summary>
        /// Places a penguin
        /// </summary>
        public int[] PlacementPenguin()
        {
            Random rnd = new Random();
            bool search = true;

            while (search) //while it is in a searching state
            {
                PlacementPenguinX = rnd.Next(7);
                PlacementPenguinY = rnd.Next(7);
                ICell c = MainBoard.Board[PlacementPenguinX, PlacementPenguinY];

                if (c.CellType == CellType.Fish && c.FishCount == 1)
                {
                    int[] tab = new int[2];
                    tab[0] = PlacementPenguinX;
                    tab[1] = PlacementPenguinY;
                    search = false;
                    Log.Debug("AI will place itself at x: " + PlacementPenguinX + " , y: " + PlacementPenguinY);
                    return tab;
                }
            }
            Log.Error("no cell found");
            return null; //TODO: change this
        }

        /// <summary>
        /// Determines wheere a penguin can move
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public void DetectionCases(int posX, int posY)
        {
            Log.Debug("-- ON DETERMINE LES DEPLACEMENTS DISPONIBLES --");

            for (int direction = 0; direction <= 5; direction++)
            {
                Log.Debug(" - On test la direction : " + Enum.GetName(typeof(Direction), direction));
                ICell oui = MainBoard.Board[posX, posY];
                bool count = true;

                while (count)
                {
                    if (MainBoard.Board[posX, posY].CellType != CellType.Water || MainBoard.Board[posX, posY].CellType != CellType.FishWithPenguin)
                    {
                        _tabDirection[(int)Direction.Droite]++;
                    }
                    else
                    {
                        count = false;
                    }
                }
                Log.Debug(Enum.GetName(typeof(Direction), direction) + " : " + _tabDirection[(int)Direction.Droite]);
            }
            Log.Debug("-- DEPLACEMENTS DISPONIBLES TERMINES --");
        }
    }
}