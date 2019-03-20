using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;


namespace Game.Penguins.Core.Code.GameBoard
{
    class Plateau : IBoard
    {

        private ICell[,] m_board;

        // On implémente la propriété Board accessible en lecture.
        public ICell[,] Board
        {
            get { return m_board; }
        }

        public void CreationPlateau()
        {
            m_board = new ICell[8,8];


        }

    }
    
}
