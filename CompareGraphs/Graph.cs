using System;
using System.Collections.Generic;
using System.Linq;

namespace CompareGraphs
{
    internal class Graph
    {
        private readonly int[] _g;
        private readonly int[] _p;
        private readonly int[,] _adjacencyMatrix;
        private int _vitality;
        private readonly int _numberOfVertex;
        public Graph(int[] g, int[] p)
        {
            _g = g;
            _p = p;
            _adjacencyMatrix = ConvertMFItoAdjacencyMatrix(_g, _p);
            _numberOfVertex = p.Length;
            if (IsCorrectData(g, p))
            {
                FindVitality();
            }
            else
            {
                throw new IncorrectDataException();
            }
        }
        /// <summary>
        /// Проверка на корректность данных
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsCorrectData(int[] g, int[] p)
        {
            bool isCorrect;
            if (p.Length >= 20)
                isCorrect = false;
            if (p[p.Length - 1] >= 50)
                isCorrect = false;
            foreach (var i in g)
                if (i > p.Length)
                    isCorrect = false;
            for (int i = 0, n = 0; i < p.Length; i++)
            {
                if (p[i] < n || p[i] > g.Length)
                    isCorrect = false;
                else
                    n = p[i];
            }
            isCorrect = IsGraphConnected();
            return isCorrect;
        }
        /// <summary>
        /// Метод выполняет проверку графа на связность
        /// </summary>
        /// <returns></returns>
        private bool IsGraphConnected()
        {
            var markedVertexes = new bool[_numberOfVertex];
            markedVertexes[0] = true;
            CheckVertexForConnection(0, ref markedVertexes);
            return !markedVertexes.Contains(false);
        }
        /// <summary>
        /// Метод проходит по всем вершинам, связанным с посланной вершиной vertex и помечает их.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="markedVertexes"></param>
        private void CheckVertexForConnection(int vertex, ref bool[] markedVertexes)
        {
            if (!markedVertexes.Contains(false)) return;
            for (var i = 0; i < _numberOfVertex; i++)
            {
                if (_adjacencyMatrix[vertex, i] == 0) continue;
                if (markedVertexes[i]) continue;
                markedVertexes[i] = true;
                CheckVertexForConnection(i, ref markedVertexes);
            }
        }
#region methodsGet
        /// <summary>
        /// Возвращает живучесть графа
        /// </summary>
        /// <returns></returns>
        public int GetVitality()
        {
            return _vitality;
        }
        /// <summary>
        /// Возвращает массив G (MFI-представления)
        /// </summary>
        /// <returns></returns>
        public int[] GetG()
        {
            return _g;
        }
        /// <summary>
        /// Возвращает массив Р (MFI - представления)
        /// </summary>
        /// <returns></returns>
        public int[] GetP()
        {
            return _p;
        }

        /// <summary>
        /// Возвращает матрицу смежности
        /// </summary>
        /// <returns></returns>
        public int[,] GetAdjacencyMatrix()
        {
            return _adjacencyMatrix;
        }
#endregion
        private List<List<int>> _allPathBetweenToVertexs;
        /// <summary>
        /// Нахождение живучести
        /// </summary>
        /// <returns></returns>
        private void FindVitality()
        {
            // находим кратчайшие пути между всеми вершинами
            var w = AlgoFloydWarshall();
            var diameters = FindDiameters(w);
            var vitalities = new int[diameters.Count];
            for (var i = 0; i < diameters.Count; i++)
            {
                _allPathBetweenToVertexs = FindAllPath(diameters[i]);
                var buffer = FindVitalityForThisDiameter();
                vitalities[i] = buffer;
            }
            _vitality = vitalities.Max();
            _vitality = _vitality == 0 ? _p.Count() - 1 : _vitality;
        }
        /// <summary>
        /// Нахождения кратчайших путей между всеми парами вершин
        /// </summary>
        /// <returns></returns>
        private int[,] AlgoFloydWarshall()
        {
            var w = new int[_numberOfVertex, _numberOfVertex];
            for(var i = 0; i < _numberOfVertex; i++)
            {
                for (var j = 0; j < _numberOfVertex; j++)
                {
                    if (_adjacencyMatrix[i, j] == 0)
                        w[i, j] = Int32.MaxValue;
                    else
                        w[i, j] = _adjacencyMatrix[i, j];
                }
            }
            for (var k = 0; k < _numberOfVertex; k++)
                for (var i = 0; i < _numberOfVertex; i++)
                    for (var j = 0; j < _numberOfVertex; j++)
                        if (w[i,k] < Int32.MaxValue && w[k,j] < Int32.MaxValue)
                            if (w[i,k] + w[k,j] < w[i,j])
                                w[i,j] = w[i,k] + w[k,j];

            return w;
        }
        /// <summary>
        /// Находим все возможные диаметры графа
        /// </summary>
        /// <param name="w"></param>
        /// Матрица с кратчайшими путями между всеми парами вершин/param>
        /// <returns></returns>
        private List<Edge> FindDiameters(int[,] w)
        {
            // список хранящий длинейшие пути среди кратчайших
            var diameters = new List<Edge>();
            diameters.Add(new Edge(0, 1, w[0, 1]));
            // находим все максимальные кратчайшие пути
            for (var i = 0; i < _numberOfVertex; i++)
            {
                for (var j = i + 1; j < _numberOfVertex; j++)
                {
                    if (diameters.Last().Weight > w[i, j])
                        continue;
                    else if (diameters.Last().Weight == w[i, j])
                        diameters.Add(new Edge(i, j, w[i, j]));
                    else
                    {
                        diameters.Clear();
                        diameters.Add(new Edge(i, j, w[i, j]));
                    }
                }
            }
            return diameters;
        }
        /// <summary>
        /// Нахожденит все пути, которые параллельны живучести для определенного диаметра
        /// </summary>
        /// <param name="diameter"></param>
        /// <returns></returns>
        private List<List<int>> FindAllPath(Edge diameter)
        {
            var currentPath = new List<int>();
            var visitedVertexs = new List<int>();
            var allPath = new List<List<int>>();
            //добавляем стартовую вершину в текущий путь и посещенные вершины 
            currentPath.Add(diameter.StartVertex.Number);
            visitedVertexs.Add(diameter.StartVertex.Number);
            while(true)
            {
                var i = currentPath.Last();
                for (var j = 0; j < _numberOfVertex; j++)
                {
                    if (_adjacencyMatrix[i, j] == 0)
                        continue;
                    else
                    {
                        if (visitedVertexs.IndexOf(j) == -1 && currentPath.IndexOf(j) == -1)
                        {
                            currentPath.Add(j);
                            visitedVertexs.Add(j);
                            if (j == diameter.EndVertex.Number)
                            {
                                var list = new List<int>(currentPath);
                                allPath.Add(list);
                                currentPath.RemoveAt(currentPath.Count - 1);
                            }
                            else
                            {
                                visitedVertexs.Add(-1);
                                i = j;
                                j = -1;
                            }
                        }
                       
                    }
                }
                currentPath.RemoveAt(currentPath.Count - 1);
                if(currentPath.Count == 0)
                    break;
                RemoveVisitedVertexes(visitedVertexs);
            }
      
            return allPath;
        }
        /// <summary>
        /// Удаляем посещенные вершины
        /// </summary>
        /// <param name="visitedVertexs"></param>
        private void RemoveVisitedVertexes(IList<int> visitedVertexs)
        {
            for (var i = visitedVertexs.Count - 1; ; i--)
            {
                if (visitedVertexs[i] == -1)
                {
                    visitedVertexs.RemoveAt(i);
                    break;
                }
                else
                    visitedVertexs.RemoveAt(i);
            }
        }
      
        // Словарь соответствия вершина - в каких диаметрах содержится 
        Dictionary<int, List<int>> diameterVertex = new Dictionary<int, List<int>>();
        // Словарь соответствия вершина - в каких путях содержится 
        Dictionary<int, List<int>> otherVertex = new Dictionary<int, List<int>>();
        private int FindVitalityForThisDiameter()
        {
            diameterVertex.Clear();
            otherVertex.Clear();
            var vitality = 0;
            _allPathBetweenToVertexs.Sort((l1, l2) => l1.Count.CompareTo(l2.Count));

            var diameter = _allPathBetweenToVertexs[0].Count;
            var pathsLongerDiameter = 0;
            for(var i = 0; i < _allPathBetweenToVertexs.Count; i++)
            {
                if(_allPathBetweenToVertexs[i].Count > diameter)
                {
                    pathsLongerDiameter = i;
                    break;
                }
            }
            if(pathsLongerDiameter == 0)
                return vitality;
            // перебираем пути равные диаметру
            for (var i = 0; i < pathsLongerDiameter; i++)
            {
                // перебираем все вершины диаметра
                for(var v = 1; v < _allPathBetweenToVertexs[i].Count - 1; v++)
                {
                    // ищем эту вершину в других путях, больше чем диаметр
                    for (var j = pathsLongerDiameter; j < _allPathBetweenToVertexs.Count; j++)
                    {
                        var vertex = _allPathBetweenToVertexs[i][v];
                        if (!diameterVertex.ContainsKey(vertex))
                            diameterVertex.Add(vertex, new List<int>());
                        diameterVertex[vertex].Add(i);
                        if (_allPathBetweenToVertexs[j].IndexOf(vertex) != -1)
                        {
                            if (!otherVertex.ContainsKey(vertex))
                                otherVertex.Add(vertex, new List<int>());
                            otherVertex[vertex].Add(j);
                        }
                    }
                }
            }
            if (diameterVertex.Count == 0)
                return vitality;
            return SetCoverProblem(pathsLongerDiameter);
        }
        /// <summary>
        /// Задача о покрытии
        /// </summary>
        /// <param name="pathsLongerDiameter"></param>
        /// <returns></returns>
        private int SetCoverProblem(int pathsLongerDiameter)
        {
            var multiplicity = new List<int>();
            var keys = new List<int>(diameterVertex.Keys);
            var numberVertex = 0;
            for (var size = 1; size <= pathsLongerDiameter; size++)
            {
                numberVertex = RemoveVerticesToIncreaseDiameter(multiplicity, keys, size, pathsLongerDiameter);
                if (numberVertex != 0)
                    break;
            }
            return numberVertex;
        }
        /// <summary>
        /// Определяем какие вершины нужно удалить для увеличения диаметра
        /// </summary>
        /// <param name="multiplicity"></param>
        /// <param name="keys"></param>
        /// <param name="size"></param>
        /// <param name="pathsLongerDiameter"></param>
        /// <returns></returns>
        private int RemoveVerticesToIncreaseDiameter(IList<int> multiplicity, IList<int> keys, int size, int pathsLongerDiameter)
        {
            if (multiplicity.Count == size)
                return 0;
            for (var i = 0; i < keys.Count; i++){
                if (multiplicity.IndexOf(keys[i]) == -1)
                    multiplicity.Add(keys[i]);
                else
                    continue;
                if (multiplicity.Count == size){
                    if (Checked(multiplicity, pathsLongerDiameter))
                        return size;
                    else
                        multiplicity.RemoveAt(multiplicity.Count - 1);
                }
                else
                {
                    var result = RemoveVerticesToIncreaseDiameter(multiplicity, keys, size, pathsLongerDiameter);
                    if (result == 0)
                        multiplicity.Remove(multiplicity.Count - 1);
                    else
                        return result;
                }
            }
            return 0;
        }
        /// <summary>
        /// Проверяем, возможно ли удалить вершину
        /// </summary>
        /// <param name="multiplicity"></param>
        /// <param name="pathsLongerDiameter"></param>
        /// <returns></returns>
        private bool Checked(IEnumerable<int> multiplicity, int pathsLongerDiameter)
        {
            var isSet = false;
            var generalDiameter = new List<int>();
            var generalOther = new List<int>();
            foreach (var i in multiplicity)
            {
                generalDiameter.AddRange(diameterVertex[i]);
                if (otherVertex.ContainsKey(i))
                    generalOther.AddRange(otherVertex[i]);
            }
            generalDiameter = generalDiameter.Distinct().ToList();
            generalOther = generalOther.Distinct().ToList();
            if (generalDiameter.Count == pathsLongerDiameter)
                if(generalOther.Count < _allPathBetweenToVertexs.Count - pathsLongerDiameter)
                    isSet = true;
            return isSet;
        }

        /// <summary>
        /// Преобразовывает MFI-представления в матрицу смежности.
        /// </summary>
        /// <returns>Возвращает матрицу смежности</returns>
        private int[,] ConvertMFItoAdjacencyMatrix(IList<int> g,IList<int> p)
        {
            var adjMatrix = new int[p.Count,p.Count];
            for (int countP = 0, countG = 0; countP < p.Count; countP++)
            {
                for (; countG < p[countP]; countG++)
                {
                    //граф неориентированный - матрица симметрична
                    adjMatrix[countP, g[countG]-1] = adjMatrix[g[countG]-1,countP] = 1;
                }
            }
            return adjMatrix;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;
            var p = obj as Graph;
            if(p == null)
                return false;
            return _vitality == p.GetVitality();
        }

        public override int GetHashCode()
        {
            return _numberOfVertex;
        }
        public static bool operator== (Graph obj1, Graph obj2)
        {
            return obj1.GetVitality() == obj2.GetVitality();
        }
        public static bool operator!= (Graph obj1, Graph obj2)
        {
            return obj1.GetVitality() != obj2.GetVitality();
        }
       
    }
}
