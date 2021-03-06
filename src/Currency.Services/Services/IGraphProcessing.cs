using System;
using System.Collections.Generic;

namespace Currency.Services
{
    public interface IGraphProcessing
    {
        HashSet<string> VisitNodes(Graph graph, string start);
        Func<string, IEnumerable<string>> ShortestPathFunc(Graph graph, string start);
    }
}
