using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Code.GameBoard;
using Game.Penguins.Core.Code.Helper;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.UnitTests.Helper
{
    [TestClass]
    public class MovementVerificationHelperTest
    {
        [TestMethod]
        public void WhereCanIMove_right()
        {
            Cell[,] board = new Cell[2, 2];

            board[0, 0] = new Cell(CellType.FishWithPenguin, 3, 0, 0);
            board[0, 1] = new Cell(CellType.Fish, 0, 0, 1);
            board[1, 0] = new Cell(CellType.Water, 3, 1, 0);
            board[1, 1] = new Cell(CellType.Water, 0, 1, 1);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[0, 0]);

            Assert.AreEqual(1, helperResults.Count);

            Assert.AreEqual(0, helperResults[0].XPos);
            Assert.AreEqual(1, helperResults[0].YPos);

        }

        
    }
}
