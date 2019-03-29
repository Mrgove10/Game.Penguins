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
            Random rand = new Random();
            var randomNumber = rand.Next(0, 3);
            return (PlayerColor) randomNumber;

            //TODO need to change this !!!!!
            /*
            List<PlayerColor> TakenColors = new List<PlayerColor>();
            Random rand = new Random();
            var randomNumber = rand.Next(0, 3);
            PlayerColor FinalColor;
            for (int i = 0; i < 3; i++)
            {
                FinalColor = (PlayerColor) randomNumber;
                if ((PlayerColor)i == FinalColor)
                {
                    TakenColors.Add(FinalColor);
                    return FinalColor;
                }
                else
                {
                    randomNumber = rand.Next(0, 3);
                }
            }

            throw new Exception("Color geneation error");*/
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