using System;
using System.Collections.Generic;

namespace Currency.Services
{
    public class Graph
    {
        public Graph() { }
        public Graph(IEnumerable<string> vertices, IEnumerable<Tuple<string, string, double>> edges)
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<string, HashSet<string>> AdjacencyList { get; } = new Dictionary<string, HashSet<string>>();

        public void AddVertex(string vertex)
        {
            AdjacencyList[vertex] = new HashSet<string>();
        }

        public void AddEdge(Tuple<string, string, double> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}