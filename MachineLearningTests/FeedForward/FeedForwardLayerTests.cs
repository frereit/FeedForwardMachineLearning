using Microsoft.VisualStudio.TestTools.UnitTesting;
using MachineLearning;
using MachineLearning.FeedForward;
using MachineLearning.MatrixMath;

namespace MachineLearning.FeedForward.Tests
{
    [TestClass()]
    public class FeedForwardLayerTests
    {
        [TestMethod()]
        public void XORNetworkTest()
        {
            Matrix inputMatrix00 = new Matrix(new double[,]
            {
                {0.0 },
                {0.0 },
            });

            FeedForwardLayer inputLayer = new FeedForwardLayer(inputMatrix00);
            FeedForwardLayer hiddenLayer = new FeedForwardLayer(inputLayer, 2, new BinaryStepActivation());
            FeedForwardLayer outputLayer = new FeedForwardLayer(hiddenLayer, 1, new BinaryStepActivation());
            hiddenLayer.Weights = new Matrix(new double[,]
            {
                {1.0, 1.0 },
                {-1.0, -1.0 }
            });
            hiddenLayer.Bias = new Matrix(new double[,]
            {
                {-0.5 },
                {1.5 }
            });
            outputLayer.Weights = new Matrix(new double[,]
            {
                {1.0, 1.0 }
            });
            outputLayer.Bias = new Matrix(new double[,]
            {
                {-1.5 }
            });
            Matrix outputMatrix00 = outputLayer.Compute();

            Matrix inputMatrix01 = new Matrix(new double[,]
            {
                {0.0 },
                {1.0 },
            });
            inputLayer.Weights = inputMatrix01;
            Matrix outputMatrix01 = outputLayer.Compute();

            Matrix inputMatrix10 = new Matrix(new double[,]
            {
                {1.0 },
                {0.0 },
            });
            inputLayer.Weights = inputMatrix01;
            Matrix outputMatrix10 = outputLayer.Compute();

            Matrix inputMatrix11 = new Matrix(new double[,]
            {
                {1.0 },
                {1.0 },
            });
            inputLayer.Weights = inputMatrix11;
            Matrix outputMatrix11 = outputLayer.Compute();

            Assert.AreEqual(-1, outputMatrix00[0, 0]);
            Assert.AreEqual(1, outputMatrix10[0, 0]);
            Assert.AreEqual(1, outputMatrix01[0, 0]);
            Assert.AreEqual(-1, outputMatrix11[0, 0]);
        }
    }
}