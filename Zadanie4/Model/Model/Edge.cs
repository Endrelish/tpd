namespace Model.Model
{
    public class Edge
    {
        public string Label { get; set; }
        public int Cost { get; set; }
        public Vertex StartVertex { get; set; }
        public Vertex EndVertex { get; set; }
    }
}