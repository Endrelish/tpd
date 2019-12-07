using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class Path
    {
        public List<Vertex> Vertices { get; set; }
        public int Duration => Vertices != null && Vertices.Any() ? Vertices.Sum(v => v.Duration) : 0;

        public override string ToString()
        {
            return string.Join(" => ", Vertices.Select(v => v.Name));
        }
    }
}