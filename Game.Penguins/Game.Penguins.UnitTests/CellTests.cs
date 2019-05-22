using System;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CellCreation()
        {
            Cell c = new Cell(CellType.Fish,3);
            Assert.IsTrue(c.FishCount == 3);
        }

        [TestMethod]
        public void Celle()
        {
            
        }

        /* [TestMethod]
         public void CellCreationError()
         {
             Cell c = new Cell(CellType.Fish, 5);
         //    Assert.ThrowsException<ArgumentOutOfRangeException>(System.Action);
         }*/
    }
}