using System;
using System.CodeDom.Compiler;
using System.Collections;

namespace MachineLearning.MatrixMath
{
    public class Matrix
    {
        private double[,] _matrix;

        public int Rows
        {
            get { return _matrix.GetLength(0); }
        }
        public int Columns
        {
            get { return _matrix.GetLength(1); }
        }

        public Matrix(double[,] pMatrix)
        {
            _matrix = pMatrix;
        }
        
        public Matrix(int pRows, int pColumns)
        {
            _matrix = new double[pRows, pColumns];
            for(int row = 0; row < Rows; row++)
            {
                for(int col = 0; col < Columns; col++)
                {
                    _matrix[row, col] = 0.0;
                }
            }
        }


        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if(m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw (new MatrixAdditionNotPossibleException("Matrix dimensions are not identical: Matrix1: " 
                                                                + m1.Rows + "x" + m1.Columns + ". Matrix2: " 
                                                                + m2.Rows + "x" + m2.Columns + "."));
            }
            Matrix result = new Matrix(m1.Rows, m1.Columns);
            for(int row = 0; row < m1.Rows; row++)
            {
                for(int col = 0; col < m1.Columns; col++)
                {
                    result[row, col] = m1[row, col] + m2[row, col];
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix m1, double operand)
        {
            Matrix result = new Matrix(m1.Rows, m1.Columns);
            for (int row = 0; row < m1.Rows; row++)
            {
                for (int col = 0; col < m1.Columns; col++)
                {
                    result[row, col] = m1[row, col] + operand;
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1 + (m2 * -1);
        }

        public static Matrix operator -(Matrix m1, double operand)
        {
            return m1 + (operand * -1);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if(m1.Columns != m2.Rows)
            {
                throw (new MatrixMultiplicationNotPossibleException("Column Count of Matrix 1 (" 
                                                            + m1.Columns + ") does not match up with Row Count of Matrix 2 (" 
                                                            + m2.Rows + ")."));
            }
            Matrix result = new Matrix(m1.Rows, m2.Columns);
            for(int m1Row = 0; m1Row < m1.Rows; m1Row++)
            {
                for(int m2Col = 0; m2Col < m2.Columns; m2Col++)
                {
                    double sum = 0;
                    for(int i = 0; i < m1.Columns; i++)
                    {
                        sum += m2[i, m2Col] * m1[m1Row, i];
                    }
                    result[m1Row, m2Col] = sum;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix m1, double scalar)
        {
            Matrix result = new Matrix(m1.Rows, m1.Columns);
            for(int row = 0; row < m1.Rows; row++)
            {
                for(int col = 0; col < m1.Columns; col++)
                {
                    result[row, col] = m1[row,col] * scalar;
                }
            }
            return result;
        }

        public static Matrix operator /(Matrix m1, double scalar)
        {
            return m1 * (1 / scalar);
        }

        public Matrix Transpose()
        {
            Matrix transposed = new Matrix(Columns, Rows);
            for(int row = 0; row < Rows; row++)
            {
                for(int col = 0; col < Columns; col++)
                {
                    transposed[col, row] = this[row, col];
                }
            }
            return transposed;
        }

        public bool Equals(Matrix m1)
        {
            if (m1.Rows != Rows) return false;
            if (m1.Columns != Columns) return false;
            for(int row = 0; row < Rows; row++)
            {
                for(int col = 0; col < Columns; col++)
                {
                    if (this[row, col] != m1[row, col]) return false;
                }
            }
            return true;
        }

        public static Matrix Ones(int pRows, int pColumns)
        {
            Matrix ones = new Matrix(pRows, pColumns);
            for(int row = 0; row < pRows; row++)
            {
                for(int col = 0; col < pColumns; col++)
                {
                    ones[pRows, pColumns] = 1.0;
                }
            }
            return ones;
        }

        public static Matrix Identitiy(int pSize)
        {
            Matrix identity = new Matrix(pSize, pSize);
            for(int row = 0; row < pSize; row++)
            {
                for(int col = 0; col < pSize; col++)
                {
                    if(row == col)
                    {
                        identity[row, col] = 1.0;
                    }
                }
            }
            return identity;
        }

        public static Matrix Random(int pRows, int pColumns)
        {
            return Matrix.Random(pRows, pColumns, -1.0, 1.0);
        }

        public static Matrix Random(int pRows, int pColumns, double pMin, double pMax)
        {
            Matrix rnd = new Matrix(pRows, pColumns);
            for(int row = 0; row < pRows; row++)
            {
                for(int col = 0; col < pColumns; col++)
                {
                    rnd[row, col] = Globals.rng.NextDouble() * (pMax - pMin) + pMin;
                }
            }
            return rnd;
        }

        public bool IsVector()
        {
            if (Rows == 1 || Columns == 1) return true;
            return false;
        }

        public double[] ToPackedArray()
        {
            double[] result = new double[Rows*Columns];
            for(int row = 0; row < Rows; row++)
            {
                for(int col = 0; col < Columns; col++)
                {
                    result[row * Columns + col] = this[row, col];
                }
            }
            return result;
        }

        public static double DotProduct(Matrix pMatrix1, Matrix pMatrix2)
        {
            if(!pMatrix2.IsVector() || !pMatrix1.IsVector())
            {
                throw new DotProductNotPossibleException("To take the dot product, both matrixes must be vectors.");
            }

            double result = 0;
            double[] aArray = pMatrix1.ToPackedArray();
            double[] bArray = pMatrix2.ToPackedArray();
            if(aArray.Length != bArray.Length)
            {
                throw new DotProductNotPossibleException("To take the dot product, both matrixes must be of the same length.");
            }
            int length = aArray.Length;
            for(int i = 0; i < length; i++)
            {
                result += aArray[i] * bArray[i];
            }
            return result;
        }

        public static Matrix HadamardProduct(Matrix pMatrix1, Matrix pMatrix2)
        {
            Matrix result = new Matrix(pMatrix1.Rows, pMatrix1.Columns);
            for (int row = 0; row < result.Rows; row++)
            {
                for (int col = 0; col < result.Columns; col++)
                {
                    result[row, col] = pMatrix1[row, col] * pMatrix2[row, col];
                }
            }

            return result;
        }

        public Matrix GetCol(int col)
        {
            Matrix result = new Matrix(Rows, 1);
            for (int row = 0; row < Rows; row++)
            {
                result[row, 0] = this[row, col];
            }
            return result;
        }

        public Matrix GetRow(int row)
        {
            Matrix result = new Matrix(1, Columns);
            for(int col = 0; col < Columns; col++)
            {
                result[0, col] = this[row, col];
            }
            return result;
        }
        public double this[int i, int j]
        {
            get { return _matrix[i, j]; }
            set { _matrix[i, j] = value; }
        }
    }
}
