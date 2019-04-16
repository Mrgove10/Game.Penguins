using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Penguins
{
    public class Penguin : IPenguin
    {
        public IPlayer Player { get; set; }

        public Penguin(IPlayer PlayerAppartenance)
        {
            Player = PlayerAppartenance;
        }
    }
}