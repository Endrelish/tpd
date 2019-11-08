using System.Collections.Generic;
using System.IO;

namespace Model
{
    public static class CsvReader
    {
        public static IEnumerable<IEnumerable<string>> ReadCsv(string filePath)
        {
            using (var reader = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                while (!reader.EndOfStream)
                    yield return reader.ReadLine()?.Split(';');
            }
        }
    }
}