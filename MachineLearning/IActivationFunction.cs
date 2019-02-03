using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public interface IActivationFunction
    {
        double Compute(double input);
    }

    public class ReLuActivation : IActivationFunction
    {
        public double Compute(double input)
        {
            return Math.Max(input, 0);
        }
    }

    public class ThresholdActivation : IActivationFunction
    {
        private double treshold;

        public ThresholdActivation(double pThreshold)
        {
            this.treshold = pThreshold;
        }

        public double Compute(double input)
        {
            if (input > treshold) return 1;
            return -1;
        }
    }
}
