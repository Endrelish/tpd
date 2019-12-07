using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class Vertex
    {
        public Vertex()
        {
            Predecessors = new List<Vertex>();
            Successors = new List<Vertex>();
        }
        public int Duration { get; set; }
        public string Name { get; set; }
        public List<Vertex> Predecessors { get; set; }
        public List<Vertex> Successors { get; set; }

        public int StartTime => !Predecessors.Any() ? 0 : Predecessors.Select(p => p.StartTime + p.Duration).Max();
    }
}