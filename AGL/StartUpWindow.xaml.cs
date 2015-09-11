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

            LoadProject.modelioPath = Environment.GetEnvironmentVariable("MODELIO_PATH");
            LoadProject.netbeansPath = Environment.GetEnvironmentVariable("NETBEANS_PATH");
            LoadProject.jmerisePath = Environment.GetEnvironmentVariable("JMERISE_PATH");

            if (null == LoadProject.modelioPath)
                LoadProject.modelioPath = Environment.GetEnvironmentVariable("MODELIO_PATH", EnvironmentVariableTarget.User);
            if (null == LoadProject.netbeansPath)
                LoadProject.netbeansPath = Environment.GetEnvironmentVariable("NETBEANS_PATH", EnvironmentVariableTarget.User);
            if (null == LoadProject.jmerisePath)
                LoadProject.jmerisePath = Environment.GetEnvironmentVariable("JMERISE_PATH", EnvironmentVariableTarget.User);

            if (LoadProject.modelioPath == null || LoadProject.netbeansPath == null || LoadProject.jmerisePath == null)
            {
                System.Windows.Forms.MessageBox.Show("Les variables d'environnement n'ont pas été configurées, le programme ne peut pas s'éxecuter");
                this.Close();
            }
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
