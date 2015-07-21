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
            PasserelleA.loadBesoins_Click(sender, e);

        }

        private void loadMCD_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.loadMCD_Click(sender, e);
        }

        private void loadUseCase_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.loadUseCase_Click(sender, e);
        }

        private void validatePasserelleA_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.validatePasserelleA_Click(sender, e);
        }
        

    }
}
