using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AGL
{
    public static class PasserelleC
    {
        public static bool isModified = false;

        public static void loadJava_Click(object sender, RoutedEventArgs e)
        {
            isModified = true;
            Process netbeans = Process.Start(LoadProject.netbeansPath);
            netbeans.WaitForExit();
        }

        internal static void validatePasserelleC_Click(object sender, RoutedEventArgs e)
        {
            isModified = false;
            bool mcd = PasserelleB.checkMCDcoherence();
            bool classDiagram = PasserelleB.checkClassDiagramCoherence();

            if (false == mcd)
                System.Windows.Forms.MessageBox.Show("Une modification a engendré une incohérence entre le mcd et les classes .java présentes dans le dossier généré");
            else if (false == classDiagram)
                System.Windows.Forms.MessageBox.Show("Une modification a engendré une incohérence entre le diagramme de classes et les classes .java présentes dans le dossier source");
            else
                System.Windows.Forms.MessageBox.Show("Aucun problème détecté.");
        }
    }
}
