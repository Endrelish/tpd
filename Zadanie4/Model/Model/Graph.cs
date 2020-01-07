using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

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

        public IEnumerable<Vertex> EndVertices => Vertices.Values.Where(v => !v.Successors.Any());
        public IEnumerable<Vertex> StartVertices => Vertices.Values.Where(v => !v.Predecessors.Any());

        private Vertex GetOrCreateVertex(string vertexLabel)
        {
            if (!_vertices.ContainsKey(vertexLabel))
                _vertices[vertexLabel] = new Vertex{Label = vertexLabel};

            return _vertices[vertexLabel];
        }

        public void Add(EdgeDescription edgeDescription)
        {
            var startVertex = GetOrCreateVertex(edgeDescription.StartVertexLabel);
            var endVertex = GetOrCreateVertex(edgeDescription.EndVertexLabel);
            var edge = new Edge
            {
                Cost = edgeDescription.Cost,
                Label = edgeDescription.Label,
                StartVertex = startVertex,
                EndVertex = endVertex
            };
            startVertex.Successors.Add(edge);
            endVertex.Predecessors.Add(edge);
        }
    }
}