using NooneUI;
using NooneUI.Core;
using NooneUI.Services;
using Qml.Net;
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
            var mainQml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Main.qml");
            var bootstapper = new BootstrapperBuilder<BomToolBootstrapper>()
                  .DetectQtRuntime()
                  .EnableLogging(true)
                  .SetStyle("Material")
                  .SetMainQml(mainQml)
                  .Build();
            
            return bootstapper.Launch(args);
            

                  
        }
    }
}
