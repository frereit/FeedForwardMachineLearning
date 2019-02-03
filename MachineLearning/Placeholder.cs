using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Placeholder
    {
        public ArrayList OutputNodes { get; set; }
        public object Output { get; set; }
        public Placeholder()
        {
            OutputNodes = new ArrayList();

            Globals.DefaultGraph.Placeholders.Add(this);
        }
    }
}
