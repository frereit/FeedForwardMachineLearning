using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{

    public class FeedForwardLayer
    {
        public Matrix Weights { get; set; }
        public Matrix Bias { get; set; }
        readonly FeedForwardLayer input;
        readonly IActivationFunction activation;
        public bool IsInputLayer { get; }

        public FeedForwardLayer(FeedForwardLayer pInputLayer, int pInputNeurons, int pNeurons, IActivationFunction pActivationFunction)
        {
            input = pInputLayer;
            activation = pActivationFunction;
            Weights = Matrix.Random(pNeurons, pInputNeurons);
            Bias = new Matrix(pNeurons, 1) + 1;
        }

        public FeedForwardLayer(Matrix output)
        {
            IsInputLayer = true;
            Weights = output;
        }

        public Matrix Compute()
        {
            if (IsInputLayer) return Weights;
            Matrix inputMatrix = input.Compute();
            Matrix outputMatrix = new Matrix(Weights.Rows, 1);
            for(int row = 0; row < outputMatrix.Rows; row++)
            {
                Matrix neuronWeights = Weights.GetRow(row);
                double output = activation.Compute(inputMatrix.DotProduct(neuronWeights) + Bias[row, 0]);
                outputMatrix[row, 0] = output;
            }
            return outputMatrix;
        }
    }
}
