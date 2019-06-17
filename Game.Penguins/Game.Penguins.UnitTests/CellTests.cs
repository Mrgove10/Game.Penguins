using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [TestClass]
    public class CellTests
    {
        //TEST CELL CREATION - CORRECT
        [TestMethod]
        public void CellCreation()
        {
            Cell c = new Cell(CellType.Fish, 3);
            Assert.IsTrue(c.FishCount == 3);
        }

        //TEST NUMBER OF FISHES - CORRECT
        [TestMethod]
        public void NumberOfFishTrue()
        {
            Cell cell = new Cell(CellType.Fish, 2);
            Assert.IsTrue(cell.FishCount == 2);
        }

        //TEST NUMBER OF FISHES - INCORRECT
        [TestMethod]
        public void NumberOfFishFalse()
        {
            Cell cell = new Cell(CellType.Fish, 3);
            Assert.IsFalse(cell.FishCount == 2);
        }
    }
}