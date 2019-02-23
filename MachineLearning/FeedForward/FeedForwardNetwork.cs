using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.MatrixMath;

namespace MachineLearning.FeedForward
{
    public class FeedForwardNetwork
    {
        private readonly List<FeedForwardLayer> _layers;
        private FeedForwardLayer inputLayer;
        public IActivationFunction ActivationFunction = new ReLuActivation();

        public FeedForwardNetwork(int pInputNeurons)
        {
            inputLayer = new FeedForwardLayer(new Matrix(pInputNeurons, 1));
            _layers = new List<FeedForwardLayer>();
        }

        public void AddLayer(int pNeurons)
        {
            var newLayer = _layers.Count > 0
                ? new FeedForwardLayer(_layers.Last(), pNeurons, ActivationFunction)
                : new FeedForwardLayer(inputLayer, pNeurons, ActivationFunction);
            _layers.Add(newLayer);
        }

        public Matrix ComputeOutputs(Matrix pInputMatrix)
        {
            if(_layers.Count < 1) throw new FeedForwardNetworkTooSmallException("Network needs at least 2 layers (Input & Output)!");
            if (pInputMatrix.Rows != inputLayer.NeuronCount)
                throw new InputLayerNotMatchedException(
                    "Neurons of the input matrix do not match with neurons of the input layer.");
            inputLayer.Weights = pInputMatrix;
            return _layers.Last().Compute();
        }

        /// <summary>
        /// Runs a single training epoch on the given input values
        /// </summary>
        /// <param name="pInputs">Input Matrix Vectors.</param>
        /// <param name="pExpected">Expected Matrix Vectors. Used for error calculation.</param>
        /// <returns>Current Error</returns>
        public void Train(List<Matrix> pInputs, List<Matrix> pExpected, double pLearningRate)
        {
            if(pInputs.Count != pExpected.Count) throw new NotImplementedException(); //TODO: Implement this
            var deltaWeights = new List<Matrix>();
            var deltaBiases = new List<Matrix>();

            foreach (var layer in _layers)
            {
                deltaBiases.Add(new Matrix(layer.Bias.Rows, layer.Bias.Columns));
                deltaWeights.Add(new Matrix(layer.Weights.Rows, layer.Weights.Columns));
            }

            for (var trainingInputIndex = 0; trainingInputIndex < pInputs.Count; trainingInputIndex++)
            {
                var deltaGradients = Backprop(pInputs[trainingInputIndex], pExpected[trainingInputIndex]);
                for (var layerIndex = 0; layerIndex < _layers.Count; layerIndex++)
                {
                    deltaBiases[layerIndex] += deltaGradients[1][layerIndex];
                    deltaWeights[layerIndex] += deltaGradients[0][layerIndex];
                }
            }

            var scalar = pLearningRate / pInputs.Count;
            for (var layerIndex = 0; layerIndex < _layers.Count; layerIndex++)
            {
                _layers[layerIndex].Bias    += deltaBiases[layerIndex]  * scalar;
                _layers[layerIndex].Weights += deltaWeights[layerIndex] * scalar;
            }
        }

        /// <summary>
        /// Saves the network to the specified file.
        /// </summary>
        /// <param name="savePath"></param>
        public void Save(string savePath)
        {
            using (var dataSr = new StreamWriter(savePath + ".ffnn"))
            {
                foreach (var layer in _layers)
                {
                    dataSr.WriteLine("#");
                    for (var row = 0; row < layer.Weights.Rows; row++)
                    {
                        for (var col = 0; col < layer.Weights.Columns; col++) dataSr.Write(layer.Weights[row, col] + ";");
                        dataSr.Write(layer.Bias[row, 0]);
                        dataSr.WriteLine();
                    }
                }
            }
        }

        public void Load(string loadPath)
        {
            using (var dataSr = new StreamReader(loadPath + ".ffnn"))
            {
                var model = dataSr.ReadToEnd();
                var layers = model.Split('#');
                _layers.Clear();
                for (var layerInd = 1; layerInd < layers.Length; layerInd++)
                {
                    var layerS = layers[layerInd];
                    var lines = layerS.Trim().Split('\n');
                    var layer = layerInd > 1
                                ? new FeedForwardLayer(_layers[layerInd - 2], lines.Length, ActivationFunction)
                                : new FeedForwardLayer(inputLayer, lines.Length, ActivationFunction);
                    for (var neuronInd = 0; neuronInd < lines.Length; neuronInd++)
                    {
                        var line = lines[neuronInd];
                        var entries = line.Split(';');
                        for (var i = 0; i < entries.Length - 1; i++)
                        {
                            layer.Weights[neuronInd, i] = double.Parse(entries[i]);
                        }

                        layer.Bias[neuronInd, 0] = double.Parse(entries.Last());
                    }
                    _layers.Add(layer);
                }
            }
       
        }

        private List<List<Matrix>> Backprop(Matrix pInput, Matrix pExpected)
        {            
            var deltaWeights = new List<Matrix>();
            var deltaBiases = new List<Matrix>();

            foreach (var layer in _layers)
            {
                deltaBiases.Add(new Matrix(layer.Bias.Rows, layer.Bias.Columns));
                deltaWeights.Add(new Matrix(layer.Weights.Rows, layer.Weights.Columns));
            }

            ComputeOutputs(pInput);

            var m1 = CostFunction.MeanSquaredErrorDerivative(_layers.Last().ActivationsMatrix, pExpected);
            var m2 = ActivationFunction.Derivative(_layers.Last().WeightedInputMatrix);
            var delta = Matrix.HadamardProduct(m1, m2);
            deltaBiases[deltaBiases.Count - 1] = delta;
            if(_layers.Count > 1)
                deltaWeights[deltaWeights.Count - 1] = delta * _layers[_layers.Count-2].ActivationsMatrix.Transpose();
            else
                deltaWeights[deltaWeights.Count - 1] = delta * inputLayer.Weights.Transpose();
            for (var i = deltaWeights.Count-2; i >= 0; i--)
            {
                var activationMatrix = _layers[i].ActivationsMatrix;
                var derivedMatrix = ActivationFunction.Derivative(activationMatrix);
                delta = Matrix.HadamardProduct(_layers[i + 1].Weights.Transpose() * delta, derivedMatrix);
                deltaBiases[i] = delta;
                if (i == 0)
                    deltaWeights[i] = delta * inputLayer.Weights.Transpose();
                else
                    deltaWeights[i] = delta * _layers[i - 1].ActivationsMatrix.Transpose();
            }

            return new List<List<Matrix>>{deltaWeights, deltaBiases};
        }
    }
}
