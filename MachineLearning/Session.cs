using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Session
    {
        public object Run(Operation operation, Dictionary<Placeholder, object> feed_dict)
        {
            ArrayList nodesPostorder = new Traverser(operation).Traverse();
            foreach(object node in nodesPostorder) 
            {
                if(node is Placeholder)
                {
                    ((Placeholder)node).Output = feed_dict[(Placeholder) node];
                }else if (node is Variable)
                {
                    ((Variable)node).Output = ((Variable)node).Value;
                }else if (node is Operation)
                {
                    foreach(object inputNode in ((Operation)node).InputNodes)
                    {
                        if(inputNode is Variable)
                            ((Operation)node).Inputs.Add(((Variable) inputNode).Output);
                        else if(inputNode is Placeholder)
                            ((Operation)node).Inputs.Add(((Placeholder)inputNode).Output);

                        ((Operation)node).Output = ((Operation)node).Compute(((Operation) node).InputNodes);
                    }
                }
            }
            return operation.Output;
        }
    }
}
