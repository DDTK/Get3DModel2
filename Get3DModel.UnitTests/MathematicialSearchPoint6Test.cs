﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using CalculatedBlock;
using ParsingInputData;

namespace Get3DModel.UnitTests
{
    [TestClass]
    public class MathematicialSearchPoint6Test
    {
        [TestMethod]
        public void MathematicialSearchPoint_6Test()          //Проверка ядер свертки
        {
            //arrange
            MathematicialSearchPoint core = new MathematicialSearchPoint6();

            //act
            double[,] exepectedGx = new double[,] { { 1, 1, 0, -1, -1 }, { 1, 2, 0, -2, -1 }, { 2, 3, 0, -3, -2 },{ 1, 2, 0, -2, -1 }, { 1, 1, 0, -1, -1 } };
            double[,] exepectedGy = new double[,] { { -1, -1, -2, -1, -1 }, { -1, -2, -3, -2, -1 }, { 0, 0, 0, 0, 0 },{ 1, 2, 3, 2, 1 }, { 1, 1, 2, 1, 1 } };
            double[,] actualGx = core.XMatrix;
            double[,] actualGy = core.YMatrix;

            //assert
            Debug.WriteLine("Проверка ядер  свертки Gx и Gy");
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(exepectedGx[i, j], actualGx[i, j]);
                    Assert.AreEqual(exepectedGy[i, j], actualGy[i, j]);
                }
        }

        [TestMethod]
        public void gradientAtPoint6_CornerPoint_Test()              //Проверка градиента  угловой точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            //Blue[i, j] = 0.3 * img.GetPixel(i, j).R + 0.59 * img.GetPixel(i, j).G + 0.11 * img.GetPixel(i, j).B;    //Заполнение монохромного изображения

            MathematicialSearchPoint core = new MathematicialSearchPoint6();
            core.setImage(img);
            double[,] X = new double[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    X[i, j] = 0;
            for (int i = 2; i < 5; i++)
                for (int j = 2; j < 5; j++)
                    X[i, j] = img.GetPixel(i - 2, j - 2).B;               //Матрица окружения точки (0,0)

            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = Math.Sqrt(gradX * gradX + gradY * gradY);
            double actual = core.gradientAtPoint(0, 0);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint6_BoundaryPoint_Test()              //Проверка градиента  граничной точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png"); ;    //Заполнение монохромного изображения

            MathematicialSearchPoint core = new MathematicialSearchPoint6();
            core.setImage(img);

            double[,] X = new double[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    X[i, j] = 0;
            for (int i = 797; i < 800; i++)
                for (int j = 1; j < 6; j++)
                    X[i - 797, j - 1] = img.GetPixel(i, j).B;             //Матрица окружения точки (799,3)

            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = Math.Sqrt(gradX * gradX + gradY * gradY);
            double actual = core.gradientAtPoint(799, 3);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint6_IntPoint_Test()              //Проверка градиента  внутренней точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            MathematicialSearchPoint core = new MathematicialSearchPoint6();
            core.setImage(img);

            double[,] X = new double[5, 5];
            for (int i = 600; i < 605; i++)
                for (int j = 600; j < 605; j++)
                    X[i - 600, j - 600] = img.GetPixel(i, j).B;   //Матрица окружения точки (602,602)
            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = Math.Sqrt(gradX * gradX + gradY * gradY);
            double actual = core.gradientAtPoint(602, 602);
            //assert
            Assert.AreEqual(exepected, actual);
        }
    }
}
