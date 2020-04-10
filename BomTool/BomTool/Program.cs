using BomTool.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BomTool
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            var mainQml = Path.Combine(Bootstrapper.AppDir, "Views", "Main.qml");
            return new Bootstrapper()
                  .DetectQtRuntime()
                  .SetStyle("Material")
                  .RegisterTypes(() =>
                  {

                  })
                  .SetMainMainQml(mainQml)
                  .Launch(args);
                  
        }
    }
}
