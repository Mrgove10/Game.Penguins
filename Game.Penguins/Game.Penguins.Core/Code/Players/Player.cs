using Game.Penguins.Core.Code.Penguins;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Players
{
    public class Player : IPlayer
    {
        public Guid Identifier { get; }
        public PlayerType PlayerType { get; }
        public PlayerColor Color { get; set; }
        public string Name { get; }

        private int _points;

        public int Points
        {
            get => _points;
            set
            {
                _points = value; //value = access the object created by set
                StateChanged?.Invoke(this, null);
            }
        }

        public int Penguins { get; set; }

        public List<Penguin> ListPenguins { get; set; }

        public event EventHandler StateChanged;

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
            Color = PlayerColor.Red;
            Points = 0;
            PlayerType = playerType;
            Penguins = 0;
            ListPenguins = new List<Penguin>();
        }
    }
}