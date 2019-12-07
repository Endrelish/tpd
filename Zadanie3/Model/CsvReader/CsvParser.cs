using System;
using System.Linq;
using Model.Model;

namespace Model.CsvReader
{
    public static class CsvParser
    {
        public static Graph ParseCsv(string filename)
        {
            var graph = new Graph();
            foreach (var line in CsvReader.ReadCsv(filename))
            {
                var fields = line.Split(';');
                graph.Add(new VertexDescription
                    {
                        Name = fields[0],
                        Duration = int.Parse(fields[1]),
                        PredecessorsNames = fields[2].Split(',').Where(s => !s.Equals(string.Empty, StringComparison.Ordinal))
                    }
                );
            }

            return graph;
        }
    }
}