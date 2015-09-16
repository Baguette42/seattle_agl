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
using Newtonsoft.Json.Linq;

namespace AGL
{
    public static class PasserelleA
    {
        public static bool isModified = false;

        public static void loadBesoins_Click(object sender, RoutedEventArgs e, string besoinsPath)
        {
            isModified = true;

            if (besoinsPath != null)
            {
                Process excel = Process.Start(besoinsPath);
                excel.WaitForExit();
            }
            else
            {
                try
                {
                    StreamWriter sw = new StreamWriter(File.OpenWrite(LoadProject.projectFolder + "\\besoins.csv"));
                    sw.WriteLine("Exigence,Reference,Texte,Emplacement");
                    sw.Close();
                }
                finally
                {
                    Process excel = Process.Start(LoadProject.projectFolder + "\\besoins.csv");
                    excel.WaitForExit();
                }


            }
        }

        public static void loadUseCase_Click(object sender, RoutedEventArgs e)
        {
            isModified = true;

            Process modelio = Process.Start(LoadProject.modelioPath);
            modelio.WaitForExit();
        }

        public static void validatePasserelleA_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(LoadProject.projectFolder + "\\besoins.json") == false)
            {
                System.Windows.Forms.MessageBox.Show("Le fichier \"besoins.json\" est manquant");
            }
            else if (File.Exists(LoadProject.projectFolder + "\\usecase.json") == false)
            {
                System.Windows.Forms.MessageBox.Show("Le fichier \"usecase.json\" est manquant");

            }
            else
            {
                String msg = compareJsonSpec();
                if (msg.Length != 0)
                {
                    System.Windows.Forms.MessageBox.Show(msg, "Important");
                    return;
                }

                isModified = false;
            }
        }

        public static string compareJsonSpec()
        {
            String excelPath = LoadProject.projectFolder + "\\besoins.json";
            String usecasePath = LoadProject.projectFolder + "\\usecase.json";
            StreamReader excelReader = File.OpenText(excelPath);
            StreamReader usecaseReader = File.OpenText(usecasePath);

            JArray excelArray = JArray.Parse(excelReader.ReadToEnd());
            JArray usecaseArray = JArray.Parse(usecaseReader.ReadToEnd());

            String result = "";
            bool found = false;

            for (int i = 0; i < excelArray.Count; ++i)
            {
                JToken[] besoin = excelArray[i].ToArray();
                String reference = besoin[1].Value<string>();
                for (int j = 0; j < usecaseArray.Count; ++j)
                {
                    JToken[] usecase = usecaseArray[j].ToArray();
                    if (true == reference.Equals(usecase[1].Value<string>()))
                    {
                        found = true;
                    }
                }
                if (false == found)
                {
                    result += "L'expression du besoin " + besoin[0].Value<string>() + " n'est pas référencée dans les usecases." + Environment.NewLine;
                }
                found = false;
            }

            if (result.Length != 0)
                result += "Merci de corriger la(les) erreur(s) avant de continuer.";

            excelReader.Close();
            usecaseReader.Close();

            return result;
        }

        public static void generateSTB()
        {
            WordDocumentWriter.CreateSTB(LoadProject.projectFolder);
            System.Windows.Forms.MessageBox.Show("STB générée dans le dossier " + LoadProject.projectFolder);

        }

        public static void excelToJson(String filename)
        {
            StreamReader sreader = File.OpenText(filename);
            String path = filename.Substring(0, filename.LastIndexOf('\\'));
            String fileWrite = path + "\\besoins.json";

            //if there is already a JSON, we delete it to avoid writing over it
            if (File.Exists(fileWrite))
                File.Delete(fileWrite);

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
            if (buffer.Length > 0)
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

            //if there is already a JSON, we delete it to avoid writing over it
            if (File.Exists(fileWrite))
                File.Delete(fileWrite);

            StreamWriter swriter = new StreamWriter(File.OpenWrite(@fileWrite));

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("//packagedElement");

            swriter.Write("[");
            String buffer = "";
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection attributes = node.Attributes;
                if (node.Attributes["xmi:type"].Value.Equals("uml:UseCase"))
                {
                    buffer += "[\"" + node.Attributes["name"].Value + "\",\"";
                    buffer += node.SelectSingleNode("ownedComment").SelectSingleNode("body").InnerText;
                    //FIXME vérifier si la langue nique pas le CRLF
                    //remove linebreak
                    //buffer = buffer.Substring(0, buffer.Length - 2);
                    buffer += "\"],";
                }
            }
            buffer = buffer.Substring(0, buffer.Length - 1);
            swriter.Write(buffer);
            swriter.Write("]");
            swriter.Flush();
            swriter.Close();
        }
    }
}
