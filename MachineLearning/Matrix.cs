using System;
using System.Collections;

namespace MachineLearning
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
                throw (new MatrixAdditionNotPossibleException("Matrix dimensions are not indentical: Matrix1: " 
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
                throw (new DotProductNotPossibleException("Column Count of Matrix 1 (" 
                                                            + m1.Rows + ") does not match up with Row Count of Matrix 2 (" 
                                                            + m2.Columns + ")."));
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

        public static Matrix Identitiy(int size)
        {
            Matrix identity = new Matrix(size, size);
            for(int row = 0; row < size; row++)
            {
                for(int col = 0; col < size; col++)
                {
                    if(row == col)
                    {
                        identity[row, col] = 1.0;
                    }
                }
            }
            return identity;
        }

        public double this[int i, int j]
        {
            get { return _matrix[i, j]; }
            set { _matrix[i, j] = value; }
        }
    }
}
