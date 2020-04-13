using Qml.Net;
using Qml.Net.Runtimes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Qml_ = Qml.Net.Qml;

namespace BomTool.Core
{
    public class Bootstrapper
    {
        public static QCoreApplication Application { get; set; }
        public readonly static string AppDir = Path.GetDirectoryName(typeof(Bootstrapper).Assembly.Location);

        internal Action ResolveQtRuntime { get; set; } = () => RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();

        internal Action DoRegisterTypes { get; set; }

        internal string Style { get; set; } = "Material";

        internal string MainQml { get; set; } = Path.Combine(AppDir, "Main.qml");


        public int Launch(string[] args = null)
        {
            ResolveQtRuntime();
            QQuickStyle.SetStyle(this.Style);
            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    DoRegisterTypes?.Invoke();
                    DoAutoRegisterTypes();
                    qmlEngine.Load(MainQml);
                    Application = application;
                    return application.Exec();
                }
            }
        }

        protected virtual void DoAutoRegisterTypes() => AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsDefined(typeof(AutoRegisterTypeAttribute), false))
            .ToList()
            .ForEach(t =>
            {
                var a = t.GetCustomAttribute<AutoRegisterTypeAttribute>();
                if (a.IsSingleton)
                {
                    Qml_.RegisterSingletonType(t, t.Name, a.Uri, a.MajorVersion, a.MinorVersion);
                }
                else
                {
                    Qml_.RegisterType(t, t.Name, a.Uri, a.MajorVersion, a.MinorVersion);
                }
            });

    }

    static class BootstrapperExtensions
    {
        public static Bootstrapper DetectQtRuntime(this Bootstrapper bootstrapper)
        {
            var loader = new QtRuntimeLoader();
            if (loader.HasBuiltIn)
            {
                bootstrapper.ResolveQtRuntime = loader.LoadBuiltIn;
            }
            return bootstrapper;
        }

        public static Bootstrapper RegisterTypes(this Bootstrapper bootstrapper, Action doRegisterTypes)
        {
            bootstrapper.DoRegisterTypes = doRegisterTypes;
            return bootstrapper;
        }

        public static Bootstrapper SetStyle(this Bootstrapper bootstrapper, string style = "Materail")
        {
            bootstrapper.Style = style;
            return bootstrapper;
        }

        public static Bootstrapper SetMainMainQml(this Bootstrapper bootstrapper, string mainQml)
        {
            bootstrapper.MainQml = mainQml;
            return bootstrapper;
        }
    }

    

    class QtRuntimeLoader
    {
        private static readonly string ResourceName = "BomTool.runtime.qt.tar.gz";
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
