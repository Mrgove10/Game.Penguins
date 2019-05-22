using Game.Penguins.Core;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System;

namespace Game.Penguins.AI.Code
{
    public class AiEasy : IAI
    {
        public int PlacementPenguinX { get; set; }
        public int PlacementPenguinY { get; set; }

        public IBoard plateau { get; }
        public IPenguin Penguin { get; }

        private readonly int[] _tabDirection = new int[6];

        public AiEasy(IBoard plateauParam, IPenguin penguinParam)
        {
            plateau = plateauParam;
            Penguin = penguinParam;
        }

        public void PlacementPenguin()
        {
            Random rndX = new Random();
            PlacementPenguinX = rndX.Next(7);

            Random rndY = new Random();
            PlacementPenguinY = rndY.Next(7);

            bool search = true;

            while (search)
            {
                if (plateau.Board[PlacementPenguinX, PlacementPenguinY].CellType == CellType.Fish && plateau.Board[PlacementPenguinX, PlacementPenguinY].FishCount == 1)
                {
                    //PlacePenguin[randomX, randomY];
                    search = false;
                }
                else
                {
                    rndX = new Random();
                    PlacementPenguinX = rndX.Next(7);

                    rndY = new Random();
                    PlacementPenguinY = rndY.Next(7);
                }
            }
        }

        public void DetectionCases(int posX, int posY)
        {
#if DEBUG
            Console.WriteLine("-- ON DETERMINE LES DEPLACEMENTS DISPONIBLES --");
#endif
            for (int direction = 0; direction <= 5; direction++)
            {
#if DEBUG
                Console.WriteLine(" - On test la direction : " + Enum.GetName(typeof(Direction), direction));
#endif
                ICell oui = plateau.Board[posX, posY];
                bool count = true;

                while (count)
                {
                    if (plateau.Board[posX, posY].CellType != CellType.Water || plateau.Board[posX, posY].CellType != CellType.FishWithPenguin)
                    {
                        _tabDirection[(int)Direction.Droite]++;
                    }
                    else
                    {
                        count = false;
                    }
                }
#if DEBUG
                Console.WriteLine(Enum.GetName(typeof(Direction), direction) + " : " + _tabDirection[(int)Direction.Droite]);
#endif
            }
#if DEBUG
            Console.WriteLine("-- DEPLACEMENTS DISPONIBLES TERMINES --");
#endif
        }
    }
}