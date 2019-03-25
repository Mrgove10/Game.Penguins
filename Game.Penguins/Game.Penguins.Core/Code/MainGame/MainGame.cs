using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Player;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.ViewModels;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.MainGame
{
    public class MainGame : IGame
    {
        public MainGame()
        {
            /*8x8 Board , coordenates go from
            0,0 on the upper left to
            7,7 on the bottom right*/
            Board = new Plateau(8, 8);
            CurrentPlayer = null;
            Players = new List<IPlayer>();
            AddPlayer();
        }

        public IBoard Board { get; }
        public NextActionType NextAction { get; }
        public IPlayer CurrentPlayer { get; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        /// <summary>
        /// A des a player
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddPlayer(string playerName, PlayerType playerType)
        {
             if (Players.Count == 2)
            {
                Players.Add(new Player(Player1Name.get()), playerType, 4);
                Players.Add(new Player(playerName), playerType, 4);
            }
            else if (Players.Count == 3)
            {
                Players.Add(new Player(playerName), playerType, 3);
                Players.Add(new Player(playerName), playerType, 3);
                Players.Add(new Player(playerName), playerType, 3);
            }
            else if (Players.Count == 4)
            {
                Players.Add(new Player(playerName), playerType, 2);
                Players.Add(new Player(playerName), playerType, 2);
                Players.Add(new Player(playerName), playerType, 2);
                Players.Add(new Player(playerName), playerType, 2);
            }
            
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