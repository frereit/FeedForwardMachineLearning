using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public interface IActivationFunction
    {
        double Compute(double pInput);
    }

    public class ReLuActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return Math.Max(pInput, 0);
        }
    }

    public class ThresholdActivation : IActivationFunction
    {
        private readonly double treshold;

        public ThresholdActivation(double pThreshold)
        {
            this.treshold = pThreshold;
        }

        public double Compute(double pInput)
        {
            if (pInput > treshold) return 1;
            return -1;
        }
    }

    public class LinearActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return pInput;
        }
    }

    public class BinaryStepActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return pInput < 0 ? 0 : 1;
        }
    }

    public class SigmoidActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            double denominator = 1 + Math.Pow(Math.E, -pInput);
            return 1 / denominator;
        }
    }

    public class GaussianActivation : IActivationFunction
    {
        public double Compute(double pInput)
        {
            return Math.Pow(Math.E, Math.Pow(-pInput, 2));
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
    }
}
