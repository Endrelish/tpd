using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model
{
    public class GameSolver
    {
        public GameSolution Solve(PayoffMatrix matrix)
        {
            matrix.Reduce();
            var xStrategies = matrix.Rows.Select(r => (r.ToList().IndexOf(r.Min()), r.Min())).ToList();
            var yStrategies = matrix.Columns.Select(c => (c.ToList().IndexOf(c.Max()), c.Max())).ToList();
        }

        private bool SaddlePoint(List<(int, double)> xStrategies, List<(int, double)> yStrategies)
        {
            var maxX = xStrategies.First(x => x.Item2.Equals(xStrategies.Select(s => s.Item2).Max()));
            var xIndex = xStrategies.IndexOf(maxX);
            var minY = yStrategies.First(y => y.Item2.Equals(yStrategies.Select(s => s.Item2).Max()));
            var yIndex = minY.Item1;

            return xIndex == yIndex;
        }
    }
}