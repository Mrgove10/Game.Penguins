using Game.Penguins.Core.Interfaces.Game.Players;
using System;

namespace Game.Penguins.Core.Code.Player
{
    public class Player : IPlayer
    {
        /// <summary>
        /// Constructor of teh player object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerType"></param>
        /// <param name="playerColor"></param>
        /// <param name="numberOfPenguins"></param>
        public Player(string name, PlayerType playerType, PlayerColor playerColor, int numberOfPenguins)
        {
            Identifier = new Guid();
            Name = name;
            Color = PlayerColor.Blue;
            Points = 0;

            if (numberOfPenguins < 0 || numberOfPenguins > 4)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Penguins = numberOfPenguins;
            }
        }

        public Guid Identifier { get; }
        public PlayerType PlayerType { get; }
        public PlayerColor Color { get; }
        public string Name { get; }
        public int Points { get; }
        public int Penguins { get; }

        public event EventHandler StateChanged;
    }
}