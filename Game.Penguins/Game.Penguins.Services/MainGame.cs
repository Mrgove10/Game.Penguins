using Common.Logging;
using Game.Penguins.Core;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Code.Penguins;
using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Penguins.AI.Code;

//using Game.Penguins.Core.Code.Helper.Points;

namespace Game.Penguins.Services
{
    public class MainGame : IGame
    {
        #region Declarations
        private readonly IAI _aiEasy;
        private IAI _aiMedium;
        private IAI _aiHard;
        
        public IBoard Board { get; }
        public NextActionType NextAction { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        private IList<IPlayer> playersPlayOrder;
        private int currentPlayerNumber = 0;
        private int turnNumber;
        private int penguinsPerPlayer;

        private readonly ILog Log = LogManager.GetLogger<MainGame>(); //http://netcommon.sourceforge.net/docs/2.1.0/reference/html/ch01.html#logging-usage

        private readonly PointHelper _pointManager = new PointHelper();

        #endregion Declarations

        /// <summary>
        /// MainGame constructor
        /// </summary>
        public MainGame()
        {
            Log.Debug("Starting Game");
            /*8x8 Board , coordinates go from
            0,0 on the upper left to
            7,7 on the bottom right*/
            Board = new Plateau(8, 8);
            _aiEasy = new AiEasy(Board);
            // AiMedium = new AiMedium(Board);
            // AiHard = new AiHard(Board);
            Players = new List<IPlayer>();
            CurrentPlayer = null;

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
            //initialise player whit 0 penguins & a default color( will be updated later) 
            IPlayer tempPlayer = new Player(playerName, playerType);
            Players.Add(tempPlayer);
            return tempPlayer;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            playersPlayOrder = GeneratePlayOrder(); //randomizes the play order
            UpdateNumberOfPenguins(Players.Count); // updated the number of penguins per player
            CalculateCurrentPlayerNumber();
            CurrentPlayer = playersPlayOrder[currentPlayerNumber];
            Log.Debug("Current Number Of players : " + Players.Count);
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Runs the turn of a player
        /// </summary>
        private void WhatIsNextTurn()
        {
            if (turnNumber < penguinsPerPlayer) //this means we are in a placement turn
            {
                Log.Debug("Next turn is a Placement Turn");
                NextAction = NextActionType.PlacePenguin;
            }
            else
            {
                Log.Debug("Next turn is a Normal Turn");
                NextAction = NextActionType.MovePenguin;
            }
            Log.Debug("Current player to play : " + currentPlayerNumber);
        }

        /// <summary>
        /// Calculates what player can now play
        /// </summary>
        private void CalculateCurrentPlayerNumber()
        {
            //calculates the current player
            if (currentPlayerNumber < Players.Count - 1) //increments the current player
            {
                currentPlayerNumber++;
            }
            else //If we arrive at the end of the players we start from the beginning
            {
                currentPlayerNumber = 0;
                turnNumber++;
            }
            CurrentPlayer = playersPlayOrder[currentPlayerNumber];
            Log.Debug("Current player is now " + currentPlayerNumber);
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
                    penguinsPerPlayer = 4;//4
                    break;

                case 3:
                    penguinsPerPlayer = 3;//3
                    break;

                case 4:
                    penguinsPerPlayer = 2;//2
                    break;
            }

            foreach (var p in Players)
            {
                var player = (Player)p;
                player.Penguins = penguinsPerPlayer;
                for (int i = 0; i < penguinsPerPlayer; i++)//Adds the number fo penguins to the list of the player
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
            Log.Debug(CurrentPlayer.Name + " want's to place a penguin at x " + x + " y " + y);
            Cell currentCell = (Cell)Board.Board[x, y];
            if (currentCell.FishCount == 1 && currentCell.CellType != CellType.FishWithPenguin)
            {
                currentCell.CurrentPenguin = new Penguin((Player)CurrentPlayer);
                currentCell.CellType = CellType.FishWithPenguin;
                Log.Debug("current cell type: " + currentCell.CellType + " " + currentCell.FishCount);
                CalculateCurrentPlayerNumber();
                WhatIsNextTurn();
                StateChanged?.Invoke(this, null);
            }
            else
            {
                Log.Error("Cell has more then 1 penguin");
            }
        }

        /// <summary>
        /// Call the AI to place his penguin
        /// </summary>
        public void PlacePenguin()
        {
            Log.Debug("AI Placement");
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                Log.Debug("Easy AI");
                int[] p = _aiEasy.PlacementPenguin();
                PlacePenguinManual(p[0], p[1]);
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                //Meduim AI place function here
                StateChanged?.Invoke(this, null);
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                //Hard AI place function here
                StateChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Moves the penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public void MoveManual(ICell origin, ICell destination)
        {
            //TODO: you can mouve any player
            Cell originCell = (Cell)origin;
            Cell destinationCell = (Cell)destination;
            if (destinationCell.CellType == CellType.Fish)
            {
                if (originCell != destinationCell)
                {
                    if (CurrentPlayer == originCell.CurrentPenguin.Player)
                    {
                        Log.Debug("initial cell : " + originCell.XPos + ":" + originCell.YPos);
                        Log.Debug("Destination cell : " + destinationCell.XPos + ":" + destinationCell.YPos);
                        _pointManager.UpdatePlayerPoints(CurrentPlayer, originCell.FishCount);
                        destinationCell.CellType = CellType.FishWithPenguin;
                        destinationCell.CurrentPenguin = originCell.CurrentPenguin;
                        originCell.deleteCell();
                        StateChanged?.Invoke(this, null);
                    }
                    else
                    {
                        Log.Debug("This is not the penguin of the player");
                    }
                }
                else
                {
                    Log.Debug("Origin cell can not be the same as the destination cell");
                }
            }
            else
            {
                Log.Debug("You can not move to that cell");
            }
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

        public void EndGame()
        {
            
        }
    }
}