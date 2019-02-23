using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.MatrixMath;

namespace MachineLearning.FeedForward
{

    public class FeedForwardLayer
    {
        public Matrix Weights { get; set; }
        public Matrix Bias { get; set; }

        public Matrix WeightedInputMatrix;
        public Matrix ActivationsMatrix;
        public int NeuronCount => Weights.Rows;
        private readonly FeedForwardLayer _input;
        private readonly IActivationFunction _activation;
        public bool IsInputLayer { get; }

        public FeedForwardLayer(FeedForwardLayer pInputLayer, int pNeurons, IActivationFunction pActivationFunction)
        {
            _input = pInputLayer;
            _activation = pActivationFunction;
            Weights = Matrix.Random(pNeurons, pInputLayer.Weights.Rows);
            Bias = new Matrix(pNeurons, 1);
            ActivationsMatrix = new Matrix(Weights.Rows, 1);
            WeightedInputMatrix = new Matrix(Weights.Rows, 1);
        }

        public FeedForwardLayer(Matrix output)
        {
            IsInputLayer = true;
            Weights = output;
        }

        public Matrix Compute()
        {
            if (IsInputLayer) return Weights;
            var inputMatrix = _input.Compute();
            for(var row = 0; row < ActivationsMatrix.Rows; row++)
            {
                var neuronWeights = Weights.GetRow(row);
                WeightedInputMatrix[row, 0] = Matrix.DotProduct(inputMatrix, neuronWeights) + Bias[row, 0];
                var activated = _activation.Compute(WeightedInputMatrix[row, 0]);
                ActivationsMatrix[row, 0] = activated;
            }
            return ActivationsMatrix;
        }
    }
}
