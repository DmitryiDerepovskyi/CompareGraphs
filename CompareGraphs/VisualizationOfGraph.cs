using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace CompareGraphs
{
    public static class VisualizationOfGraph
    {
        private static int _radiusPoint = 10;
        private static List<Ellipse> _listPoint;
        private static Vertex[] _vertices;
        private static List<Edge> _ribs;
   
        // нахождение максимального к-ва ребер
        public static void PrintGraph(ref Canvas canvasPaintGraph, int[,] adjMatrix)
        {
            canvasPaintGraph.Children.Clear();
            var numberVertexes = (int)Math.Sqrt(adjMatrix.Length);
            _vertices = new Vertex[numberVertexes];
            // Привязка вершин к координатам
            Coordinates(adjMatrix, (int)canvasPaintGraph.ActualWidth, (int)canvasPaintGraph.ActualHeight);
            Ribs(adjMatrix);
            _listPoint = new List<Ellipse>(_vertices.Length);
            for (var i = 0; i < _ribs.Count; i++)
            {
                var edge = new Line
                {
                    Fill = Brushes.Black,
                    Stroke = Brushes.Coral,
                    X1 = _ribs[i].StartVertex.X,
                    X2 = _ribs[i].EndVertex.X,
                    Y1 = _ribs[i].StartVertex.Y,
                    Y2 = _ribs[i].EndVertex.Y,
                    StrokeThickness = 2
                };
                canvasPaintGraph.Children.Add(edge);
            }
            for (var i = 0; i < numberVertexes; i++)
            {
                var point = new Ellipse {Stroke = Brushes.Brown};
                Canvas.SetLeft(point, _vertices[i].X - _radiusPoint);
                Canvas.SetTop(point, _vertices[i].Y - _radiusPoint);
                point.Width = 2 * _radiusPoint;
                point.Height = 2 * _radiusPoint;
                point.Fill = Brushes.LightCoral;
                point.StrokeThickness = 2;
                _listPoint.Add(point);
                canvasPaintGraph.Children.Add(point);

                var textBlock = new TextBlock {Text = _vertices[i].Number.ToString(), FontSize = _radiusPoint};
                Canvas.SetLeft(textBlock, _vertices[i].X - _radiusPoint / 4);
                Canvas.SetTop(textBlock, _vertices[i].Y - _radiusPoint / 1.5);
                canvasPaintGraph.Children.Add(textBlock);
            }
        }

        private static void Ribs(int[,] adjMatrix)
        {
            _ribs = new List<Edge>();
            var numberVertexes = (int)Math.Sqrt(adjMatrix.Length);
            for(int i = 0, countRib = 0; i < numberVertexes; i++)
                for(var j = 0; j < numberVertexes; j++)
                    if(adjMatrix[i,j] != 0)
                    {
                        _ribs.Add(new Edge(_vertices[i], _vertices[j], adjMatrix[i, j]));
                        countRib++;
                    }
        }
        /// <summary>
        /// Задание координат для вершин графа
        /// </summary>
        /// <param name="adjMatrix"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        private static void Coordinates(int[,] adjMatrix, int maxX, int maxY)
        {
            _radiusPoint = (int)(Math.Min(maxX, maxY)*0.05);
            var radius = Math.Min(maxX, maxY) / 2 - _radiusPoint;
            var numberVertexes = (int)Math.Sqrt(adjMatrix.Length);
            _vertices = new Vertex[numberVertexes];
            int x;
            int y;
            int step = (int) 360 / numberVertexes;
            for (int i = 0, alfa = 180; i < numberVertexes; alfa += step, i++)
            {
                x = Convert.ToInt32(radius * Math.Cos(alfa * Math.PI / 180)) + maxX/2;
                y = Convert.ToInt32(radius * Math.Sin(alfa * Math.PI / 180)) + maxY/2;
                var ver = new Vertex(i+1, x, y);
                _vertices[i] = ver;
            }
        }
    }
}
