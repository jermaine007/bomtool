using NooneUI.Services;
using Qml.Net.Runtimes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NooneUI.Core
{

    class QtRuntimeLoader
    {
        public ServicesContainer Container => ServicesContainer.Instance;
        private string qtRuntimeAssembly = null;

        public Assembly Assembly { get; private set; }

        public string ResourceName => $"{QtRuntimeAssembly}.qt.tar.gz";

        public string QtRuntimeAssembly => qtRuntimeAssembly ?? (qtRuntimeAssembly = new Func<string>(() =>
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "NooneUI.QtRuntime.Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "NooneUI.QtRuntime.Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "NooneUI.QtRuntime.OSX";
            }
            return string.Empty;
        })());

        public bool HasBuiltInRuntime => new Func<bool>(() =>
        {
            if (string.IsNullOrEmpty(QtRuntimeAssembly))
            {
                return false;
            }
            var dll = Path.Combine(Container.Get<IBootstrapper>().ApplicationDirectory, $"{QtRuntimeAssembly}.dll");
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
            var runtimeDir = Path.Combine(Container.Get<IBootstrapper>().ApplicationDirectory, "qmlnet-qt-runtimes");
            if (!Directory.Exists(runtimeDir))
            {
                var stream = Assembly.GetManifestResourceStream(ResourceName);
                RuntimeManager.ExtractTarGZStream(stream, runtimeDir);
            }
            RuntimeManager.ConfigureRuntimeDirectory(runtimeDir);
        })();

    }
}
