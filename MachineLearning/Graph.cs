using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Graph
    {
        public ArrayList Operations { get; set; }
        public ArrayList Variables { get; set; }
        public ArrayList Placeholders { get; set; }

        public Graph()
        {
            Operations = new ArrayList();
            Placeholders = new ArrayList();
            Variables = new ArrayList();
        }

        public void SetAsDefault()
        {
            Globals.DefaultGraph = this;
        }
    }
}
