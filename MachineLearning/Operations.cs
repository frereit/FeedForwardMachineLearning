using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Operation
    {
        public ArrayList InputNodes { get;  }
        public ArrayList OutputNodes { get; set;  }
        public ArrayList Inputs { get; set; }
        public object Output { get; set; }

        public Operation(ArrayList pInputNodes)
        {
            InputNodes = pInputNodes;
            OutputNodes = new ArrayList();

            foreach(object node in InputNodes)
            {
                if(node is Variable)
                {
                    ((Variable) node).OutputNodes.Add(this);
                }else if(node is Placeholder)
                {
                    ((Placeholder)node).OutputNodes.Add(this);
                }else if(node is Operation)
                {
                    ((Operation)node).OutputNodes.Add(this);
                }
                
            }

            Globals.DefaultGraph.Operations.Add(this);
        }
        
        public object Compute(params Object[] objects) { return null; }
    }

    public class Add : Operation
    {

        public Add(object x, object y) : base(new ArrayList() { x, y, })
        {

        }

        public new double Compute(params Object[] objects)
        {
            Inputs = new ArrayList() { objects[0], objects[1] };
            return (double) objects[0] + (double) objects[1];
        }
    }

    public class Multiply : Operation
    {
        public Multiply(object x, object y) : base(new ArrayList() { x, y, })
        {

        }

        public new double Compute(params Object[] objects)
        {
            Inputs = new ArrayList() { objects[0], objects[1] };
            return (double) objects[0] * (double) objects[1];
        }
    }


    public class MatMul : Operation
    {
        public MatMul(Matrix x, Matrix y) : base(new ArrayList() { x, y, })
        {

        }

        public new Matrix Compute(params Object[] objects)
        {
            Inputs = new ArrayList() { objects[0], objects[1] };
            return (Matrix) objects[0] * (Matrix) objects[1];
        }
    }
}
