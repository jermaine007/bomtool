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
            => BootstrapperBuilder.Create<BomToolBootstrapper>()
                  .DetectQtRuntime()
                  .EnableLogging(true)
                  .SetStyle("Material")
                   // set main ui qml file
                  .SetMainQml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Main.qml"))
                  .Build()
                  .Launch(args);

    }
}
