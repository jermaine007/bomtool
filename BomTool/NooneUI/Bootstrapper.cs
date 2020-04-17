using NooneUI.Core;
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
        public QCoreApplication Application { get; private set; }

        public string ApplicationDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public ServicesContainer ServicesContainer => ServicesContainer.Instance;

        internal BootstrapperBuilder Builder { get; private set; }

        internal void Bind(BootstrapperBuilder builder) => Builder = builder;

        protected virtual void AddImportPath(QQmlApplicationEngine engine)
        {
            if (Builder.ImportPath.Count() == 0)
            {
                return;
            }
            foreach (var path in Builder.ImportPath)
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

        public void Dispatch(Action action) => Application.Dispatch(action);

        public Task DispatchAsync(Func<Task> action) => Application.DispatchAsync(action);


        public int Launch(string[] args)
        {
            Builder.ResolveQtRuntime();
            QQuickStyle.SetStyle(Builder.Style);
            RegisterServices();
            using (var application = new QGuiApplication(args))
            {
                using (var qmlEngine = new QQmlApplicationEngine())
                {
                    Builder.DoRegisterTypes?.Invoke();
                    DoAutoRegisterTypes();
                    AddImportPath(qmlEngine);
                    qmlEngine.Load(Builder.MainQml);
                    Application = application;
                    return application.Exec();
                }
            }
        }
    }

}
