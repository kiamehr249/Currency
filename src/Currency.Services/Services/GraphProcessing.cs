using System;
using System.Collections.Generic;

namespace Currency.Services
{
    public class GraphProcessing : IGraphProcessing
    {
        public HashSet<string> VisitNodes(Graph graph, string start)
        {
            var visited = new HashSet<string>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
            }

            return visited;
        }

        public Func<string, IEnumerable<string>> ShortestPathFunc(Graph graph, string start)
        {
            if (!graph.AdjacencyList.ContainsKey(start))
            {
                return v => {
                    var path = new List<string> { };
                    return path;
                };
            }

            var previous = new Dictionary<string, string>();

            var queue = new Queue<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            return v => {
                var path = new List<string> { };
                
                var current = v;

                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };
        }

    }
}