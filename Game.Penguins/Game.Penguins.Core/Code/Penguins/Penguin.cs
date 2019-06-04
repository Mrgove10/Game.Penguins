using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Penguins
{
    public class Penguin : IPenguin
    {
        public IPlayer Player { get; set; }
        public int XPos;
        public int YPos;

        public Penguin(IPlayer player, int xPos, int yPos)
        {
            Player = player;
            XPos = xPos;
            YPos = yPos;
        }
    }
}