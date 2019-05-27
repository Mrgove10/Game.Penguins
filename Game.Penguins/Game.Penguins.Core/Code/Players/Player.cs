﻿using Game.Penguins.Core.Interfaces.Game.GameBoard;
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

        private int points;
        public int Points {
            get => points;
            set
            {
                points = value; //value = access the object created by set
                StateChanged?.Invoke(this, null);
            }
        }
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
            Color = PlayerColor.Red;
            Points = 0;
            PlayerType = playerType;
            Penguins = 0;
            PlayerPenguinsList = new List<IPenguin>();
        }
    }
}