﻿using System;
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

            List<Cell> helperResults = helperTest.WhereCanIMove(board[0, 1]);

            Assert.AreEqual(1, helperResults.Count);

            Assert.AreEqual(0, helperResults[0].XPos);
            Assert.AreEqual(0, helperResults[0].YPos);
        }

        [TestMethod]
        public void WhereCanIMove_UpperLeft1()
        {
            Cell[,] board = new Cell[2, 2];

            board[0, 0] = new Cell(CellType.Fish, 3, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 0, 0, 1);
            board[1, 0] = new Cell(CellType.Water, 3, 1, 0);
            board[1, 1] = new Cell(CellType.FishWithPenguin, 0, 1, 1);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[1, 1]);

            Assert.AreEqual(1, helperResults.Count);

            Assert.AreEqual(0, helperResults[0].XPos);
            Assert.AreEqual(0, helperResults[0].YPos);
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

            List<Cell> helperResults = helperTest.WhereCanIMove(board[1, 0]);

            Assert.AreEqual(1, helperResults.Count);

            Assert.AreEqual(0, helperResults[0].XPos);
            Assert.AreEqual(1, helperResults[0].YPos);
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

            List<Cell> helperResults = helperTest.WhereCanIMove(board[0, 1]);

            Assert.AreEqual(1, helperResults.Count);

            Assert.AreEqual(1, helperResults[0].XPos);
            Assert.AreEqual(0, helperResults[0].YPos);
        }

        [TestMethod]
        public void WhereCanIMove_BottomRight1()
        {
            Cell[,] board = new Cell[2, 2];

            board[0, 0] = new Cell(CellType.FishWithPenguin, 3, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 0, 0, 1);
            board[1, 0] = new Cell(CellType.Water, 3, 1, 0);
            board[1, 1] = new Cell(CellType.Fish, 0, 1, 1);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[0, 0]);

            Assert.AreEqual(1, helperResults.Count);

            Assert.AreEqual(1, helperResults[0].XPos);
            Assert.AreEqual(1, helperResults[0].YPos);
        }

        [TestMethod]
        public void WhereCanIMove_UpperRight2()
        {
            Cell[,] board = new Cell[3, 3];

            board[0, 0] = new Cell(CellType.Water, 0, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 0, 0, 1);
            board[0, 2] = new Cell(CellType.Fish, 3, 0, 2);

            board[1, 0] = new Cell(CellType.Water, 0, 1, 0);
            board[1, 1] = new Cell(CellType.Fish, 3, 1, 1);
            board[1, 2] = new Cell(CellType.Water, 0, 1, 2);

            board[2, 0] = new Cell(CellType.FishWithPenguin, 3, 2, 0);
            board[2, 1] = new Cell(CellType.Water, 0, 2, 1);
            board[2, 2] = new Cell(CellType.Water, 0, 2, 2);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[2, 0]); //mettre l'originCell -> posPenguin

            Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

            Assert.AreEqual(1, helperResults[0].XPos); 
            Assert.AreEqual(1, helperResults[0].YPos); 

            Assert.AreEqual(0, helperResults[1].XPos); 
            Assert.AreEqual(2, helperResults[1].YPos);
        }

        [TestMethod]
        public void WhereCanIMove_UpperLeft2()
        {
            Cell[,] board = new Cell[3, 3];

            board[0, 0] = new Cell(CellType.Fish, 1, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
            board[0, 2] = new Cell(CellType.Water, 1, 0, 2);

            board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
            board[1, 1] = new Cell(CellType.Fish, 1, 1, 1);
            board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

            board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
            board[2, 1] = new Cell(CellType.Water, 1, 2, 1);
            board[2, 2] = new Cell(CellType.FishWithPenguin, 1, 2, 2);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[2, 2]); //mettre l'originCell -> posPenguin

            Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

            Assert.AreEqual(1, helperResults[0].XPos);
            Assert.AreEqual(1, helperResults[0].YPos);

            Assert.AreEqual(0, helperResults[1].XPos);
            Assert.AreEqual(0, helperResults[1].YPos);
        }

        [TestMethod]
        public void WhereCanIMove_BottomRight2()
        {
            Cell[,] board = new Cell[3, 3];

            board[0, 0] = new Cell(CellType.FishWithPenguin, 1, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
            board[0, 2] = new Cell(CellType.Water, 1, 0, 2);

            board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
            board[1, 1] = new Cell(CellType.Fish, 1, 1, 1);
            board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

            board[2, 0] = new Cell(CellType.Water, 1, 2, 0);
            board[2, 1] = new Cell(CellType.Water, 1, 2, 1);
            board[2, 2] = new Cell(CellType.Fish, 1, 2, 2);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[0, 0]); //mettre l'originCell -> posPenguin

            Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

            Assert.AreEqual(1, helperResults[0].XPos);
            Assert.AreEqual(1, helperResults[0].YPos);

            Assert.AreEqual(0, helperResults[1].XPos);
            Assert.AreEqual(2, helperResults[1].YPos);
        }

        [TestMethod]
        public void WhereCanIMove_BottomLeft2()
        {
            Cell[,] board = new Cell[3, 3];

            board[0, 0] = new Cell(CellType.Water, 1, 0, 0);
            board[0, 1] = new Cell(CellType.Water, 1, 0, 1);
            board[0, 2] = new Cell(CellType.FishWithPenguin, 1, 0, 2);

            board[1, 0] = new Cell(CellType.Water, 1, 1, 0);
            board[1, 1] = new Cell(CellType.Fish, 1, 1, 1);
            board[1, 2] = new Cell(CellType.Water, 1, 1, 2);

            board[2, 0] = new Cell(CellType.Fish, 1, 2, 0);
            board[2, 1] = new Cell(CellType.Water, 1, 2, 1);
            board[2, 2] = new Cell(CellType.Water, 1, 2, 2);


            Plateau TestBoard = new Plateau(board);

            MovementVerificationHelper helperTest = new MovementVerificationHelper(TestBoard);

            List<Cell> helperResults = helperTest.WhereCanIMove(board[0, 2]); //mettre l'originCell -> posPenguin

            Assert.AreEqual(2, helperResults.Count); //compare le nombre de possibilités

            Assert.AreEqual(1, helperResults[0].XPos);
            Assert.AreEqual(1, helperResults[0].YPos);

            Assert.AreEqual(2, helperResults[1].XPos);
            Assert.AreEqual(0, helperResults[1].YPos);
        }

    }
}
