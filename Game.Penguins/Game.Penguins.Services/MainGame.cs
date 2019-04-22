using Common.Logging;
using Game.Penguins.AI.Code;
using Game.Penguins.Core;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Penguins;
using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;
using Common.Logging.Configuration;

namespace Game.Penguins.Services
{
    public class MainGame : IGame
    {
        private IAI aiEasy;

        public IBoard Board { get; }
        public NextActionType NextAction { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        private IList<IPlayer> playersPlayOrder;
        private int currentPlayerNumber;
        private int turnNumber;
        private int penguinsPerPlayer;

        private readonly ILog Log = LogManager.GetLogger<MainGame>(); //http://netcommon.sourceforge.net/docs/2.1.0/reference/html/ch01.html#logging-usage
        
        /// <summary>
        /// MainGame constructor
        /// </summary>
        public MainGame()
        {
            Log.Debug("hello world");
            Log.Warn("caca");
            // log.Debug("Starting Game");
            /*8x8 Board , coordinates go from
            0,0 on the upper left to
            7,7 on the bottom right*/
            Board = new Plateau(8, 8);
            Players = new List<IPlayer>();
            CurrentPlayer = null;
#if DEBUG
            Console.WriteLine("Current Number Of players : " + Players.Count);
#endif
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
        /// <returns></returns>
        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            //initialise player whit 0 penguins ( will be updated later)
            //TODO : need to make the penguins good
            IPlayer tempPlayer = new Player(playerName, playerType);
            Players.Add(tempPlayer);//TODO :  unsure about this, verify
            return tempPlayer;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            playersPlayOrder = GeneratePlayOrder(); //randomizes the play order
            UpdateNumberOfPenguins(Players.Count); // updated the number of penguins per player
            CurrentPlayer = playersPlayOrder[currentPlayerNumber];
            StateChanged?.Invoke(this, null);
#if DEBUG
            //   debug();
#endif
        }

        /// <summary>
        /// Runs the turn of a player
        /// </summary>
        private void WhatIsNextTurn()
        {
            if (turnNumber < Players.Count) //this means we are in a placement turn
            {
                Console.WriteLine("Plac ement Turn");
                NextAction = NextActionType.PlacePenguin;//TODO : correct ?
            }
            else
            {
                Console.WriteLine("Normal Turn");
                NextAction = NextActionType.MovePenguin;
            }
#if DEBUG
            Console.WriteLine("Current player to play : " + currentPlayerNumber);
#endif
        }

        private void CalculateCurrentPlayerNumber()
        {
            //calculates the current player
            if (currentPlayerNumber < Players.Count - 1)
            {
                currentPlayerNumber++;
            }
            else
            {
                currentPlayerNumber = 0;
                turnNumber++;
            }
            CurrentPlayer = playersPlayOrder[currentPlayerNumber];
        }

        /// <summary>
        /// Randomizes the Player turns
        /// </summary>
        /// <returns></returns>
        private IList<IPlayer> GeneratePlayOrder()
        {
            IList<IPlayer> copyStartList = new List<IPlayer>(Players); //local copy only for this function
            List<IPlayer> randomList = new List<IPlayer>();
            Random r = new Random();
            while (copyStartList.Count > 0)
            {
                int randomIndex = r.Next(0, copyStartList.Count);
                randomList.Add(copyStartList[randomIndex]);
                copyStartList.RemoveAt(randomIndex);
            }

            int i = 0;
            foreach (var player in randomList) //Generates the color for each player
            {
                var play = (Player)player;
                play.Color = (PlayerColor)i;
                i++;
            }
            return randomList;
        }

        /// <summary>
        /// Updated the number of penguins per player
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void UpdateNumberOfPenguins(int numberOfPlayers)
        {
            switch (numberOfPlayers)
            {
                case 1:
                    throw new ArgumentOutOfRangeException();

                case 2:
                    penguinsPerPlayer = 4;
                    break;

                case 3:
                    penguinsPerPlayer = 3;
                    break;

                case 4:
                    penguinsPerPlayer = 2;
                    break;
            }
            foreach (var player1 in Players)
            {
                var player = (Player)player1;
                player.Penguins = penguinsPerPlayer;
                for (int i = 0; i < penguinsPerPlayer; i++)
                {
                    player.PlayerPenguinsList.Add(new Penguin(player));
                }
            }
        }

        /// <summary>
        /// Places the penguins on the board whit an X and Y parameter
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePenguinManual(int x, int y)
        {
            CalculateCurrentPlayerNumber();
            Console.WriteLine(CurrentPlayer.Name + " want's to place a penguin at x " + x + " y " + y);
            Cell currentCell = (Cell)Board.Board[x, y];
            if (currentCell.FishCount == 1)
            {
                if (currentCell.CurrentPenguin == null)
                {
                    currentCell.CurrentPenguin = new Penguin((Player)CurrentPlayer);
                    currentCell.CellType = CellType.FishWithPenguin;
                    StateChanged?.Invoke(this, null);
                }
            }
            Console.WriteLine("current cell type: " + currentCell.CellType + " " + currentCell.FishCount);
            WhatIsNextTurn();
        }

        /// <summary>
        /// Call the AI to place his penguin
        /// </summary>
        public void PlacePenguin()
        {
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                //Easy AI place function here

                aiEasy = new AIEasy(Board, null/*TODO : add arguments here*/);
                aiEasy.PlacementPenguin();

#if DEBUG
                Console.WriteLine("L'IA choisi sa position : [" + aiEasy.PlacementPenguinX + ", " + aiEasy.PlacementPenguinY + "]");
#endif
                Cell cellPenguin = (Cell)Board.Board[aiEasy.PlacementPenguinX, aiEasy.PlacementPenguinY];

                cellPenguin.CurrentPenguin = new Penguin((Player)CurrentPlayer);
                cellPenguin.CellType = CellType.FishWithPenguin;
                StateChanged?.Invoke(this, null);
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                //Meduim AI place function here
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                //Hard AI place function here
            }
        }

        /// <summary>
        /// Moves the penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public void MoveManual(ICell origin, ICell destination)
        {
            Cell originCell = (Cell)origin;
            Cell destinationCell = (Cell)origin;
            Console.WriteLine("initial cell :" + originCell.xPos + ":" + originCell.yPos);
            Console.WriteLine("Destination cell :" + destinationCell.xPos + ":" + destinationCell.yPos);
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Execute a move for an AI
        /// </summary>
        public void Move()
        {
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                //AIEasy.DetectionCases(posX, posY);
                //TODO TEST CETTE FONCTION AVEC POSX ET POSY
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                //Meduim AI move function here
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                //Hard AI move function here
            }
        }

        /// <summary>
        /// Print Debug info
        /// </summary>
        private void Debug()
        {
            Console.WriteLine("-----CELLS------");
            foreach (ICell cell in Board.Board)
            {
                Cell c = (Cell)cell;
                Console.WriteLine(c.xPos + ":" + c.yPos + " type : " + cell.CellType + " fishCount : " + cell.FishCount);
            }
            Console.WriteLine("Total cells on the board : " + Board.Board.Length);

            /*
            Console.WriteLine("-----PLAYER------");
            foreach (IPlayer player in Players)
            {
                Console.WriteLine(player.Identifier + " : " + player.Name + " is " + player.PlayerType + " has " + player.Penguins + " Penguins");
            }
            Console.WriteLine("-----PLAYER SHUFFLED------");
            foreach (IPlayer player in playersPlayOrder)
            {
                Console.WriteLine(player.Identifier + " : " + player.Name + " is " + player.PlayerType + " has " + player.Penguins + " Penguins");
            }
            Console.WriteLine("-----PLAYER START------");
            Console.WriteLine(CurrentPlayer.Identifier + " : " + CurrentPlayer.Name);
            */
        }
    }
}