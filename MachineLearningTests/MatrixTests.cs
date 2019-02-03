using Microsoft.VisualStudio.TestTools.UnitTesting;
using MachineLearning;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void MatrixInitTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
            });
            Matrix m2 = new Matrix(new double[,]
            {
                { 7.0,  8.0},
                { 9.0, 10.0},
                {11.0, 12.0},
            });

            Matrix m3 = new Matrix(5, 5);
            Assert.AreEqual(m3[1,2], 0.0);

            Assert.AreEqual(m1.Rows, 2);
            Assert.AreEqual(m1.Columns, 3);
            Assert.AreEqual(m1[0,1], 2.0);
        }

        [TestMethod()]
        public void MatrixMatmulTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
            });
            Matrix m2 = new Matrix(new double[,]
            {
                { 7.0,  8.0},
                { 9.0, 10.0},
                {11.0, 12.0},
            });
            Matrix m3 = new Matrix(new double[,]
            {
                { 7.0,  8.0},
                { 9.0, 10.0},
                {11.0, 12.0},
                {13.0, 14.0},
            });

            Matrix dotProduct = m1 * m2;
            Matrix dotProductActual = new Matrix(new double[,]
            {
                { 58.0,  64.0},
                {139.0, 154.0},
            });
            Assert.IsTrue(dotProduct.Equals(dotProductActual));
            Assert.ThrowsException<DotProductNotPossibleException>(delegate () { Matrix _ = m1 * m3; });
        }

        [TestMethod()]
        public void MatrixScalarMulTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
            });

            Matrix scalarProduct = m1 * 2;

            Matrix scalarProductActual = new Matrix(new double[,]
            {
                {2.0,  4.0,  6.0},
                {8.0, 10.0, 12.0},
            });

            Assert.IsTrue(scalarProduct.Equals(scalarProductActual));
        }

        [TestMethod()]
        public void MatrixScalarDivTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
            });

            Matrix scalarDivision = m1 / 2;

            Matrix scalarDivisionActual = new Matrix(new double[,]
            {
                {0.5,  1.0,  1.5},
                {2.0, 2.5, 3.0},
            });

            Assert.IsTrue(scalarDivision.Equals(scalarDivisionActual));
        }

        [TestMethod()]
        public void MatrixAdditionTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0 },
                {4.0, 5.0 },
            });
            Matrix m2 = new Matrix(new double[,]
            {
                { 7.0,  8.0},
                { 9.0, 10.0},
            });

            Matrix m3 = new Matrix(new double[,]
            {
                { 7.0,  8.0, 5.0},
                { 9.0, 10.0, 6.0},
            });

            Matrix matrixAddition = m1 + m2;
            Matrix matrixAdditionActual = new Matrix(new double[,]
            {
                { 8.0, 10.0},
                {13.0, 15.0},
            });

            Assert.IsTrue(matrixAddition.Equals(matrixAdditionActual));

            Assert.ThrowsException<MatrixAdditionNotPossibleException>(delegate() { Matrix _ = m1 + m3; });
        }

        [TestMethod()]
        public void MatrixTranspositionTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0},
                {3.0, 4.0},
                {5.0, 6.0},
            });

            Matrix transposed = m1.Transpose();
            Matrix transposedActual = new Matrix(new double[,]
            {
                {1.0, 3.0, 5.0 },
                {2.0, 4.0, 6.0 },
            });
            Assert.IsTrue(transposed.Equals(transposedActual));
        }

        [TestMethod()]
        public void MatrixEqualityTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 }
            });
            Matrix m2 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 }
            });
            Matrix m3 = new Matrix(new double[,]
            {
                {1.0, 2.0},
                {3.0, 4.0},
                {5.0, 6.0},
            });

            Assert.IsFalse(m2.Equals(m3));
            Assert.IsTrue(m1.Equals(m2));
        }
    }
}