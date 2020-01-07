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
                graph.Add(new EdgeDescription
                    {
                        StartVertexLabel = fields[0],
                        EndVertexLabel = fields[1],
                        Label = fields[2],
                        Cost = int.Parse(fields[3])
                    }
                );
            }

            return graph;
        }
    }
}