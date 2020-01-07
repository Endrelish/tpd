using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class Path
    {
        public List<string> Vertices { get; set; }
        public int TotalCost { get; set; }

        public override string ToString()
        {
            return $"{string.Join(" => ", Vertices)} :: {TotalCost}";
        }
    }
}