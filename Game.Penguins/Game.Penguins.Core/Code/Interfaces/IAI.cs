using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Code.Interfaces
{
    public interface IAi
    {
        int PlacementPenguinX { get; set; }
        int PlacementPenguinY { get; set; }
        IBoard MainBoard { get; }

        Coordinates PlacementPenguin();

        Coordinates ChoseFinalDestinationCell(int posX, int posY);
    }
}