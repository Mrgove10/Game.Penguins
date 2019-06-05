using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        // CREATE PLAYER WITH A NORMAL NAME  - CORRECT
        [TestMethod]
        public void CreatePlayerNormal()
        {
            Player p = new Player("Foo", PlayerType.Human);
            Assert.IsTrue(p.Name == "Foo" && p.PlayerType == PlayerType.Human);
        }

        //TEST CREATE A PLAYER WITH A POSSIBLY ANNOYING NAME - CORRECT
        [TestMethod]
        public void CreatePlayerNormalEmoji()
        {
            Player p = new Player("👩‍💻🙄💕😁✌🤞✌", PlayerType.Human);
            Assert.IsTrue(p.Name == "👩‍💻🙄💕😁✌🤞✌");
        }

        //TEST UPDATE SCORE - CORRECT
        [TestMethod]
        public void UpdatePlayerScoreTrue()
        {
            Player playerTest = new Player("Foo", PlayerType.Human);
            PointHelper PointManager = new PointHelper();

            playerTest.Points = 6;
            int pointsToAdd = 15;
            PointManager.UpdatePlayerPoints(playerTest, pointsToAdd);

            Assert.IsTrue(playerTest.Points == 21);
        }

        //TEST UPDATE SCORE - INCORRECT
        [TestMethod]
        public void UpdatePlayerScoreFalse()
        {
            Player playerTest = new Player("Foo", PlayerType.Human);
            PointHelper PointManager = new PointHelper();

            playerTest.Points = 6;
            int pointsToAdd = 1;
            PointManager.UpdatePlayerPoints(playerTest, pointsToAdd);

            Assert.IsFalse(playerTest.Points == 21);
        }
    }
}