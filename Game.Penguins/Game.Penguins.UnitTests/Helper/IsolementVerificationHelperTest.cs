using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.UnitTests.Helper
{
    [TestClass]
    public class IsolementVerificationHelperTest
    {
        [TestMethod]
        public void TestPenguinIsIsolated()
        {
            Cell[,] board = new Cell[3, 3];

            board[0, 0] = new Cell(CellType.Water, 1, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
            board[0, 2] = new Cell(CellType.Water, 1, 0, 2);

            board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
            board[1, 1] = new Cell(CellType.FishWithPenguin, 1, 1, 1);
            board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

            board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
            board[2, 1] = new Cell(CellType.Water, 1, 2, 1);
            board[2, 2] = new Cell(CellType.Water, 1, 2, 2);

            Plateau TestBoard = new Plateau(board);

            IsolationVerificationHelper helperTest = new IsolationVerificationHelper(TestBoard);

            bool IsIsolated = helperTest.VerifyIsolation(board[1, 1]);

            Assert.IsTrue(IsIsolated);
        }

        /* [TestMethod]
         public void TestPenguinIsNotIsolated()
         {
             Cell[,] board = new Cell[3, 3];

             board[0, 0] = new Cell(CellType.Fish, 1, 0, 0);
             board[0, 1] = new Cell(CellType.Fish, 1, 0, 1);
             board[0, 2] = new Cell(CellType.Fish, 1, 0, 2);

             board[1, 0] = new Cell(CellType.Fish, 1, 1, 0);
             board[1, 1] = new Cell(CellType.FishWithPenguin, 1, 1, 1);
             board[1, 2] = new Cell(CellType.Fish, 1, 1, 2);

             board[2, 0] = new Cell(CellType.Fish, 1, 2, 0);
             board[2, 1] = new Cell(CellType.Fish, 1, 2, 1);
             board[2, 2] = new Cell(CellType.Fish, 1, 2, 2);

             Plateau TestBoard = new Plateau(board);

             IsolationVerificationHelper helperTest = new IsolationVerificationHelper(TestBoard);

             bool IsIsolated = helperTest.VerifyIsolation(board[1, 1]);

             Assert.IsFalse(IsIsolated);
         }*/

        /* public void TestVerifyCellTrue()
         {
             Cell[,] board = new Cell[2, 2];

             board[0, 0] = new Cell(CellType.FishWithPenguin, 1, 0, 0);
             board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
             board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
             board[1, 1] = new Cell(CellType.Water, 1, 1, 1);

             Plateau TestBoard = new Plateau(board);

             IsolationVerificationHelper helperTest = new IsolationVerificationHelper(TestBoard);

             helperTest.VerifyCells(board[0, 0], 0, 1);

             Assert.IsFalse();
         }*/
    }
}