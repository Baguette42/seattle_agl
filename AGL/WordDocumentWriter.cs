using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;

namespace AGL
{
    public static class WordDocumentWriter
    {

         //Create document method
        public static void CreateSTB(string path)
        {
            try
            {
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //Add header into the document
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "Spécification Technique du Besoin";
                }

                //Add the footers into the document
                /*foreach (Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 10;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "Footer text goes here";
                }*/

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = "This is test document " + Environment.NewLine;

                object styleHeading1 = "Titre 1";
                object styleHeading2 = "Titre 2";



                /*
                 * INTRODUCTION 
                 */
                //Add Introduction paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph intro = document.Content.Paragraphs.Add(ref missing);
                intro.Range.set_Style(ref styleHeading1);
                intro.Range.Text = "Introduction";
                intro.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                para2.Range.set_Style(ref styleHeading2);
                para2.Range.Text = "Objet et objectifs du document";
                para2.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
                para3.Range.set_Style(ref styleHeading2);
                para3.Range.Text = "Domaine d'application";
                para3.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph termi = document.Content.Paragraphs.Add(ref missing);
                termi.Range.set_Style(ref styleHeading2);
                termi.Range.Text = "Terminologie";
                termi.Range.InsertParagraphAfter();
                
                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph docs_ref = document.Content.Paragraphs.Add(ref missing);
                docs_ref.Range.set_Style(ref styleHeading2);
                docs_ref.Range.Text = "Documents référencés";
                docs_ref.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph pres_doc = document.Content.Paragraphs.Add(ref missing);
                pres_doc.Range.set_Style(ref styleHeading2);
                pres_doc.Range.Text = "Présentation du document";
                pres_doc.Range.InsertParagraphAfter();

                /*
                 * DESCRIPTION GENERALE 
                 */
                //Add Introduction paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph desc_gen = document.Content.Paragraphs.Add(ref missing);
                desc_gen.Range.set_Style(ref styleHeading1);
                desc_gen.Range.Text = "Description générale";
                desc_gen.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph apercu = document.Content.Paragraphs.Add(ref missing);
                apercu.Range.set_Style(ref styleHeading2);
                apercu.Range.Text = "Aperçu général du logiciel";
                apercu.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph func_prod = document.Content.Paragraphs.Add(ref missing);
                func_prod.Range.set_Style(ref styleHeading2);
                func_prod.Range.Text = "Fonctionnalités du produit";
                func_prod.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph carac_user = document.Content.Paragraphs.Add(ref missing);
                carac_user.Range.set_Style(ref styleHeading2);
                carac_user.Range.Text = "Caractéristiques des utilisateurs";
                carac_user.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph contr = document.Content.Paragraphs.Add(ref missing);
                contr.Range.set_Style(ref styleHeading2);
                contr.Range.Text = "Contraintes";
                contr.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph hypo = document.Content.Paragraphs.Add(ref missing);
                hypo.Range.set_Style(ref styleHeading2);
                hypo.Range.Text = "Hypothèses et traçabilité";
                hypo.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph exi = document.Content.Paragraphs.Add(ref missing);
                exi.Range.set_Style(ref styleHeading2);
                exi.Range.Text = "Exigences et contraintes différées";
                exi.Range.InsertParagraphAfter();

                /*
                * SPECIFICATION DETAILLEE
                */
                //Add Introduction paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = "Spécification détaillée";
                para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph inter_ex = document.Content.Paragraphs.Add(ref missing);
                inter_ex.Range.set_Style(ref styleHeading2);
                inter_ex.Range.Text = "Interfaces externes";
                inter_ex.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph desc_func = document.Content.Paragraphs.Add(ref missing);
                desc_func.Range.set_Style(ref styleHeading2);
                desc_func.Range.Text = "Description des fonctions";
                desc_func.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph ex_perfs= document.Content.Paragraphs.Add(ref missing);
                ex_perfs.Range.set_Style(ref styleHeading2);
                ex_perfs.Range.Text = "Exigences de performance";
                ex_perfs.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph descr_donnees = document.Content.Paragraphs.Add(ref missing);
                descr_donnees.Range.set_Style(ref styleHeading2);
                descr_donnees.Range.Text = "Description des données";
                descr_donnees.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph contraintes_env = document.Content.Paragraphs.Add(ref missing);
                contraintes_env.Range.set_Style(ref styleHeading2);
                contraintes_env.Range.Text = "Contraintes d'environnement de développement";
                contraintes_env.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph exi_qual = document.Content.Paragraphs.Add(ref missing);
                exi_qual.Range.set_Style(ref styleHeading2);
                exi_qual.Range.Text = "Exigences qualité";
                exi_qual.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph exi_compl = document.Content.Paragraphs.Add(ref missing);
                exi_compl.Range.set_Style(ref styleHeading2);
                exi_compl.Range.Text = "Exigences complémentaires";
                exi_compl.Range.InsertParagraphAfter();
                /*

                //Create a 5X5 table and insert some dummy record
                Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, 5, 5, ref missing, ref missing);

                firstTable.Borders.Enable = 1;
                foreach (Microsoft.Office.Interop.Word.Row row in firstTable.Rows)
                {
                    foreach (Microsoft.Office.Interop.Word.Cell cell in row.Cells)
                    {
                        //Header row
                        if (cell.RowIndex == 1)
                        {
                            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                            cell.Range.Font.Bold = 1;
                            //other format properties goes here
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
                            cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                            //Center alignment for the Header cells
                            cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        }
                        //Data row
                        else
                        {
                            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
                        }
                    }
                }
                */

                //Save the document
                object filename = @"";
                //if a path is defined, filename = selected path + STB.docx
                if (path != null && !path.Equals(""))
                {
                    string temp = path + "\\STB.docx";
                    filename = @temp;
                }
                else
                    //else, the following default emplacement is used
                    filename = @"C:\Users\Public\Documents\STB.docx";

                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
                //MessageBox.Show("Document created successfully !");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
        }
    }
}
