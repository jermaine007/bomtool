using NooneUI.Core;
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
            var mainQml = Path.Combine(Bootstrapper.ApplicationDirectory, "Views", "Main.qml");
            QmlNetConfig.UnhandledTaskException += QmlNetConfig_UnhandledTaskException;
            return new Bootstrapper()
                  .DetectQtRuntime()
                  .SetStyle("Material")
                  .RegisterTypes(() =>
                  {

                  })
                  .SetMainQml(mainQml)
                  .Launch(args);
                  
        }

        private static void QmlNetConfig_UnhandledTaskException(AggregateException obj)
        {
            foreach (var ex in obj.InnerExceptions)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
