using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Code.Interfaces
{
    public interface IAi
    {
        //coordinates for AI's penguin placement (only)
        int PlacementPenguinX { get; set; }

        int PlacementPenguinY { get; set; }
        IBoard MainBoard { get; }

        Coordinates PlacementPenguin();

        //coordinates for AI's destination cell when moving
        Coordinates ChoseFinalDestinationCell(int posX, int posY);
    }
}