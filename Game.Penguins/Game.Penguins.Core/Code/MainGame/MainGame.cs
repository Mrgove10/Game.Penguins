using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.MainGame
{
    public class MainGame : IGame
    {
        MainGame()
        {
                
        }

        public IBoard Board { get; }
        public NextActionType NextAction { get; }
        public IPlayer CurrentPlayer { get; }
        public IList<IPlayer> Players { get; }
        public event EventHandler StateChanged;
        public void AddPlayer(string playerName, PlayerType playerType)
        {
            //ajouter les differents joueur
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            //Generation de la grille
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Places the penguins on the board whit an X and Y parametre
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePenguinManual(int x, int y)
        {

            throw new NotImplementedException();
        }

        public void PlacePenguin()
        {
            throw new NotImplementedException();
        }

        public void MoveManual(ICell origin, ICell destination)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
