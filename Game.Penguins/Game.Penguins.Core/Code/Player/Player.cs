﻿using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.Players;

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
        public Player(string name, PlayerType playerType, PlayerColor playerColor)
        {
            Identifier = new Guid();
            Name = name;
            Color = PlayerColor.Blue;
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
