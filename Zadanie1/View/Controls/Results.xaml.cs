using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        public Results()
        {
            InitializeComponent();
        }

        public Func<string> OpenFileDialog { get; } = BrowseForFile;

        public Action PlaySound { get; } = SystemSounds.Beep.Play;

        private static string BrowseForFile()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
                return dialog.FileName;
            return null;
        }
    }
}
