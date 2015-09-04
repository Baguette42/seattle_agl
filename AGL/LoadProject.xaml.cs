using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AGL
{
    /// <summary>
    /// Logique d'interaction pour LoadProject.xaml
    /// </summary>
    public partial class LoadProject : Window
    {
        private bool folderSelected = false;

        public LoadProject()
        {
            InitializeComponent();
        }

        private void loadProjectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            folderPath.Content = dialog.SelectedPath;
            folderSelected = true;
            // if a folder is selected, validation button is enabled
            validateProjectFolder.IsEnabled = true;
    
        }

        private void validateProjectFolder_Click(object sender, RoutedEventArgs e)
        {

            if (folderSelected)
            {
                MainWindow mainWin = new MainWindow();
                mainWin.Show();
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Aucun dossier selectionné, pas de traitement effectué");
            }
        }

    }
}
