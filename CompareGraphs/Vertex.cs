namespace CompareGraphs
{
    public class Vertex
    {
        public Vertex(int number)
        {
            _number = number;
            X = 0;
            Y = 0;
        }
        public Vertex( int number, int x, int y)
        {
            _number = number;
            X = x;
            Y = y;
        }

        private readonly int _number;

        public int X { get; set; }

        public int Y { get; set; }

        public int Number
        {
            get { return _number; }
        }
    }
}
