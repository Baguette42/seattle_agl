using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public static class PasserelleB
    {
        public static string loadXMI_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
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
                return filename;
            } else {
                return null;
            }
        }

        public static void classesToJson(String filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            String path = filename.Substring(0, filename.LastIndexOf('\\'));
            String fileWrite = path + "\\classes.json";
            StreamWriter swriter = new StreamWriter(File.OpenWrite(@fileWrite));

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//packagedElement");

            swriter.Write("[");
            String buffer = "";
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection attributes = node.Attributes;
                if (node.Attributes["xmi:type"].Value.Equals("uml:Class")) {
                    buffer += "[";
                    buffer += "\"" + node.Attributes["name"].Value + "\"";

                    //ajout des attributs
                    foreach (XmlNode attributeNode in node.ChildNodes) {
                        if (attributeNode.Name.Equals("ownedAttribute")) {
                            buffer += ", ";
                            buffer += "\"" + attributeNode.Attributes["name"].Value + "\"";
                        }
                    }

                    foreach (XmlNode methodNode in node.ChildNodes) {
                        if (methodNode.Name.Equals("ownedOperation")) {
                            buffer += ", ";
                            buffer += "\"" + methodNode.Attributes["name"].Value + "\"";
                        }
                    }
                    buffer += "],";
                }
            }
            buffer = buffer.Substring(0, buffer.Length - 1);
            swriter.Write(buffer);
            swriter.Write("]");
            swriter.Flush();
            swriter.Close();
        }
        public static void validatePasserelleB_Click(object sender, RoutedEventArgs e, String xmiPath)
        {
            classesToJson(xmiPath);
        }
    }
}
