using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core
{
    public interface IAI
    {
        int PlacementPenguinX { get; set; }
        int PlacementPenguinY { get; set; }
        IBoard MainBoard { get; }

        int[] PlacementPenguin();

        void DetectionCases(int posX, int posY);
    }
}