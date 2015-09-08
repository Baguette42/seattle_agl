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

        public static void loadJava_Click(object sender, RoutedEventArgs e)
        {
            Process netbeans = Process.Start(LoadProject.netbeansPath);
            netbeans.WaitForExit();
          
        }
    }
}
