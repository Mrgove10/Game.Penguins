using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core
{
    public interface IAI
    {
        int PlacementPenguinX { get; set; }
        int PlacementPenguinY { get; set; }
        IBoard plateau { get; }

        void PlacementPenguin();

        void DetectionCases(int posX, int posY);
    }
}