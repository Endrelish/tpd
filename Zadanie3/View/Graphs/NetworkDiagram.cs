using System.Linq;
using Model.Model;

namespace View.Graphs
{
    public static class NetworkDiagram
    {
        public static void Draw(Graph graph)
        {
            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph g = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            var edges = graph.Vertices.Values.SelectMany(v => v.Successors.Select(s => (v.Name, s.Name)));
            foreach (var edge in edges)
            {
                g.AddEdge(edge.Item1, edge.Item2);
            }
            //bind the graph to the viewer 
            viewer.Graph = g;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.Width = 750;
            form.Height = 750;
            //show the form 
            form.ShowDialog();
        }
    }
}