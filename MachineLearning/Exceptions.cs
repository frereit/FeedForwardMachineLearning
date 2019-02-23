using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    [Serializable]
    public class DotProductNotPossibleException : Exception
    {
        public DotProductNotPossibleException(){ }

        public DotProductNotPossibleException(string message) : base(message) { }

        public DotProductNotPossibleException(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]
    public class MatrixMultiplicationNotPossibleException : Exception
    {
        public MatrixMultiplicationNotPossibleException() { }

        public MatrixMultiplicationNotPossibleException(string message) : base(message) { }

        public MatrixMultiplicationNotPossibleException(string message, Exception inner) : base(message) { }
    }

    [Serializable]
    public class MatrixAdditionNotPossibleException : Exception
    {
        public MatrixAdditionNotPossibleException() { }

        public MatrixAdditionNotPossibleException(string message) : base(message) { }

        public MatrixAdditionNotPossibleException(string message, Exception inner) : base(message) { }
    }

    [Serializable]
    public class FeedForwardNetworkTooSmallException : Exception
    {
        public FeedForwardNetworkTooSmallException() { }

        public FeedForwardNetworkTooSmallException(string message) : base(message) { }

        public FeedForwardNetworkTooSmallException(string message, Exception inner) : base(message) { }
    }

    [Serializable]
    public class DerivativeNotPossibleException : Exception
    {
        public DerivativeNotPossibleException() { }

        public DerivativeNotPossibleException(string message) : base(message) { }

        public DerivativeNotPossibleException(string message, Exception inner) : base(message) { }
    }
    
    [Serializable]
    public class InputLayerNotMatchedException : Exception
    {
        public InputLayerNotMatchedException() { }

        public InputLayerNotMatchedException(string message) : base(message) { }

        public InputLayerNotMatchedException(string message, Exception inner) : base(message) { }
    }

    [Serializable]
    public class ErrorCalculationNotPossibleException : Exception
    {
        public ErrorCalculationNotPossibleException() { }

        public ErrorCalculationNotPossibleException(string message) : base(message) { }

        public ErrorCalculationNotPossibleException(string message, Exception inner) : base(message) { }
    }
}
