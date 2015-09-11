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
using System.Windows.Shapes;

namespace AGL
{
    /// <summary>
    /// Logique d'interaction pour PasswordPopup.xaml
    /// </summary>
    public partial class PasswordPopup : Window
    {
        private bool deleteDatabase = false;
        public PasswordPopup(bool deleteDatabase)
        {
            this.deleteDatabase = deleteDatabase;
            InitializeComponent();
        }

        private void validatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (deleteDatabase)
                PasserelleB.deleteProjectDatabase(passbox.Password);

            PasserelleB.createProjectDatabaseAux(passbox.Password);

            if (deleteDatabase)
                PasserelleB.mcdModificationCheckAux();

            this.Close();
        }


    }
}
