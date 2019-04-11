using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI.Code
{
    class AIEasy
    {
        IBoard plateau;
        enum Direction { Droite, Gauche, HautGauche, HautDroite, BasGauche, BasDroite };

        int[] tabDirection = new int[6];
        public AIEasy(IBoard plateauParam)
        {
            plateau = plateauParam;
        }
        public void DetectionCases(int posX , int posY)
        {
            for (int direction = 0; direction <= 5; direction++)
            {
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
            }
            //TODO: Debug
        }
    }
}
