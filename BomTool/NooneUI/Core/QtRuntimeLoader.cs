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
        private static readonly string ResourceName = "NooneUI.QtRuntime.Runtime.qt.tar.gz";
        private static readonly string AssemblyName = "NooneUI.QtRuntime.dll";

        public Assembly Assembly { get; private set; }

        public bool HasBuiltInRuntime => new Func<bool>(() =>
        {
            var dll = Path.Combine(Bootstrapper.ApplicationDirectory, AssemblyName);
            if (!File.Exists(dll))
            {
                return false;
            }
            try
            {
                Assembly = Assembly.LoadFrom(dll);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return Assembly.GetManifestResourceNames().Any(n => n == ResourceName);
        })();

        public void Load() => new Action(() =>
        {
            var runtimeDir = Path.Combine(Bootstrapper.ApplicationDirectory, "qmlnet-qt-runtimes");
            if (!Directory.Exists(runtimeDir))
            {
                var stream = Assembly.GetManifestResourceStream(ResourceName);
                RuntimeManager.ExtractTarGZStream(stream, runtimeDir);
            }
            RuntimeManager.ConfigureRuntimeDirectory(runtimeDir);
        })();

    }
}
