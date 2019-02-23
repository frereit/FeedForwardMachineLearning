using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.MatrixMath;

namespace MachineLearning
{
    public interface IActivationFunction
    {
        double Compute(double pInput);

        double Derivative(double pInput);

        Matrix Derivative(Matrix pInput);
    }

    public class ReLuActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return Math.Max(pInput, 0);
        }

        public double Derivative(double pInput)
        {
            return pInput > 0 ? 1 : 0;
        }

        public Matrix Derivative(Matrix pInput)
        {
            Matrix result = new Matrix(pInput.Rows, pInput.Columns);
            for (int row = 0; row < pInput.Rows; row++)
            {
                for (int col = 0; col < pInput.Columns; col++)
                {
                    result[row, col] = Derivative(pInput[row, col]);
                }
            }

            return result;
        }
    }

    public class LinearActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return pInput;
        }

        public double Derivative(double pInput)
        {
            return 1;
        }

        public Matrix Derivative(Matrix pInput)
        {
            Matrix result = new Matrix(pInput.Rows, pInput.Columns);
            for (int row = 0; row < pInput.Rows; row++)
            {
                for (int col = 0; col < pInput.Columns; col++)
                {
                    result[row, col] = Derivative(pInput[row, col]);
                }
            }

            return result;
        }
    }

    public class SigmoidActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            double denominator = 1 + Math.Pow(Math.E, -pInput);
            return 1 / denominator;
        }

        public double Derivative(double pInput)
        {
            double sigmoid = Compute(pInput);
            return sigmoid * (1 - sigmoid);
        }       
        
        public Matrix Derivative(Matrix pInput)
        {
            Matrix result = new Matrix(pInput.Rows, pInput.Columns);
            for (int row = 0; row < pInput.Rows; row++)
            {
                for (int col = 0; col < pInput.Columns; col++)
                {
                    result[row, col] = Derivative(pInput[row, col]);
                }
            }

            return result;
        }
    }

    public class TanHActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            double nominator = Math.Pow(Math.E, pInput) - Math.Pow(Math.E, -pInput);
            double denominator = Math.Pow(Math.E, pInput) + Math.Pow(Math.E, -pInput);
            return nominator / denominator;
        }

        public double Derivative(double pInput)
        {
            return 1 - Math.Pow(Compute(pInput), 2);
        }

        public Matrix Derivative(Matrix pInput)
        {
            Matrix result = new Matrix(pInput.Rows, pInput.Columns);
            for (int row = 0; row < pInput.Rows; row++)
            {
                for (int col = 0; col < pInput.Columns; col++)
                {
                    result[row, col] = Derivative(pInput[row, col]);
                }
            }

            return result;
        }
    }
    
    public class BinaryStepActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return pInput > 0 ? 1 : -1;
        }

        public double Derivative(double pInput)
        {
            throw new DerivativeNotPossibleException("The Binary Step Function cannot be derived.");
        }

        public Matrix Derivative(Matrix pInput)
        {
            throw new DerivativeNotPossibleException("The Binary Step Function cannot be derived.");
        }
    }
}
