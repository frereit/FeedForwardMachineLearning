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
    public class MatrixAdditionNotPossibleException : Exception
    {
        public MatrixAdditionNotPossibleException() { }

        public MatrixAdditionNotPossibleException(string message) : base(message) { }

        public MatrixAdditionNotPossibleException(string message, Exception inner) : base(message) { }
    }
}
