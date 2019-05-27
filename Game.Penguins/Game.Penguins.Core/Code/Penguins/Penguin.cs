using System;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Penguins
{
    public class Penguin : IPenguin
    {
        public IPlayer Player { get; set; }
        public Guid ID { get; set; }
        public int XPos;
        public int YPos;
        public Penguin(IPlayer PlayerAppartenance)
        {
            ID = new Guid();
            XPos = 0;
            YPos = 0;
            Player = PlayerAppartenance;
        }
    }
}