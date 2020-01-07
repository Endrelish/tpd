using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class Strategy
    {
        public Dictionary<string, Path> Paths { get; set; }

        public IEnumerable<string> FormattedPaths => Paths.OrderByDescending(p => p.Key).Select(p => p.Value.ToString());
        public IEnumerable<Vertex> StartVertices { get; set; }

        public string OptimalSolution => StartVertices.Select(s => Paths[s.Label])
            .Aggregate((prev, curr) => prev.TotalCost < curr.TotalCost ? prev : curr).ToString();

        private static string GetSeparator(Path path)
        {
            return path.Vertices.Any() ? " => " : " ";
        }
    }
}