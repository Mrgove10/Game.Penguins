using Game.Penguins.Core.Code.Player;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreateNormalPlayer()
        {
            Player m = new Player("Adrien", PlayerType.Human, PlayerColor.Blue);
            Assert.IsTrue(m.Name == "Adrien");
        }
    }
}