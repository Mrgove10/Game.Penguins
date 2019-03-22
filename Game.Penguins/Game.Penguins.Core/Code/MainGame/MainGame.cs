using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.MainGame
{
    public class MainGame : IGame
    {
        private MainGame()
        {
            Board = new Plateau(8, 8);
            NextAction = null;
            CurrentPlayer = null;
            Players = new List<IPlayer>();
            AddPlayer();
        }

        public IBoard Board { get; }
        public NextActionType NextAction { get; }
        public IPlayer CurrentPlayer { get; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        public void AddPlayer(string playerName, PlayerType playerType)
        {
            Players.Add(new Player(playerName, playerType));
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

        //
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