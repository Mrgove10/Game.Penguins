using Common.Logging;
using Game.Penguins.AI.Easy.Code;
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

        private readonly IAi _aiEasy;

        public IBoard Board { get; }
        public NextActionType NextAction { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        private IList<IPlayer> _playersPlayOrder; //list of the player by they play order
        private int _currentPlayerNumber; //the number of the current player
        private int _penguinsPerPlayer; //number of penguins per player

        private readonly ILog _log = LogManager.GetLogger<MainGame>(); //http://netcommon.sourceforge.net/docs/2.1.0/reference/html/ch01.html#logging-usage

        private readonly PointHelper _pointManager;
        private readonly IsolationVerificationHelper _isolationHelper;

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
            _isolationHelper = new IsolationVerificationHelper(Board);

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
            //initialise player with 0 penguins & a default color( will be updated later)
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
            CalculateCurrentPlayerNumber(); //calculates which player will now play
            WhatIsNextTurn(); //attributes a number to the current turn
            CurrentPlayer = _playersPlayOrder[_currentPlayerNumber]; //defines the current player
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
            else //in a normal turn :
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
            while (copyStartList.Count > 0) //randomizes the player who starts the game 
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

        #region Placement

        /// <summary>
        /// Places the penguins on the board whit an X and Y parameter
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePenguinManual(int x, int y)
        {
            _log.Debug(CurrentPlayer.Name + " want's to place a penguin at x " + x + " y " + y);
            Cell currentCell = (Cell)Board.Board[x, y];

            if (currentCell.FishCount == 1 && currentCell.CellType != CellType.FishWithPenguin) // is empty and has only one penguin
            {
                Player currentPlayer = (Player)CurrentPlayer; //the current player...
                Penguin createdPenguin = new Penguin(currentPlayer, x, y);//...gets a new penguin to place..
                currentPlayer.ListPenguins.Add(createdPenguin); //... which is added to his stack of penguins...
                currentCell.CurrentPenguin = createdPenguin; //...it becomes the resident penguin of the cell...
                currentCell.CellType = CellType.FishWithPenguin;//... which types becomes fish + penguin
                currentPlayer.Penguins++; // the current player's penguins increases

                //and we switch to the next player's turn
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
            switch (CurrentPlayer.PlayerType)
            {
                case PlayerType.AIEasy:
                    {
                        Coordinates pos = _aiEasy.PlacementPenguin();
                        PlacePenguinManual(pos.X, pos.Y);
                        break;
                    }

                case PlayerType.AIMedium:
                    //Hard AI place function here
                    break;

                case PlayerType.AIHard:
                    //Medium AI place function here
                    break;
            }
            StateChanged?.Invoke(this, null);
        }

        #endregion Placement

        #region Mouvement

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
            if (destinationCell.CellType == CellType.Fish) //the destination must have at least one fish on it
            {
                if (originCell != destinationCell) //the destination cell should not be the origin cell
                {
                    if (CurrentPlayer == originCell.CurrentPenguin.Player) //the current player must be the one on the origin cell
                    {
                        _log.Debug("initial cell : " + originCell.XPos + ":" + originCell.YPos);
                        _log.Debug("Destination cell : " + destinationCell.XPos + ":" + destinationCell.YPos);
                        _pointManager.UpdatePlayerPoints(CurrentPlayer, originCell.FishCount); // the number of fish on the origin cell is added to the current player's score as he moves
                        destinationCell.CellType = CellType.FishWithPenguin; // the destination cell becomes a "Fish + Penguin" type cell
                        destinationCell.CurrentPenguin = originCell.CurrentPenguin; //the penguin moves
                        originCell.DeleteCell(); //the origin cell is removed
                        StateChanged?.Invoke(this, null); //board update
                    }
                    else
                    {
                        _log.Debug("This is not the penguin of the player"); //if the current player tries to move from the wrong cell
                    }
                }
                else
                {
                    _log.Debug("Origin cell can not be the same as the destination cell"); //if the current player selects his origin cell to move to
                }
            }
            else
            {
                _log.Debug("You can not move to that cell"); //if the destination cell is not eligible
            }

            //preparing for next player's turn
            CalculateCurrentPlayerNumber();
            WhatIsNextTurn();

            //verifying if the game is over yet
            VerifyEndGame();

            _isolationHelper.VerifyIsolation(destinationCell); //deletes the penguin and the cell
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Execute a move for an AI
        /// </summary>
        public void Move()
        {
            switch (CurrentPlayer.PlayerType)
            {
                case PlayerType.AIEasy:
                    //Easy AI movement
                    Player currentPlayer = (Player)CurrentPlayer;
                    Penguin penguinToMove = currentPlayer.ListPenguins[new Random().Next(currentPlayer.ListPenguins.Count)];
                    Coordinates posCell = _aiEasy.ChoseFinalDestinationCell(penguinToMove.XPos, penguinToMove.YPos);
                    Cell originCell = (Cell)Board.Board[penguinToMove.XPos, penguinToMove.YPos];

                    if (posCell == null)//a  player can not move anymore, end of game for him
                    {
                        if (_isolationHelper.VerifyIsolation(originCell))
                        {
                            //in this case then penguin is isolated
                            currentPlayer.Penguins--; //decreases the number of penguins for this player
                            originCell.CurrentPenguin = null;
                            originCell.CellType = CellType.Water;
                            _log.Warn("penguin at " + originCell.XPos + " - " + originCell.YPos + "is isolated");
                        }
                    }
                    else
                    {
                        Cell destinationCell = (Cell)Board.Board[posCell.X, posCell.Y];
                        MoveManual(originCell, destinationCell);

                        penguinToMove.XPos = posCell.X;
                        penguinToMove.YPos = posCell.Y;
                    }

                    break;

                case PlayerType.AIMedium:
                    //Medium AI move function here
                    break;

                case PlayerType.AIHard:
                    //Hard AI move function here
                    break;
            }
        }

        #endregion Mouvement

        /// <summary>
        /// Verifies end game
        /// </summary>
        public void VerifyEndGame()
        {
            int playerAlive = 0;

            //TODO si penguin == 0;
            foreach (IPlayer player in Players)
            {
                if (player.Penguins > 0) 
                {
                    playerAlive += 1;
                }
            }

            if (playerAlive == 0)
            {
                //GAME-OVER
                NextAction = NextActionType.Nothing;
                _log.Debug(" -- FIN DU JEU -- ");
            }
        }
    }
}