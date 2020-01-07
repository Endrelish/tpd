using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class Vertex
    {
        public Vertex()
        {
            Predecessors = new List<Edge>();
            Successors = new List<Edge>();
        }
        public string Label { get; set; }
        public List<Edge> Predecessors { get; set; }
        public List<Edge> Successors { get; set; }
    }
}