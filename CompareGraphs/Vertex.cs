using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareGraphs
{
    public class Vertex
    {
        public Vertex(int number)
        {
            this._number = number;
            X = 0;
            Y = 0;
        }
        public Vertex( int number, int x, int y)
        {
            this._number = number;
            this.X = x;
            this.Y = y;
        }

        private int _number;

        public int X { get; set; }

        public int Y { get; set; }

        public int Number
        {
            get { return _number; }
        }
    }
}
