using Game.Penguins.Core.Code.GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.UnitTests
{
   [TestClass]
    public class GameBoardTest
    {
        [TestMethod]
        public void GbCreation()
        {
            Plateau gb = new Plateau(10,10);
            
            Assert.IsTrue();//TODO : make this correct , idk how
        }
    }
}