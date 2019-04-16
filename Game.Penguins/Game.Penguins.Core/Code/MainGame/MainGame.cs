using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.MainGame
{
    public class MainGame : IGame
    {
        public IAI AIEasy;

        public IBoard Board { get; }
        public NextActionType NextAction { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public IList<IPlayer> Players { get; }

        public event EventHandler StateChanged;

        private IList<IPlayer> playersPlayOrder;
        private int currentPlayerNumber = 0;
        private int turnNumber = 0;
        private int penguinsPerPlayer = 0;
        private bool placementDone = false;

        /// <summary>
        /// MainGame constructor
        /// </summary>
        public MainGame()
        {
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
        /// Addes a player to the game
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
        /// <returns></returns>
        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            //initialise player whit 0 penguins ( will be updated later)
            //TODO : need to make the penguins good
            IPlayer tempPlayer = new Player.Player(playerName, playerType);
            Players.Add(tempPlayer);//TODO :  unsure about this, verify
            return tempPlayer;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            playersPlayOrder = GeneratePlayOrder(); //randomizes the play order
            UpdateNumberOfPlayers(Players.Count); // updated the number of penguins per player
            CurrentPlayer = playersPlayOrder[currentPlayerNumber];
            StateChanged?.Invoke(this, null);
#if DEBUG
            //   debug();
#endif
        }

        /// <summary>
        /// Runs the turn of a player
        /// </summary>
        private void Turn()
        {
            if (turnNumber < Players.Count) //this means we are in a placement turn
            {
                Console.WriteLine("Placement Turn");
                NextAction = NextActionType.PlacePenguin;
            }
            else
            {
                Console.WriteLine("Normal Turn");
                NextAction = NextActionType.MovePenguin;
            }
#if DEBUG
            Console.WriteLine("Current player to play : " + currentPlayerNumber);
#endif
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
            foreach (var player in randomList) //Generated the colo for each player
            {
                var play = (Player.Player)player;
                play.Color = (PlayerColor)i;
                Console.WriteLine("-----" + i);
                i++;
            }
            return randomList;
        }

        /// <summary>
        /// Updated the number of penguins per player
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void UpdateNumberOfPlayers(int numberOfPlayers)
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

            foreach (Player.Player player in Players)
            {
                player.Penguins = penguinsPerPlayer;
                for (int i = 0; i < penguinsPerPlayer; i++)
                {
                    player.PlayerPenguinsList.Add(new Penguin.Penguin(player));
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
            Turn();
            Console.WriteLine(CurrentPlayer.Name + " want's to place a penguin at x " + x + " y " + y);
            Cell currentCell = (Cell)Board.Board[x, y];
            if (currentCell.FishCount == 1)
            {
                if (currentCell.CurrentPenguin == null)
                {
                    currentCell.CurrentPenguin = new Penguin.Penguin(CurrentPlayer);
                    currentCell.CellType = CellType.FishWithPenguin;
                }
            }
            Console.WriteLine("current cell type: " + currentCell.CellType + " " + currentCell.FishCount);
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Call the AI to place his penguin
        /// </summary>
        public void PlacePenguin()
        {
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                //Easy AI place function here
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

            throw new NotImplementedException();
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
            /*
            Console.WriteLine("-----CELLS------");
            foreach (ICell cell in Board.Board)
            {
                Console.WriteLine("type : " + cell.CellType + " fishCount : " + cell.FishCount);
            }
            Console.WriteLine("Total cells on the board : " + Board.Board.Length);
            */
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
        }
    }
}