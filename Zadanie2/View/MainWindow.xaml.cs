using System.Collections.Generic;
using System.Windows;
using Model;
using Model.Model;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var matrix1 = new PayoffMatrix(
                new List<IList<double>>
                {
                    new List<double> {3, -3, 7},
                    new List<double> {-1, 5, 2},
                    new List<double> {0, -4, 4}
                });

            var matrix2 = new PayoffMatrix(
                new List<IList<double>>
                {
                    new List<double> {3,2,1},
                    new List<double> {4,1,-3},
                    new List<double> {5,0,-5}
                });
            var solution = (new GameSolver()).Solve(matrix1);
            InitializeComponent();
        }
    }
}
