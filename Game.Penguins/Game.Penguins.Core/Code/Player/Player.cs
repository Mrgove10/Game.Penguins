using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Player
{
    public class Player : IPlayer
    {
        /// <summary>
        /// Constructor of teh player object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerType"></param>
        /// <param name="numberOfPenguins"></param>
        public Player(string name, PlayerType playerType, int numberOfPenguins)
        {
            Identifier = new Guid();
            Name = name;
            Color = getPlayerColor();
            Points = 0;
            PlayerType = playerType;
            
            if (numberOfPenguins < 0 || numberOfPenguins > 4)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Penguins = numberOfPenguins;
            }
        }

        private PlayerColor getPlayerColor()
        {
            List<PlayerColor> TakenColors = new List<PlayerColor>();
            Random rand = new Random();
            var randomNumber = rand.Next(0, 3);
            PlayerColor FinalColor;
            foreach (var color in TakenColors)
            {
                FinalColor = (PlayerColor) randomNumber;
                if (color != FinalColor)
                {
                    TakenColors.Add(FinalColor);
                    return FinalColor;
                }
                else
                {
                    randomNumber = rand.Next(0, 3);
                }
            }

            throw new Exception("wtf error");
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