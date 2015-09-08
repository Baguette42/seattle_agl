using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AGL
{
    /// <summary>
    /// Logique d'interaction pour StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            Process createProjectProcess = Process.Start(LoadProject.netbeansPath);
            createProjectProcess.WaitForExit();

            LoadProject loadProjectWindow = new LoadProject();
            loadProjectWindow.Show();
            this.Close();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            LoadProject loadProjectWindow = new LoadProject();
            loadProjectWindow.Show();
            this.Close();
        }
    }
}
