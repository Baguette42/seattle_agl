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
            validationNeededA.Visibility = Visibility.Hidden;
            validationNeededB.Visibility = Visibility.Hidden;
            validationNeededC.Visibility = Visibility.Hidden;
            checkBesoinsFile();
            checkUsecaseFile();
            checkClassDiagramFile();
            checkMCDFile();

            if (Directory.Exists(LoadProject.projectFolder + "\\src\\dao"))
            {
                generatedJavaFilePath.Content = LoadProject.projectFolder + "\\src\\dao";
            }
            generatedJavaFilePath2.Content = LoadProject.projectFolder + "\\src";
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
            PasserelleB.mcdModificationCheck();
            lockTable(PasserelleB.isModified);
        }

        private void loadXMI_Click(object sender, RoutedEventArgs e)
        {
            //returns the selected file's path (null value if no file was selected)
            PasserelleB.loadXMI_Click(sender, e);
            checkClassDiagramFile();
            if (PasserelleB.checkClassDiagramCoherence() == false)
                System.Windows.Forms.MessageBox.Show("Une modification a engendré une incohérence entre le diagramme de classes et les classes .java présentes dans le dossier source");
            lockTable(PasserelleB.isModified);
        }

        private void generateSTB_Click(object sender, RoutedEventArgs e)
        {
            PasserelleA.generateSTB();
        }
        private void loadgeneratedJava_Click(object sender, RoutedEventArgs e)
        {
            validationNeededC.Visibility = Visibility.Visible;
            PasserelleC.loadJava_Click(sender, e);
            if ((PasserelleB.checkMCDcoherence()) == false)
                System.Windows.Forms.MessageBox.Show("Une modification a engendré une incohérence entre le mcd et les classes .java présentes dans le dossier généré");
            generatedJavaFilePath.Content = LoadProject.projectFolder + "\\dao";
        }

        private void loadgeneratedJava_Click2(object sender, RoutedEventArgs e)
        {
            validationNeededC.Visibility = Visibility.Visible;
            PasserelleC.loadJava_Click(sender, e);
            if ((PasserelleB.checkClassDiagramCoherence()) == false)
                System.Windows.Forms.MessageBox.Show("Une modification a engendré une incohérence entre le diagramme de classes et les classes .java présentes dans le dossier source");
            generatedJavaFilePath2.Content = LoadProject.projectFolder;
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
            lockTable(PasserelleB.isModified);
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
                validationNeededA.Visibility = Visibility.Visible;
                validationNeededB.Visibility = Visibility.Visible;
                foreach (TabItem t in tab.Items)
                    if (false == t.IsSelected)
                        t.IsEnabled = false;
            }
            else
            {
                validationNeededA.Visibility = Visibility.Hidden;
                validationNeededB.Visibility = Visibility.Hidden;
                foreach (TabItem t in tab.Items)
                    t.IsEnabled = true;
            }
        }

        private void validatePasserelleC_Click(object sender, RoutedEventArgs e)
        {
            PasserelleC.validatePasserelleC_Click(sender, e);
            validationNeededC.Visibility = Visibility.Hidden;
        }

    }
}
