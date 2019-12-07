using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class Graph
    {
        public Graph()
        {
            _vertices = new Dictionary<string, Vertex>();
        }
        private Dictionary<string, Vertex> _vertices;

        public IReadOnlyDictionary<string, Vertex> Vertices
        {
            get => _vertices;
        }

        public IEnumerable<Vertex> StartVertices => Vertices.Values.Where(v => !v.Predecessors.Any());

        public void Add(VertexDescription vertexDescription)
        {
            var vertex = new Vertex
            {
                Name = vertexDescription.Name,
                Duration = vertexDescription.Duration,
                Predecessors = vertexDescription.PredecessorsNames.Select(n => _vertices[n]).ToList()
            };
            _vertices.Add(vertexDescription.Name, vertex);
            foreach (var p in vertex.Predecessors)
            {
                p.Successors.Add(vertex);
            }
        }
    }
}