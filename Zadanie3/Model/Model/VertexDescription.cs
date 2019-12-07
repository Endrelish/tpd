using System.Collections.Generic;

namespace Model.Model
{
    public class VertexDescription
    {
        public string Name { get; set; }
        public IEnumerable<string> PredecessorsNames { get; set; }
        public int Duration { get; set; }
    }
}