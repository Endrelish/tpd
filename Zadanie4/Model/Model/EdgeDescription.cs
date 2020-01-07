using System.Collections.Generic;

namespace Model.Model
{
    public class EdgeDescription
    {
        public string StartVertexLabel { get; set; }
        public string EndVertexLabel { get; set; }
        public string Label { get; set; }
        public int Cost { get; set; }
    }
}