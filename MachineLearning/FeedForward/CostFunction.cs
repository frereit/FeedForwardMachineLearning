using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.MatrixMath;

namespace MachineLearning.FeedForward
{
    public class CostFunction
    {
        public static double MeanSquaredError(Matrix pActual, Matrix pExpected)
        {
            if(pActual.Rows != pExpected.Rows) throw new ErrorCalculationNotPossibleException("Mismatch of neurons in actual matrix and neurons in expected matrix.");
            double cost = 0;
            for (var i = 0; i < pActual.Rows; i++)
            {
                cost += Math.Pow(pActual[i, 0] - pExpected[i, 0], 2);
            }

            return cost/2;
        }

        public static Matrix MeanSquaredErrorDerivative(Matrix pActual, Matrix pExpected)
        {
            return pExpected - pActual;
        }

        public static double MeanSquaredError(List<Matrix> pActual, List<Matrix> pExpected)
        {
            if(pActual.Count != pExpected.Count) throw new ErrorCalculationNotPossibleException("Mismatch of actual matrix count and expected matrix count.");
            var cost = pActual.Select((t, i) => MeanSquaredError(t, pExpected[i])).Sum();
            return cost / pActual.Count;
        }
    }
}
