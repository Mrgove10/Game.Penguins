using Common.Logging;
using Game.Penguins.AI.Code;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Code.Interfaces;
using Game.Penguins.Core.Code.Penguins;
using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Services
{
    public class MainGame : IGame
    {
        #region Declarations

        private readonly IAI _aiEasy;

        public IBoard Board { get; }
        public NextActionType NextAction { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        private IList<IPlayer> _playersPlayOrder;
        private int _currentPlayerNumber;
        private int _penguinsPerPlayer;

        private readonly ILog _log = LogManager.GetLogger<MainGame>(); //http://netcommon.sourceforge.net/docs/2.1.0/reference/html/ch01.html#logging-usage

        private readonly PointHelper _pointManager;
        private readonly IsolementVerificationHelper _isolationHelper;

        #endregion Declarations

        /// <summary>

        /// MainGame constructor
        /// </summary>
        public MainGame()
        {
            _log.Debug("Starting Game");
            /*8x8 Board , coordinates go from
            0,0 on the upper left to
            7,7 on the bottom right*/
            Board = new Plateau(8, 8);

            _pointManager = new PointHelper();
            _isolationHelper = new IsolementVerificationHelper(Board);

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
            _playersPlayOrder = GeneratePlayOrder(); //randomizes the play order
            UpdateNumberOfPenguins(Players.Count); // updated the number of penguins per player
            CalculateCurrentPlayerNumber();
            WhatIsNextTurn();
            CurrentPlayer = _playersPlayOrder[_currentPlayerNumber];
            _log.Debug("Current Number Of players : " + Players.Count);
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Runs the current turn number
        /// </summary>
        private void WhatIsNextTurn()
        {
            _log.Debug("===Turn ===");
            if (CurrentPlayer.Penguins < _penguinsPerPlayer) //this means we are in a placement turn
            {
                _log.Debug("Next turn is a Placement Turn");
                NextAction = NextActionType.PlacePenguin;
            }
            else
            {
                _log.Debug("Next turn is a Normal Turn");
                NextAction = NextActionType.MovePenguin;
            }
            _log.Debug("Current player to play : " + _currentPlayerNumber + " (" + CurrentPlayer.Name + ") " + CurrentPlayer.Color);
        }

        /// <summary>
        /// Calculates what player can now play
        /// </summary>
        private void CalculateCurrentPlayerNumber()
        {
            //calculates the current player
            if (_currentPlayerNumber < Players.Count - 1) //increments the current player
            {
                _currentPlayerNumber++;
            }
            else //If we arrive at the end of the players we start from the beginning
            {
                _currentPlayerNumber = 0;
            }
            CurrentPlayer = _playersPlayOrder[_currentPlayerNumber];
            _log.Debug("Current player is now " + _currentPlayerNumber + " (" + CurrentPlayer.Name + ")");
        }

        /// <summary>
        /// Randomizes the Player turns
        /// </summary>
        /// <returns></returns>
        private IList<IPlayer> GeneratePlayOrder()
        {
            List<IPlayer> copyStartList = new List<IPlayer>(Players); //local copy only for this function
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
                    _penguinsPerPlayer = 1;//4 penguins per player
                    break;

                case 3:
                    _penguinsPerPlayer = 3;//3 penguins per player
                    break;

                case 4:
                    _penguinsPerPlayer = 2;//2 penguins per player
                    break;
            }
        }

        /// <summary>
        /// Places the penguins on the board whit an X and Y parameter
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePenguinManual(int x, int y)
        {
            _log.Debug(CurrentPlayer.Name + " want's to place a penguin at x " + x + " y " + y);
            Cell currentCell = (Cell)Board.Board[x, y];
            if (currentCell.FishCount == 1 && currentCell.CellType != CellType.FishWithPenguin)
            {
                Player currentPlayer = (Player)CurrentPlayer;
                Penguin createdPenguin = new Penguin(currentPlayer, x, y);
                currentPlayer.ListPenguins.Add(createdPenguin);
                currentCell.CurrentPenguin = createdPenguin;
                currentCell.CellType = CellType.FishWithPenguin;
                currentPlayer.Penguins++;
                CalculateCurrentPlayerNumber();
                WhatIsNextTurn();
                StateChanged?.Invoke(this, null);
            }
            else
            {
                _log.Error("Cell has more then 1 penguin");
            }
        }

        /// <summary>
        /// Call the AI to place his penguin
        /// </summary>
        public void PlacePenguin()
        {
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                Coordinates pos = _aiEasy.PlacementPenguin();
                PlacePenguinManual(pos.X, pos.Y);
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                //Medium AI place function here
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
            _log.Debug("Player " + CurrentPlayer.Name + " wants to move from [" + ((Cell)origin).XPos + "|" + ((Cell)origin).YPos + "] to [" + ((Cell)destination).XPos + "|" + ((Cell)destination).YPos + "]");
            Cell originCell = (Cell)origin;
            Cell destinationCell = (Cell)destination;
            //todo: if teh cell is not water
            if (destinationCell.CellType == CellType.Fish)
            {
                if (originCell != destinationCell)
                {
                    if (CurrentPlayer == originCell.CurrentPenguin.Player)
                    {
                        _log.Debug("initial cell : " + originCell.XPos + ":" + originCell.YPos);
                        _log.Debug("Destination cell : " + destinationCell.XPos + ":" + destinationCell.YPos);
                        _pointManager.UpdatePlayerPoints(CurrentPlayer, originCell.FishCount);
                        destinationCell.CellType = CellType.FishWithPenguin;
                        destinationCell.CurrentPenguin = originCell.CurrentPenguin;
                        originCell.DeleteCell();
                        StateChanged?.Invoke(this, null);
                    }
                    else
                    {
                        _log.Debug("This is not the penguin of the player");
                    }
                }
                else
                {
                    _log.Debug("Origin cell can not be the same as the destination cell");
                }
            }
            else
            {
                _log.Debug("You can not move to that cell");
            }

            _isolationHelper.VerifyIsolate(destinationCell); //deletes the penguin and the cell
        }

        /// <summary>
        /// Execute a move for an AI
        /// </summary>
        public void Move()
        {
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                Player currentPlayer = (Player)CurrentPlayer;
                Penguin penguinToMove = currentPlayer.ListPenguins[new Random().Next(currentPlayer.ListPenguins.Count)];
                Coordinates posCell = _aiEasy.ChoseFinalDestinationCell(penguinToMove.XPos, penguinToMove.YPos);

                MoveManual(Board.Board[penguinToMove.XPos, penguinToMove.YPos], Board.Board[posCell.X, posCell.Y]);
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                //Medium AI move function here
            }
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                //Hard AI move function here
            }
        }

        public void VerifyEndGame()
        {
            int PlayerAlive = 0;

            //TODO si penguin == 0;
            foreach (IPlayer player in Players)
            {
                if (player.Penguins > 0)
                {
                    PlayerAlive += 1;
                }
            }

            if (PlayerAlive == 0)
            {
                //GAMEOVER
                NextAction = NextActionType.Nothing;
                _log.Debug(" -- FIN DU JEU -- ");
            }

            //Next actionType == nothing
        }
    }
}