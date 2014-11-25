namespace CompareGraphs
{
    public class Edge
    {
      
        public Edge(Vertex vertexStart, Vertex vertexEnd, int weight)
        {
            _vertexStart = vertexStart;
            _vertexEnd = vertexEnd;
            Weight = weight;
        }

        public Edge(int  start, int  end, int weight)
        {
            _vertexStart = new Vertex(start);
            _vertexEnd = new Vertex(end);
            Weight = weight;
        }
        private readonly Vertex _vertexStart;
        private readonly Vertex _vertexEnd;

        public Vertex StartVertex
        {
            get { return _vertexStart; }
        }
        public Vertex EndVertex
        {
            get { return _vertexEnd; }
        }

        public int Weight { get; set; }
    }
}
