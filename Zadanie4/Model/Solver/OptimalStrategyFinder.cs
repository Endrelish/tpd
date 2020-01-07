using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model.Solver
{
    public static class OptimalStrategyFinder
    {
        public static Strategy FindOptimalStrategy(Graph graph)
        {
            var paths = new Dictionary<string, Path>();
            foreach (var e in graph.EndVertices)
            {
                GetPaths(paths, e, 0, new List<string>());
            }

            return new Strategy
            {
                Paths = paths,
                StartVertices = graph.StartVertices
            };
        }

        private static void GetPaths(Dictionary<string, Path> items, Vertex vertex, int cost, ICollection<string> currentPath)
        {
            currentPath = currentPath.Concat(new List<string>{vertex.Label}).ToList();
            var path = new Path
            {
                TotalCost = cost,
                Vertices = currentPath.Reverse().ToList()
            };
            if(!items.ContainsKey(vertex.Label) || items[vertex.Label].TotalCost > path.TotalCost)
                items[vertex.Label] = path;

            foreach (var p in vertex.Predecessors)
            {
                GetPaths(items, p.StartVertex, cost + p.Cost, currentPath.Concat(new List<string> { p.Label}).ToList());
            }
        }
    }
}