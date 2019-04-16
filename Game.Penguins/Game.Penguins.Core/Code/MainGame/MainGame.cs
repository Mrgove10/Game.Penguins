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

        private IList<IPlayer> PlayersPlayOrder;
        private int CurrentPlayerNumber = 0;
        private int turnNumber = 0;
        private int penguinsPerPlayer = 0;

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
#if DEBUG
            Console.WriteLine("Current Number Of players : " + Players.Count);
#endif
            if (StateChanged != null)
            {
                StateChanged.Invoke(this, null);
            }
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
            IPlayer tempPlayer = new Player.Player(playerName, playerType);
            Players.Add(tempPlayer);//TODO :  unsure about this, verrify
            return tempPlayer;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            PlayersPlayOrder = GeneratePlayOrder(); //randomises the play order
            UpdateNumberOfPlayers(Players.Count); // updated the number of penguins per player
            CurrentPlayer = PlayersPlayOrder[CurrentPlayerNumber];
            StateChanged.Invoke(this, null);
            //  getPlayerColor();
#if DEBUG

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
            Console.WriteLine("-----PLAYERSHUFFLED------");
            foreach (IPlayer player in PlayersPlayOrder)
            {
                Console.WriteLine(player.Identifier + " : " + player.Name + " is " + player.PlayerType + " has " + player.Penguins + " Penguins");
            }
            Console.WriteLine("-----PLAYERSTARTS------");
            Console.WriteLine(CurrentPlayer.Identifier + " : " + CurrentPlayer.Name);
#endif
            //  PlacePenguinManual(1, 1);

            //    run();
        }

        private void run()
        {
        }

        /// <summary>
        /// Runs the turn of a player
        /// </summary>
        /// <param name="Player"></param>
        private void turn(int turnNumber, IPlayer currentPlayer)
        {
            if (turnNumber < Players.Count) //this means we are in a placement turn
            {
                if (currentPlayer.PlayerType == PlayerType.Human)
                {
                    StateChanged.Invoke(this, null);
                }
                else
                {
                    PlacePenguin(); //AI
                }
            }
            else
            {
                if (currentPlayer.PlayerType == PlayerType.Human)
                {
                    StateChanged.Invoke(this, null);
                }
                else
                {
                    Move(); //AI
                }
            }
#if DEBUG
            Console.WriteLine("Current player to play : " + CurrentPlayerNumber);
#endif

            if (CurrentPlayerNumber < Players.Count - 1)
            {
                CurrentPlayerNumber++;
            }
            else
            {
                CurrentPlayerNumber = 0;
                turnNumber++;
            }
            CurrentPlayer = PlayersPlayOrder[CurrentPlayerNumber];
            //to stuff for each player turn
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
        /// Updated the number of penguins per player
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void UpdateNumberOfPlayers(int numberOfPlayers)
        {
            switch (numberOfPlayers)
            {
                case 1:
                    throw new ArgumentOutOfRangeException();
                    break;

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
        /// Places the penguins on the board whit an X and Y parametre
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePenguinManual(int x, int y)
        {
            Console.WriteLine("Player want's to place a penguin at x " + x + " y " + y);
            Cell currentcell = (Cell)Board.Board[x, y];
            if (currentcell.CurrentPenguin == null)
            {
                currentcell.CurrentPenguin = new Penguin.Penguin(CurrentPlayer);
                currentcell.CellType = CellType.FishWithPenguin;
            }
            currentcell.CurrentPenguin = new Penguin.Penguin(CurrentPlayer);
            Console.WriteLine("current cell type: " + currentcell.CellType + " " + currentcell.FishCount);
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
                AIEasy.DetectionCases(int posX, int posY)
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
    }
}