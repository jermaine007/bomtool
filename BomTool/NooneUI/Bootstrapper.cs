using NooneUI.Logging;
using NooneUI.Services;
using Qml.Net;
using Qml.Net.Runtimes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Qml_ = Qml.Net.Qml;

namespace NooneUI
{
    abstract public class Bootstrapper : IBootstrapper
    {
        public static QCoreApplication Application { get; set; }

        public string ApplicationDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public ServicesContainer ServicesContainer => ServicesContainer.Instance;

        internal Action ResolveQtRuntime { get; set; } = () => RuntimeManager.DiscoverOrDownloadSuitableQtRuntime();

        internal Action DoRegisterTypes { get; set; }

        internal string Style { get; set; } = "Material";

        internal string MainQml { get; set; }

        internal List<string> ImportPath = new List<string>();

        public Bootstrapper() { this.MainQml = Path.Combine(ApplicationDirectory, "Main.qml"); }

        internal bool EnableLogging
        {
            set
            {
                ServicesContainer.Get<ILogger>().EnableLogging = value;
            }
        }

        public int Launch(string[] args = null)
        {
            ResolveQtRuntime();
            QQuickStyle.SetStyle(this.Style);
            RegisterServices();
            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    DoRegisterTypes?.Invoke();
                    DoAutoRegisterTypes();
                    AddImportPath(qmlEngine);
                    qmlEngine.Load(MainQml);
                    Application = application;
                    return application.Exec();
                }
            }
        }

        protected virtual void AddImportPath(QQmlApplicationEngine engine)
        {
            if (ImportPath.Count() == 0)
            {
                return;
            }
            foreach (var path in ImportPath)
            {
                engine.AddImportPath(path);
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

        protected virtual void RegisterServices() { }

    }

}
