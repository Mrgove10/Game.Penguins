using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Player
{
    public class Player : IPlayer
    {
        public Guid Identifier { get; }
        public PlayerType PlayerType { get; }
        public PlayerColor Color { get; set; }
        public string Name { get; }
        public int Points { get; set; }
        public int Penguins { get; set; }

        public event EventHandler StateChanged;

        public List<IPenguin> PlayerPenguinsList { get; set; }

        /// <summary>
        /// Constructor of the player object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerType"></param>
        public Player(string name, PlayerType playerType)
        {
            Guid guid = Guid.NewGuid();
            Identifier = guid;
            Name = name;
            Color = getPlayerColor();
            Points = 0;
            PlayerType = playerType;
            Penguins = 0;
            PlayerPenguinsList = new List<IPenguin>();
        }

        /// <summary>
        /// Generates the player color
        /// </summary>
        /// <returns></returns>
        private PlayerColor getPlayerColor()
        {
            Random rand = new Random();
            var randomNumber = rand.Next(0, 3);

            return (PlayerColor)randomNumber;

            //TODO need to change this !!!!!
        }
    }
}