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
            ServicesContainer.Instance.Bind<IBootstrapper, BomToolBootstrapper>();
            var bootstapper = ServicesContainer.Instance.Get<IBootstrapper>();
            var mainQml = Path.Combine(bootstapper.ApplicationDirectory, "Views", "Main.qml");
            

            return bootstapper
                  .DetectQtRuntime()
                  .EnableLogging(true)
                  .SetStyle("Material")
                  .SetMainQml(mainQml)
                  .Launch(args);
                  
        }
    }
}
