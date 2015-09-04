using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace AGL
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void loadBesoins_Click(object sender, RoutedEventArgs e)
        {
            //returns the selected file's path (null value if no file was selected)
            string resultPath = PasserelleA.loadBesoins_Click(sender, e);
            //if a file was selected, we update the label with it's path
            if (resultPath != null)
                besoinsFilePath.Content = resultPath;

        }

        private void loadMCD_Click(object sender, RoutedEventArgs e)
        {
            //returns the selected file's path (null value if no file was selected)
            string resultPath = PasserelleA.loadMCD_Click(sender, e);
            //if a file was selected, we update the label with it's path
            if (resultPath != null)
                mcdFilePath.Content = resultPath;

        }

        private void loadXMI_Click(object sender, RoutedEventArgs e)
        {
            //returns the selected file's path (null value if no file was selected)
            string resultPath = PasserelleB.loadXMI_Click(sender, e);
            //if a file was selected, we update the label with it's path
            if (resultPath != null)
                xmiFilePath.Content = resultPath;
        }

        private void loadgeneratedJava_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            generatedJavaFilePath.Content = dialog.SelectedPath;
        }

        private void loadUseCase_Click(object sender, RoutedEventArgs e)
        {
            //returns the selected file's path (null value if no file was selected)
            string resultPath = PasserelleA.loadUseCase_Click(sender, e);
            //if a file was selected, we update the label with it's path
            if (resultPath != null)
                usecaseFilePath.Content = resultPath;
        }

        private void validatePasserelleA_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.validatePasserelleA_Click(sender, e);
        }

        private void validatePasserelleB_Click(object sender, RoutedEventArgs e)
        {
            PasserelleB.validatePasserelleB_Click(sender, e);
        }
        

    }
}
