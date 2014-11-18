using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareGraphs
{
    public class Vertex
    {
        private int x;
        private int y;
        private int number;

        public Vertex(int number)
        {
            this.number = number;
            x = 0;
            y = 0;
        }

        public Vertex(int x, int y, int number)
        {
            this.x = x;
            this.y = y;
            this.number = number;
        }

        public int X
        {
            get{return x;}
            set { x = value; }
        }

        public int Y
        {
            get{return y;}
            set { y = value; }
        }

        public int Number
        {
            get { return number; }
        }
    }
}
