using NooneUI.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            var mainQml = Path.Combine(Bootstrapper.ApplicationDirectory, "Views", "Main.qml");
            return new Bootstrapper()
                  .DetectQtRuntime()
                  .SetStyle("Material")
                  .RegisterTypes(() =>
                  {

                  })
                  .SetMainQml(mainQml)
                  .Launch(args);
                  
        }
    }
}
