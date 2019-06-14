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
            /*
            MainGame EventHandler += delegate (object sender, System.EventArgs e)
            {
                statsUpdated = true;
            };
           */
            Plateau gb = new Plateau(2, 2);

            Assert.IsTrue(gb.Board.Length == 4);
        }

        //TODO : comment
        [TestMethod]
        public void GbCreationFalse()
        {
            Plateau gbFalse = new Plateau(4, 4);
            Assert.IsFalse(gbFalse.Board.Length == 4);
        }
    }
}