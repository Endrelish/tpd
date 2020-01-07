using System.Collections.Generic;
using System.IO;

namespace Model.CsvReader
{
    public static class CsvReader
    {
        public static IEnumerable<string> ReadCsv(string filename)
        {
            using (var r = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                while(!r.EndOfStream)
                    yield return r.ReadLine();
            }
        }
    }
}