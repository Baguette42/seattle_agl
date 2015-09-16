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
            PasserelleB.checkMCDcoherence();
            PasserelleB.checkClassDiagramCoherence();
            isModified = false;
        }
    }
}
