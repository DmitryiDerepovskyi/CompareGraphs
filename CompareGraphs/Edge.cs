using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareGraphs
{
    public class Edge
    {
        private Vertex VertexStart;
        private Vertex VertexEnd;
        private int weight;

        public Edge(Vertex VertexStart, Vertex VertexEnd, int weight)
        {
            this.VertexStart = VertexStart;
            this.VertexEnd = VertexEnd;
            this.weight = weight;
        }

        public Edge(int  Start, int  End, int weight)
        {
            VertexStart = new Vertex(Start);
            VertexEnd = new Vertex(End);
            this.weight = weight;
        }
        public Vertex StartVertex
        {
            get { return VertexStart; }
        }
        public Vertex EndVertex
        {
            get { return VertexEnd; }
        }
        public int Weight
        {
            get { return weight;}
        }
    }
}
