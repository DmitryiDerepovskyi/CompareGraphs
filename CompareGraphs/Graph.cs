using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareGraphs
{
    internal class Graph
    {
        
        private int[] g;
        private int[] p;
        private int[,] adjacencyMatrix;
        private int diametr;
        private int vitality;
    
        public Graph(int[] G, int[] P)
        {
            if (isCorrectData(G, P))
            {
                g = G;
                p = P;
                adjacencyMatrix = ConvertMFItoAdjacencyMatrix(g,p);
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
        /// <param name="G"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public bool isCorrectData(int[] G, int[] P)
        {
            bool isCorrect = true;
            if (P.Length >= 20)
                isCorrect = false;
            if (P[P.Length - 1] >= 50)
                isCorrect = false;
            foreach (int i in G)
                if (i > P.Length)
                    isCorrect = false;
            for (int i = 0, p = 0; i < P.Length; i++)
            {
                if (P[i] < p || P[i] > G.Length)
                    isCorrect = false;
                else
                    p = P[i];
            }
            return isCorrect;
        }

#region methodsGet
        /// <summary>
        /// Возвращает живучесть графа
        /// </summary>
        /// <returns></returns>
    
        public int GetVitality()
        {
            return vitality;
        }
        /// <summary>
        /// Возвращает массив G (MFI-представления)
        /// </summary>
        /// <returns></returns>
        public int[] GetG()
        {
            return g;
        }
        /// <summary>
        /// Возвращает массив Р (MFI - представления)
        /// </summary>
        /// <returns></returns>
        public int[] GetP()
        {
            return p;
        }

        /// <summary>
        /// Возвращает матрицу смежности
        /// </summary>
        /// <returns></returns>
        public int[,] GetAdjacencyMatrix()
        {
            return adjacencyMatrix;
        }
#endregion

       
        /// <summary>
        /// Нахождения кратчайших путей между всеми парами вершин
        /// </summary>
        /// <returns></returns>
        private int[,] AlgoFloydWarshall()
        {
            int n = (int)Math.Sqrt(adjacencyMatrix.Length);
            int[,] W = new int[n, n];
            for(int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (adjacencyMatrix[i, j] == 0)
                        W[i, j] = Int32.MaxValue;
                    else
                        W[i, j] = adjacencyMatrix[i, j];
                }
            }
            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (W[i,k] < Int32.MaxValue && W[k,j] < Int32.MaxValue)
                            if (W[i,k] + W[k,j] < W[i,j])
                                W[i,j] = W[i,k] + W[k,j];

            return W;
        }

        /// <summary>
        /// Находим все возможные диаметры графа
        /// </summary>
        /// <param name="W">Матрица с кратчайшими путями между всеми парами вершин/param>
        /// <returns></returns>
        private List<Edge> FindDiameters(int[,] W)
        {
            int n = (int)Math.Sqrt(W.Length);
            // список хранящий длинейшие пути среди кратчайших
            var diameters = new List<Edge>();
            diameters.Add(new Edge(0, 1, W[0, 1]));
            // находим все максимальные кратчайшие пути
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (diameters.Last().Weight > W[i, j])
                        continue;
                    else if (diameters.Last().Weight == W[i, j])
                        diameters.Add(new Edge(i, j, W[i, j]));
                    else
                    {
                        diameters.Clear();
                        diameters.Add(new Edge(i, j, W[i, j]));
                    }
                }
            }
            return diameters;
        }
        /// <summary>
        /// Нахождения живучести для определенного диаметра
        /// </summary>
        /// <param name="diameter"></param>
        /// <returns></returns>
        private int FindVitalityForThisDiameter(Edge diameter)
        {
            int visabilityFTD = 0;
            int n = (int)Math.Sqrt(adjacencyMatrix.Length);
            var currentPath = new List<int>();
            var visitedVertexs = new List<int>();
            var allPath = new List<List<int>>();
            //добавляем стартовую вершину в текущий путь и посещенные вершины 
            currentPath.Add(diameter.StartVertex.Number);
            visitedVertexs.Add(diameter.StartVertex.Number);
            while(true)
            {
                int i = currentPath.Last();
                for (int j = 0; j < n; j++)
                {
                    if (adjacencyMatrix[i, j] == 0)
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
                                j = 0;
                            }
                        }
                       
                    }
                }
                currentPath.RemoveAt(currentPath.Count - 1);
                if(currentPath.Count == 0)
                    break;
                RemoveVisitedVertexs(visitedVertexs);
            }
            allPath.Sort(delegate(List<int> l1, List<int> l2)
            { return l1.Count.CompareTo(l2.Count); });
            if (allPath.Count == 1)
                visabilityFTD = 0;
            int dist = allPath[0].Count - 1;
            for (int i = 0; i < allPath.Count; i++)
                for (int j = 0; j < allPath[i].Count; j++)
                { 
                }
            return visabilityFTD;
        }

        /// <summary>
        /// Удаляем посещенные вершины
        /// </summary>
        /// <param name="visitedVertexs"></param>
        private void RemoveVisitedVertexs(List<int> visitedVertexs)
        {
            for (int i = visitedVertexs.Count - 1; ; i--)
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
        /// <summary>
        /// Нахождение живучести
        /// </summary>
        /// <returns></returns>
        private int FindVitality()
        {
            // находим кратчайшие пути между всеми вершинами
            int[,] W = AlgoFloydWarshall();
            List<Edge> diameters = FindDiameters(W);
            int[] vitalities = new int[diameters.Count];
            for (int i = 0; i < diameters.Count; i++)
            {
                vitalities[i] = FindVitalityForThisDiameter(diameters[i]);
            }
            return 0;
        }
        /// <summary>
        /// Преобразовывает MFI-представления в матрицу смежности
        /// </summary>
        /// <param name="G"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        private int[,] ConvertMFItoAdjacencyMatrix(int[] G,int[] P)
        {
            int minNumberVertex = G.Min();
            int maxNumberVertex = G.Max();
            int [,] AdjMatrix = new int[P.Length,P.Length];
            for (int countP = 0, countG = 0; countP < P.Length; countP++)
            {
                for (; countG < P[countP]; countG++)
                {
                    //граф неориентированный - матрица симметрична
                    AdjMatrix[countP, G[countG]-1] = AdjMatrix[G[countG]-1,countP] = 1;
                }
            }
            return AdjMatrix;
        }
    }
}
