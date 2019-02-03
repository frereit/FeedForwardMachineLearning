using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Traverser
    {
        private readonly Operation startOperation;

        public Traverser(Operation pOperation)
        {
            startOperation = pOperation;
        }

        public ArrayList Traverse()
        {
            return Recurse(startOperation);
        }

        public ArrayList Recurse(object node)
        {
            ArrayList nodesPostorder = new ArrayList();
            if(node is Operation)
            {
                foreach(object input_node in ((Operation)node).InputNodes)
                {
                    nodesPostorder.Add(Recurse(input_node));
                }
            }
            nodesPostorder.Add(node);
            return nodesPostorder;
        }
    }
}
