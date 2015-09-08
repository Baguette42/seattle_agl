using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Xml;

namespace AGL
{
    public static class PasserelleA
    {
        public static void loadBesoins_Click(object sender, RoutedEventArgs e, string besoinsPath)
        {
            if (besoinsPath != null)
            {
                Process excel = Process.Start(besoinsPath);
                excel.WaitForExit();
            } else {
                StreamWriter sw = new StreamWriter(File.OpenWrite(LoadProject.projectFolder + "\\besoins.csv"));
                sw.Close();
                Process excel = Process.Start(LoadProject.projectFolder + "\\besoins.csv");
                excel.WaitForExit();
            }

            /*
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            //dlg.DefaultExt = ".xlsx";
            //dlg.Filter = "Excel file (.xlsx)|*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                Process excel = Process.Start(filename);
                excelToJson(filename);
                return filename;
            } else {
                return null;
            }*/
        }

       

        public static void loadUseCase_Click(object sender, RoutedEventArgs e)
        {

            Process modelio = Process.Start(LoadProject.modelioPath);
            modelio.WaitForExit();
            
           /* // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xmi";
            dlg.Filter = "XMI file (.xmi)|*.xmi";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                useCaseToJson(filename);
                return filename;
            } else {
                return null;
            }*/
        }

        public static void validatePasserelleA_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                WordDocumentWriter.CreateSTB(dialog.SelectedPath);
                System.Windows.Forms.MessageBox.Show("STB générée dans le dossier " + dialog.SelectedPath);
            } else {
                System.Windows.Forms.MessageBox.Show("Aucun dossier selectionné, pas de traitement effectué");
            }
                
        }

        public static void excelToJson(String filename)
        {
            StreamReader sreader = File.OpenText(filename);
            String path = filename.Substring(0, filename.LastIndexOf('\\'));
            String fileWrite = path + "\\besoin.json";
            StreamWriter swriter = new StreamWriter(File.OpenWrite(@fileWrite));
            // Parse le header
            String line = sreader.ReadLine();

            // Parse le contenu
            swriter.Write("[");
            String buffer = "";
            while ((line = sreader.ReadLine()) != null)
            {
                buffer += "[";
                for (int i = 0; i < line.Split(',').Length; ++i)
                {
                    buffer += "\"" + line.Split(',')[i] + "\",";
                }
                // Suppression du caractère , en trop
                buffer = buffer.Substring(0, buffer.Length - 1);
                buffer += "],";
            }
            // Suppression du caractère , en trop
            buffer = buffer.Substring(0, buffer.Length - 1);
            swriter.Write(buffer);
            swriter.Write("]");
            swriter.Flush();

            sreader.Close();
            swriter.Close();
        }

       

        public static void useCaseToJson(String filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            String path = filename.Substring(0, filename.LastIndexOf('\\'));
            String fileWrite = path + "\\usecase.json";
            StreamWriter swriter = new StreamWriter(File.OpenWrite(@fileWrite));

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//packagedElement");

            swriter.Write("[");
            String buffer = "";
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection attributes = node.Attributes;
                if (node.Attributes["xmi:type"].Value.Equals("uml:UseCase"))
                    buffer += "\"" + node.Attributes["name"].Value + "\",";
            }
            buffer = buffer.Substring(0, buffer.Length - 1);
            swriter.Write(buffer);
            swriter.Write("]");
            swriter.Flush();
            swriter.Close();
        }
    }
}
