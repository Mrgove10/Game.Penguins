using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

using System;
using System.Linq;
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
            Board = new Plateau(8,8);
            CurrentPlayer = null;
            Players = new List<IPlayer>();
            Console.WriteLine("Current Number Of players : " + Players.Count);
        }

        public IBoard Board { get; }
        public NextActionType NextAction { get; }
        public IPlayer CurrentPlayer { get; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            //initialises payer whit 0 penguis ( will be updated later)
            //TODO : need to make the penguis good
            return new Player.Player(playerName, playerType, 4);
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            //debug
            foreach (Cell cell in Board.Board)
            {
                Console.WriteLine("type : " + cell.CellType + " fishCount : " + cell.FishCount);
            }
            
            Console.WriteLine("Total cells  : " + Board.Board.Length);
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