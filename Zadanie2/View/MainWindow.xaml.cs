using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using Microsoft.Win32;
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
