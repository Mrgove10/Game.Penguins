using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI.Code
{
    class AIEasy : IAI
    {
        public IBoard plateau { get; }

        int[] tabDirection = new int[6];

        public AIEasy(IBoard plateauParam)
        {
            plateau = plateauParam;
        }

        public void DetectionCases(int posX , int posY)
        {
#if DEBUG
            Console.WriteLine("-- ON DETERMINE LES DEPLACEMENTS DISPONIBLES --");
#endif
            for (int direction = 0; direction <= 5; direction++)
            {
#if DEBUG
                Console.WriteLine(" - On test la direction : " + Enum.GetName(typeof(Direction), direction));
#endif
                ICell oui = plateau.Board[posX,posY];
                bool count = true;

                while (count)
                {
                    if (plateau.Board[posX, posY].CellType != CellType.Water || plateau.Board[posX, posY].CellType != CellType.FishWithPenguin)
                    {
                        tabDirection[(int)Direction.Droite]++;
                    }
                    else
                    {
                        count = false;
                    }
                }
#if DEBUG
                Console.WriteLine(Enum.GetName(typeof(Direction), direction) + " : " + tabDirection[(int)Direction.Droite]);
#endif
            }
#if DEBUG
            Console.WriteLine("-- DEPLACEMENTS DISPONIBLES TERMINES --");
#endif
        }
    }
}
