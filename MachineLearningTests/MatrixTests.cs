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
            Assert.AreEqual(0.0, m3[1,2]);

            Assert.AreEqual(2, m1.Rows);
            Assert.AreEqual(3, m1.Columns);
            Assert.AreEqual(2.0, m1[0,1]);
        }

        [TestMethod()]
        public void MatrixIsVectorTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0 }
            });
            Assert.IsTrue(m1.IsVector());

            Matrix m2 = new Matrix(new double[,]
            {
                {5.0 },
                {6.0 },
                {7.0 },
                {8.0 },
            });
            Assert.IsTrue(m2.IsVector());

            Matrix m3 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
            });
            Assert.IsFalse(m3.IsVector());
        }

        [TestMethod()]
        public void MatrixDotProductTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0 }
            });
            Matrix m2 = new Matrix(new double[,]
            {
                {5.0 },
                {6.0 },
                {7.0 },
                {8.0 },
            });
            double dotProductActual = m1.DotProduct(m2);
            double dotProductExpected = 70;
            Assert.AreEqual(dotProductExpected, dotProductActual);
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

        [TestMethod()]
        public void MatrixGetRowTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 }
            });
            Matrix m2 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0 },
            });

            Matrix m3 = new Matrix(new double[,]
{
                {1.0, 2.0 },
                {3.0, 4.0 },
                {5.0, 6.0 },
                {7.0, 8.0 },
            });

            Matrix m4 = new Matrix(new double[,]
            {
                {1.0, 2.0 }
            });

            Assert.IsTrue(m3.GetRow(0).Equals(m4));
            Assert.IsTrue(m1.GetRow(0).Equals(m2));
        }

        [TestMethod()]
        public void MatrixGetColumnTest()
        {
            Matrix m1 = new Matrix(new double[,]
            {
                {1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0 }
            });

            Matrix m2 = new Matrix(new double[,]
            {
                {1.0 },
                {4.0 },
            });
            Assert.IsTrue(m1.GetCol(0).Equals(m2));
        }
    }
}