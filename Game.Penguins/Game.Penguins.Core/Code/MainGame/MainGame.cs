using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.MainGame
{
    public class MainGame : IGame
    {
        public IBoard Board { get; }
        public NextActionType NextAction { get; }
        public IPlayer CurrentPlayer { get; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        public IList<IPlayer> PlayersPlayOrder;

        /// <summary>
        /// MainGame constructor
        /// </summary>
        public MainGame()
        {
            /*8x8 Board , cordenates go from
            0,0 on the upper left to
            7,7 on the bottom right*/
            Board = new Plateau(8, 8);
            Players = new List<IPlayer>();
            CurrentPlayer = null;
            Console.WriteLine("Current Number Of players : " + Players.Count);
        }

        /// <summary>
        /// Addes a player to the game
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
        /// <returns></returns>
        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            //initialises payer whit 0 penguins ( will be updated later)
            //TODO : need to make the penguins good
            IPlayer tempPlayer = new Player.Player(playerName, playerType, 4);
            Players.Add(tempPlayer);//TODO :  unsure about this, verrify
            return tempPlayer;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            PlayersPlayOrder = GeneratePlayOrder();

            //TODO: update penguis / player here

            //     this.CurrentPlayer = PlayersPlayOrder[0].Identifier; //takes the first randomly selected player

            #region DEBUG

            Console.WriteLine("-----CELLS------");
            foreach (ICell cell in Board.Board)
            {
                Console.WriteLine("type : " + cell.CellType + " fishCount : " + cell.FishCount);
            }
            Console.WriteLine("Total cells on the board : " + Board.Board.Length);

            Console.WriteLine("-----PLAYER------");
            foreach (IPlayer player in Players)
            {
                Console.WriteLine(player.Identifier + " : " + player.Name + " is " + player.PlayerType + " has " + player.Penguins + " Penguins");
            }
            Console.WriteLine("-----PLAYERSHUFFLED------");
            foreach (IPlayer player in PlayersPlayOrder)
            {
                Console.WriteLine(player.Identifier + " : " + player.Name + " is " + player.PlayerType + " has " + player.Penguins + " Penguins");
            }

            #endregion DEBUG
        }

        /// <summary>
        /// Randomises the Player turns
        /// </summary>
        /// <returns></returns>
        private IList<IPlayer> GeneratePlayOrder()
        {
            IList<IPlayer> CopyStartList = new List<IPlayer>(Players); //local copy only for thsi function
            List<IPlayer> RandomList = new List<IPlayer>();
            Random r = new Random();
            int randomIndex = 0;
            while (CopyStartList.Count > 0)
            {
                randomIndex = r.Next(0, CopyStartList.Count);
                RandomList.Add(CopyStartList[randomIndex]);
                CopyStartList.RemoveAt(randomIndex);
            }

            return RandomList;
        }

        /// <summary>
        /// Places the penguins on the board whit an X and Y parametre
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePenguinManual(int x, int y)
        {
            Console.WriteLine("Player want's to place a penguin at x " + x + " y " + y);
            ICell currentcell = Board.Board[x, y];
            Console.WriteLine("curretn cellel type: " + currentcell.CellType + " " + currentcell.FishCount);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Places a penguin on the cell
        /// </summary>
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