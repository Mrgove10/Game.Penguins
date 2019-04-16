using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core
{
    public interface IAI
    {
        IBoard plateau { get; }

        void DetectionCases(int posX, int posY);
    }
}
