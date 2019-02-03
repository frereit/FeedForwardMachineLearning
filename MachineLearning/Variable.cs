using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Variable
    {
        public ArrayList OutputNodes { get; set; }
        public object Value { get; set; }
        public object Output { get; set; }

        public Variable(object pInitialValue)
        {
            Value = pInitialValue;
            OutputNodes = new ArrayList();

            Globals.DefaultGraph.Variables.Add(this);
        }
    }
}
