using Game.Penguins.Core.Code.Player;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Game.Penguins.AI.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreatePlayerNormalName()
        {
            Player m = new Player("Adrien", PlayerType.Human,  1);
            Assert.IsTrue(m.Name == "Adrien");
        }

        [TestMethod]
        public void CreatePlayerNormalType()
        {
            Player m = new Player("Adrien", PlayerType.Human,  1);
            Assert.IsTrue(m.PlayerType == PlayerType.Human);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreatePlayerNormalExecivePenguins()
        {
            Player m = new Player("Adrien", PlayerType.Human,  6);
        }

        [TestMethod]
        public void CreatePlayerNormalNotExecivePenguins()
        {
            Player m = new Player("Adrien", PlayerType.Human,  3);
            Assert.IsTrue(m.Penguins == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreatePlayerNormalNegativePenguins()
        {
            Player m = new Player("Adrien", PlayerType.Human,  -1);
        }

        [TestMethod]
        public void CreatePlayerNormalPoint()
        {
            Player m = new Player("Adrien", PlayerType.Human,  1);
            Assert.IsTrue(m.Points == 0);
        }
    }
}