using Game.Penguins.Core.Interfaces.Game.GameBoard;
using System.Collections.Generic;

namespace Game.Penguins.Core
{
    public interface IAI
    {
        int PlacementPenguinX { get; set; }
        int PlacementPenguinY { get; set; }
        IBoard MainBoard { get; }

        List<int> PlacementPenguin();

        List<int> DetectionCases(int posX, int posY);
    }
}