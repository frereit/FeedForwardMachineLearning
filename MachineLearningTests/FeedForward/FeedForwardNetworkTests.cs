using Microsoft.VisualStudio.TestTools.UnitTesting;
using MachineLearning.FeedForward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.MatrixMath;

namespace MachineLearning.FeedForward.Tests
{
    [TestClass()]
    public class FeedForwardNetworkTests
    {
        [TestMethod()]
        public void FeedForwardNetworkTest()
        {
            FeedForwardNetwork network = new FeedForwardNetwork(2);
            network.AddLayer(2);
            network.AddLayer(1);
            Matrix output = network.ComputeOutputs(new Matrix(new[,]
            {
                {1.0},
                {0.0}
            }));
            Assert.IsNotNull(output);
        }

        [TestMethod()]
        public void FeedForwardNetworkTooSmallTest()
        {
            FeedForwardNetwork network = new FeedForwardNetwork(2);
            Assert.ThrowsException<FeedForwardNetworkTooSmallException>(delegate()
            {
                network.ComputeOutputs(new Matrix(new[,]
                {
                    {0.5},
                    {0.8}
                }));
            });
        }
    }
}