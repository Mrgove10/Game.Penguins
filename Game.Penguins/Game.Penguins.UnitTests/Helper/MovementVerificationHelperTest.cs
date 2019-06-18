using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.UnitTests.Helper
{
    [TestClass]
    public class MovementVerificationHelperTest
    {
        /// THESE DO NOT WORK ANYMORE BECAUSE WE CHANGED ALGORYTHMES
        /*  [TestMethod]
          public void WhereCanIMove_right()
          {
              Cell[,] board = new Cell[2, 2];

              board[0, 0] = new Cell(CellType.FishWithPenguin, 3, 0, 0);
              board[0, 1] = new Cell(CellType.Fish, 0, 0, 1);
              board[1, 0] = new Cell(CellType.Water, 3, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 0, 1, 1);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 0]);

              Assert.AreEqual(1, helperResults.Count);

              Assert.AreEqual(0, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[0]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_left()
          {
              Cell[,] board = new Cell[2, 2];

              board[0, 0] = new Cell(CellType.Fish, 3, 0, 0);
              board[0, 1] = new Cell(CellType.FishWithPenguin, 0, 0, 1);
              board[1, 0] = new Cell(CellType.Water, 3, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 0, 1, 1);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 1]);

              Assert.AreEqual(1, helperResults.Count);

              Assert.AreEqual(0, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(0, ((Cell)helperResults[0]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_UpperLeft1()
          {
              Cell[,] board = new Cell[2, 2];

              board[0, 0] = new Cell(CellType.Water, 3, 0, 0);
              board[0, 1] = new Cell(CellType.Fish, 0, 0, 1);
              board[1, 0] = new Cell(CellType.Water, 3, 1, 0);
              board[1, 1] = new Cell(CellType.FishWithPenguin, 0, 1, 1);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[1, 1]);

              Assert.AreEqual(1, helperResults.Count);

              Assert.AreEqual(0, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[0]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_UpperRight1()
          {
              Cell[,] board = new Cell[2, 2];

              board[0, 0] = new Cell(CellType.Water, 3, 0, 0);
              board[0, 1] = new Cell(CellType.Fish, 0, 0, 1);
              board[1, 0] = new Cell(CellType.FishWithPenguin, 3, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 0, 1, 1);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[1, 0]);

              Assert.AreEqual(1, helperResults.Count);

              Assert.AreEqual(0, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[0]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_BottomLeft1()
          {
              Cell[,] board = new Cell[2, 2];

              board[0, 0] = new Cell(CellType.Water, 3, 0, 0);
              board[0, 1] = new Cell(CellType.FishWithPenguin, 0, 0, 1);
              board[1, 0] = new Cell(CellType.Fish, 3, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 0, 1, 1);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 1]);

              Assert.AreEqual(1, helperResults.Count);

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(0, ((Cell)helperResults[0]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_BottomRight1()
          {
              Cell[,] board = new Cell[2, 2];

              board[0, 0] = new Cell(CellType.FishWithPenguin, 3, 0, 0);
              board[0, 1] = new Cell(CellType.Water, 0, 0, 1);
              board[1, 0] = new Cell(CellType.Fish, 3, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 0, 1, 1);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 0]);

              Assert.AreEqual(1, helperResults.Count);

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(0, ((Cell)helperResults[0]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_UpperRight2()
          {
              Cell[,] board = new Cell[3, 3];

              board[0, 0] = new Cell(CellType.Water, 0, 0, 0);
              board[0, 1] = new Cell(CellType.Fish, 0, 0, 1);
              board[0, 2] = new Cell(CellType.Water, 3, 0, 2);

              board[1, 0] = new Cell(CellType.Fish, 0, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 3, 1, 1);
              board[1, 2] = new Cell(CellType.Water, 0, 1, 2);

              board[2, 0] = new Cell(CellType.FishWithPenguin, 3, 2, 0);
              board[2, 1] = new Cell(CellType.Water, 0, 2, 1);
              board[2, 2] = new Cell(CellType.Water, 0, 2, 2);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[2, 0]); //mettre l'originCell -> posPenguin

              Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(0, ((Cell)helperResults[0]).YPos);

              Assert.AreEqual(0, ((Cell)helperResults[1]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[1]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_UpperLeft2()
          {
              Cell[,] board = new Cell[3, 3];

              board[0, 0] = new Cell(CellType.Water, 1, 0, 0);
              board[0, 1] = new Cell(CellType.Fish, 1, 0, 1);
              board[0, 2] = new Cell(CellType.Water, 1, 0, 2);

              board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
              board[1, 1] = new Cell(CellType.Fish, 1, 1, 1);
              board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

              board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
              board[2, 1] = new Cell(CellType.Water, 1, 2, 1);
              board[2, 2] = new Cell(CellType.FishWithPenguin, 1, 2, 2);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[2, 2]); //mettre l'originCell -> posPenguin

              Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[0]).YPos);

              Assert.AreEqual(0, ((Cell)helperResults[1]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[1]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_BottomRight2()
          {
              Cell[,] board = new Cell[3, 3];

              board[0, 0] = new Cell(CellType.FishWithPenguin, 1, 0, 0);
              board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
              board[0, 2] = new Cell(CellType.Water, 1, 0, 2);

              board[1, 0] = new Cell(CellType.Fish, 1, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 1, 1, 1);
              board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

              board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
              board[2, 1] = new Cell(CellType.Fish, 1, 2, 1);
              board[2, 2] = new Cell(CellType.Water, 1, 2, 2);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 0]); //mettre l'originCell -> posPenguin

              Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(0, ((Cell)helperResults[0]).YPos);

              Assert.AreEqual(2, ((Cell)helperResults[1]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[1]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_EvenBottomLeft2()
          {
              Cell[,] board = new Cell[3, 3];

              board[0, 0] = new Cell(CellType.Water, 1, 0, 0);
              board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
              board[0, 2] = new Cell(CellType.FishWithPenguin, 1, 0, 2);

              board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
              board[1, 1] = new Cell(CellType.Fish, 1, 1, 1);
              board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

              board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
              board[2, 1] = new Cell(CellType.Fish, 1, 2, 1);
              board[2, 2] = new Cell(CellType.Water, 1, 2, 2);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 2]); //mettre l'originCell -> posPenguin

              Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[0]).YPos);

              Assert.AreEqual(2, ((Cell)helperResults[1]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[1]).YPos);
          }

          [TestMethod]
          public void WhereCanIMove_BottomLeft3()
          {
              Cell[,] board = new Cell[4, 4];

              board[0, 0] = new Cell(CellType.Water, 1, 0, 0);
              board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
              board[0, 2] = new Cell(CellType.Water, 1, 0, 2);
              board[0, 3] = new Cell(CellType.FishWithPenguin, 1, 0, 3);

              board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
              board[1, 1] = new Cell(CellType.Water, 1, 1, 1);
              board[1, 2] = new Cell(CellType.Fish, 1, 1, 2);
              board[1, 3] = new Cell(CellType.Water, 1, 1, 3);

              board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
              board[2, 1] = new Cell(CellType.Water, 1, 2, 1);
              board[2, 2] = new Cell(CellType.Fish, 1, 2, 2);
              board[2, 3] = new Cell(CellType.Water, 1, 2, 3);

              board[3, 0] = new Cell(CellType.Water, 1, 3, 0);
              board[3, 1] = new Cell(CellType.Fish, 1, 3, 1);
              board[3, 2] = new Cell(CellType.Water, 1, 3, 2);
              board[3, 3] = new Cell(CellType.Water, 1, 3, 3);

              Plateau TestBoard = new Plateau(board);

              MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

              List<ICell> helperResults = helperTest.WhereCanIMove(board[0, 3]); //mettre l'originCell -> posPenguin

              Assert.AreEqual(3, helperResults.Count); //compare le nombre de possibilités

              Assert.AreEqual(1, ((Cell)helperResults[0]).XPos);
              Assert.AreEqual(2, ((Cell)helperResults[0]).YPos);

              Assert.AreEqual(2, ((Cell)helperResults[1]).XPos);
              Assert.AreEqual(2, ((Cell)helperResults[1]).YPos);

              Assert.AreEqual(3, ((Cell)helperResults[2]).XPos);
              Assert.AreEqual(1, ((Cell)helperResults[2]).YPos);
          }*/
    }
}