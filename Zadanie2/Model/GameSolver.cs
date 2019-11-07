using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Model.Model;

namespace Model
{
    public class GameSolver
    {
        public GameSolution Solve(PayoffMatrix matrix)
        {
            var saddle = HasSaddlePoint(matrix);
            if (saddle != null)
                return new GameSolution
                {
                    Payoff = saddle.Payoff,
                    XStrategy = new Dictionary<string, double> {[matrix.XLabels[saddle.XStrategy]] = 1.0d},
                    YStrategy = new Dictionary<string, double> {[matrix.YLabels[saddle.YStrategy]] = 1.0d}
                };
            matrix.Reduce();
            var xStrategy = SolveEquations(matrix.Columns.Select(c => (IList<double>)c.ToList()).ToList()).ToList();
            var yStrategy = SolveEquations(matrix.Rows.Select(r => (IList<double>) r.ToList()).ToList()).ToList();
            return new GameSolution
            {
                XStrategy = GetStrategyFromSolution(xStrategy, matrix.XLabels),
                YStrategy = GetStrategyFromSolution(yStrategy, matrix.YLabels),
                Payoff = xStrategy.Last()
            };
        }

        public Dictionary<string, double> GetStrategyFromSolution(IList<double> solution, IList<string> labels)
        {
            return solution
                .Select((v, i) => new KeyValuePair<int, double>(i, v))
                .Take(solution.Count - 1)
                .ToDictionary(v => labels[v.Key], v => v.Value);
        }

        public IEnumerable<double> SolveEquations(IList<IList<double>> payoffs)
        {
            var values = payoffs.Select(p => p.ToList()).ToList();
            AppendValues(values);

            var left = Matrix<double>.Build.DenseOfRows(values);
            var right = Vector<double>.Build.DenseOfEnumerable(GetRightSide(left.RowCount));

            return left.Solve(right);
        }

        private void AppendValues(List<List<double>> values)
        {
            values.ForEach(v => v.Add(-1.0d));
            var last = Enumerable.Repeat(1.0d, values.First().Count - 1).ToList();
            last.Add(0.0d);
            values.Add(last);
        }

        private IEnumerable<double> GetRightSide(int count)
        {
            return Enumerable.Repeat(0.0d, count - 1).Concat(new[] {1.0d});
        }

        private static SaddlePointSolution HasSaddlePoint(PayoffMatrix matrix)
        {
            var xStrategies = matrix.Rows.Select(r => (r.ToList().IndexOf(r.Min()), r.Min())).ToList();
            var yStrategies = matrix.Columns.Select(c => (c.ToList().IndexOf(c.Max()), c.Max())).ToList();
            var maxX = xStrategies.First(x => x.Item2.Equals(xStrategies.Select(s => s.Item2).Max()));
            var xIndex = xStrategies.IndexOf(maxX);
            var minY = yStrategies.First(y => y.Item2.Equals(yStrategies.Select(s => s.Item2).Min()));
            var yIndex = yStrategies.IndexOf(minY);

            return xIndex == minY.Item1
                ? new SaddlePointSolution {XStrategy = xIndex, YStrategy = yIndex, Payoff = maxX.Item2}
                : null;
        }
    }
}