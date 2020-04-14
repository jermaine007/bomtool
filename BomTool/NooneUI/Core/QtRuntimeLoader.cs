using Qml.Net.Runtimes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NooneUI.Core
{


    class QtRuntimeLoader
    {
        private static readonly string ResourceName = "NooneUI.runtime.qt.tar.gz";
        private static readonly Assembly ThisAssembly = typeof(Bootstrapper).Assembly;

        public bool HasBuiltIn { get; }

        public QtRuntimeLoader()
        {
            HasBuiltIn = ThisAssembly.GetManifestResourceNames().Any(n => n == ResourceName);
        }

        public void LoadBuiltIn()
        {
            var runtimeDir = Path.Combine(Bootstrapper.AppDir, "qmlnet-qt-runtimes");
            if (!Directory.Exists(runtimeDir))
            {
                var stream = ThisAssembly.GetManifestResourceStream(ResourceName);
                RuntimeManager.ExtractTarGZStream(stream, runtimeDir);
            }
            RuntimeManager.ConfigureRuntimeDirectory(runtimeDir);
        }
    }
}
