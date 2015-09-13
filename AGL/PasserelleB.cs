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
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace AGL
{
    public static class PasserelleB
    {
        public static bool isModified = false;

        public static void loadXMI_Click(object sender, RoutedEventArgs e)
        {
            isModified = true;
            Process modelio = Process.Start(LoadProject.modelioPath);
            modelio.WaitForExit();
            moveJavaFilesToProjectSrc();
        }

        public static void loadMCD_Click(object sender, RoutedEventArgs e)
        {
            isModified = true;
            Process jmerise = Process.Start(LoadProject.jmerisePath);
            jmerise.WaitForExit();
        }

        public static void mcdToJson(String filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            String path = filename.Substring(0, filename.LastIndexOf('\\'));
            String fileWrite = path + "\\mcd.json";

            //if there is already a JSON, we delete it to avoid writing over it
            if (File.Exists(fileWrite))
                File.Delete(fileWrite);

            StreamWriter swriter = new StreamWriter(File.OpenWrite(@fileWrite));

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//entite");

            swriter.Write("[");
            String buffer = "";
            foreach (XmlNode node in nodes)
            {
                buffer += "[\"" + node.Attributes["name"].Value + "\",";
                XmlNodeList attributes = doc.DocumentElement.SelectNodes("//entite[@name='" + node.Attributes["name"].Value + "']/attribut");
                foreach (XmlNode attribute in attributes)
                {
                    buffer += "\"" + attribute.Attributes["name"].Value + "\",";
                }
                // Suppression du caractère , en trop
                buffer = buffer.Substring(0, buffer.Length - 1);
                buffer += "],";
            }
            buffer = buffer.Substring(0, buffer.Length - 1);
            swriter.Write(buffer);
            swriter.Write("]");
            swriter.Flush();
            swriter.Close();
        }

        public static void classesToJson(String filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            String path = filename.Substring(0, filename.LastIndexOf('\\'));
            String fileWrite = path + "\\classdiagram.json";

            //if there is already a JSON, we delete it to avoid writing over it
            try
            {
                if (File.Exists(fileWrite))
                    File.Delete(fileWrite);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Erreur : le JSON du diagramme de classe n'a pas pu être regénéré");
            }

            StreamWriter swriter = new StreamWriter(File.OpenWrite(@fileWrite));

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//packagedElement");

            swriter.Write("[");
            String buffer = "";
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection attributes = node.Attributes;
                if (node.Attributes["xmi:type"].Value.Equals("uml:Class"))
                {
                    buffer += "[";
                    buffer += "\"" + node.Attributes["name"].Value + "\"";

                    //ajout des attributs
                    foreach (XmlNode attributeNode in node.ChildNodes)
                    {
                        if (attributeNode.Name.Equals("ownedAttribute"))
                        {
                            buffer += ", ";
                            buffer += "\"" + attributeNode.Attributes["name"].Value + "\"";
                        }
                    }

                    foreach (XmlNode methodNode in node.ChildNodes)
                    {
                        if (methodNode.Name.Equals("ownedOperation"))
                        {
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
            //classesToJson(xmiPath);
            createProjectDatabase();
            isModified = false;
        }

        private static void createProjectDatabase()
        {
            //if the sql script exist, we generate the database
            if (File.Exists(LoadProject.projectFolder + "\\mcd.sql"))
            {
                //asks for MySQL root password and calls createProjectDatabaseAux
                PasswordPopup pwp = new PasswordPopup(false);
                pwp.Show();
            }
            else
                System.Windows.Forms.MessageBox.Show("Le script 'mcd.sql' n'a pas été trouvé, la base de données n'a pas pu être générée");
        }

        public static void createProjectDatabaseAux(string rootPassword)
        {
            string createDbCommandline = "mysql -u root --password=\"" + rootPassword + "\" -e \" create database " + LoadProject.projectName + "\"";
            string runScriptCommandline = "mysql -u root --password=\"" + rootPassword + "\" " + LoadProject.projectName + " < " + LoadProject.projectFolder + "\\mcd.sql";

            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            processInfo.FileName = "cmd.exe";

            processInfo.Arguments = "/C " + createDbCommandline;
            Process.Start(processInfo).WaitForExit();

            processInfo.Arguments = "/C " + runScriptCommandline;
            Process.Start(processInfo).WaitForExit();

            System.Windows.Forms.MessageBox.Show("Database générée");
        }

        public static void deleteProjectDatabase(string rootPassword)
        {
            string deleteDbCommandline = "mysql -u root --password=\"" + rootPassword + "\" -e \" drop database " + LoadProject.projectName + "\"";

            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            processInfo.FileName = "cmd.exe";

            processInfo.Arguments = "/C " + deleteDbCommandline;
            Process.Start(processInfo).WaitForExit();
        }

        public static void mcdModificationCheck()
        {
            if (Directory.Exists(LoadProject.projectFolder + "\\src\\DAO"))
            {
                if (checkMCDcoherence() == false)
                {
                    //DAO are deleted
                    Directory.Delete(LoadProject.projectFolder + "\\src\\DAO", true);
                    File.Delete(LoadProject.projectFolder + "\\src\\hibernate.cfg.xml");
                    File.Delete(LoadProject.projectFolder + "\\src\\hibernate.reveng.xml");

                    //asks for MySQL root password then drops the existing database and recreates it with the new script
                    PasswordPopup pwp = new PasswordPopup(true);
                    pwp.Show();

                }
            }
        }
            public static void mcdModificationCheckAux()
        {
         
            System.Windows.Forms.MessageBox.Show("La base de donnée a été regénérée avec le script mcd.sql, Netbeans va maintenant s'ouvrir pour permettre la regeneration des DAO" );

            Process netbeans = Process.Start(LoadProject.netbeansPath);
            netbeans.WaitForExit();
        }

        public static bool checkMCDcoherence()
        {
            if (Directory.Exists(LoadProject.projectFolder + "\\src\\DAO"))
            {
                String mcdPath = LoadProject.projectFolder + "\\mcd.json";
                StreamReader mcdReader = File.OpenText(mcdPath);

                JArray mcdArray = JArray.Parse(mcdReader.ReadToEnd());
                for (int i = 0; i < mcdArray.Count; ++i)
                {
                    JToken[] table = mcdArray[i].ToArray();
                    String tableName = table[0].Value<string>();

                    if (associatedDaoExists(tableName) == false)
                        return false;
                }
            }

            return true;
        }

        public static bool associatedDaoExists(string tableName)
        {
            return File.Exists(LoadProject.projectFolder + "\\src\\DAO\\" + tableName + "DAO.java");
        }

        public static bool associatedJavaClassExists(string classname)
        {
            return File.Exists(LoadProject.projectFolder + "\\src\\" + LoadProject.projectName + "\\" + classname + ".java");
        }

        public static void moveJavaFilesToProjectSrc()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Selectionnez le dossier \"src\" dans lequel modelio a généré le code Java";
            DialogResult generatedJavaFolder = dialog.ShowDialog();
            string sourceFolder = dialog.SelectedPath;

            string destinationPath = LoadProject.projectFolder + "\\src\\" + LoadProject.projectName;

            if (Directory.Exists(sourceFolder))
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourceFolder, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(sourceFolder, destinationPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourceFolder, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(sourceFolder, destinationPath), true);
            }
        }

        public static bool checkClassDiagramCoherence()
        {


            if (Directory.Exists(LoadProject.projectFolder + "\\src") && File.Exists(LoadProject.projectFolder + "\\classdiagram.json"))
            {
                String classdiagramPath = LoadProject.projectFolder + "\\classdiagram.json";
                StreamReader classdiagramReader = File.OpenText(classdiagramPath);

                JArray classdiagramArray = JArray.Parse(classdiagramReader.ReadToEnd());
                for (int i = 0; i < classdiagramArray.Count; ++i)
                {
                    JToken[] classToCheck = classdiagramArray[i].ToArray();
                    String className = classToCheck[0].Value<string>();

                    if (associatedJavaClassExists(className) == false)
                        return false;
                }
            }

            return true;

        }
    }
}
