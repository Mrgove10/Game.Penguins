using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreatePlayerNormal()
        {
            Player p = new Player("Foo", PlayerType.Human);
            Assert.IsTrue(p.Name == "Foo" && p.PlayerType == PlayerType.Human);
        }

        [TestMethod]
        public void CreatePlayerNormalEmoji()
        {
            Player p = new Player("👩‍💻🙄💕😁✌🤞✌", PlayerType.Human);
            Assert.IsTrue(p.Name == "👩‍💻🙄💕😁✌🤞✌");
        }
    }
}