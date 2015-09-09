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
using System.Diagnostics;
using System.IO;

namespace AGL
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string besoinsPath = null;
        private string usecasePath = null;
        private string mcdPath = null;
        private string classdiagramPath = null;

        public MainWindow()
        {
            InitializeComponent();
            validationNeeded.Visibility = Visibility.Hidden;
            checkBesoinsFile();
            checkUsecaseFile();
            checkClassDiagramFile();

            generatedJavaFilePath.Content = LoadProject.projectFolder;
        }

        private void loadBesoins_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.loadBesoins_Click(sender, e, besoinsPath);
            checkBesoinsFile();
            lockTable(PasserelleA.isModified);
        }

        private void loadMCD_Click(object sender, RoutedEventArgs e)
        {
            PasserelleB.loadMCD_Click(sender, e);
            checkMCDFile();
        }

        private void loadXMI_Click(object sender, RoutedEventArgs e)
        {
            //returns the selected file's path (null value if no file was selected)
            PasserelleB.loadXMI_Click(sender, e);
            checkClassDiagramFile();

        }

        private void generateSTB_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.generateSTB();
        }
        private void loadgeneratedJava_Click(object sender, RoutedEventArgs e)
        {
            PasserelleC.loadJava_Click(sender, e);

        }

        private void loadUseCase_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.loadUseCase_Click(sender, e);
            checkUsecaseFile();
            lockTable(PasserelleA.isModified);
        }

        private void validatePasserelleA_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.validatePasserelleA_Click(sender, e);
            lockTable(PasserelleA.isModified);
        }

        private void validatePasserelleB_Click(object sender, RoutedEventArgs e)
        {
            PasserelleB.validatePasserelleB_Click(sender, e, xmiFilePath.Content.ToString());
        }


        private void checkBesoinsFile()
        {
            if (File.Exists(LoadProject.projectFolder + "\\besoins.csv"))
            {
                besoinsPath = LoadProject.projectFolder + "\\besoins.csv";
                besoinsFilePath.Content = besoinsPath;
                PasserelleA.excelToJson(besoinsPath);
            }
        }

        private void checkUsecaseFile()
        {
            if (File.Exists(LoadProject.projectFolder + "\\usecase.xmi"))
            {
                usecasePath = LoadProject.projectFolder + "\\usecase.xmi";
                usecaseFilePath.Content = usecasePath;
                PasserelleA.useCaseToJson(usecasePath);
            }
        }

        private void checkMCDFile()
        {
            if (File.Exists(LoadProject.projectFolder + "\\mcd.xml"))
            {
                mcdPath = LoadProject.projectFolder + "\\mcd.xml";
                mcdFilePath.Content = mcdPath;
                PasserelleB.mcdToJson(mcdPath);
            }
        }

        private void checkClassDiagramFile()
        {
            if (File.Exists(LoadProject.projectFolder + "\\classdiagram.xmi"))
            {
                classdiagramPath = LoadProject.projectFolder + "\\classdiagram.xmi";
                xmiFilePath.Content = classdiagramPath;
                PasserelleB.classesToJson(classdiagramPath);
            }
        }

        public void lockTable(bool isModified)
        {
            if (isModified)
            {
                validationNeeded.Visibility = Visibility.Visible;
                foreach (TabItem t in tab.Items)
                    if (false == t.IsSelected)
                        t.IsEnabled = false;
            }
            else
            {
                validationNeeded.Visibility = Visibility.Hidden;
                foreach (TabItem t in tab.Items)
                    t.IsEnabled = true;
            }
        }

    }
}
