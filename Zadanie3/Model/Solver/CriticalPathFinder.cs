using System;
using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model.Solver
{
    public class CriticalPathFinder
    {
        public Path FindCriticalPath(Graph graph)
        {
            var path = FindAllPaths(graph).Aggregate(new Path(), (p1, p2) => p1.Duration > p2.Duration ? p1 : p2);
            path.Vertices.Reverse();
            return path;
        }

        private IEnumerable<Path> FindAllPaths(Graph graph)
        {
            return graph.StartVertices.Select(GetSuccesors).SelectMany(s => s);
        }

        private IEnumerable<Path> GetSuccesors(Vertex vertex)
        {
            if (!vertex.Successors.Any())
                return new[] {new Path {Vertices = new List<Vertex> {vertex}}};

            var vertices = vertex.Successors.Select(GetSuccesors).SelectMany(s => s).ToList();
            foreach (var v in vertices)
                v.Vertices.Add(vertex);

            return vertices;
        }
    }
}