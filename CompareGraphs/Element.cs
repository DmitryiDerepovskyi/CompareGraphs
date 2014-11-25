using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareGraphs
{
    class Element
    {
        public Element() { ;}
        public Element(int value)
        {
            this.Value = value;
        }
        public int Value { get; set; }
    }
}
