using NooneUI.Logging;
using NooneUI.Services;
using Qml.Net;
using Qml.Net.Runtimes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NooneUI.Core
{

    abstract public class BootstrapperBuilder
    {
        public BootstrapperBuilder() => this.MainQml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Main.qml");

        internal Action ResolveQtRuntime { get; set; }

        internal Action DoRegisterTypes { get; set; }

        internal string Style { get; set; } = "Material";

        internal string MainQml { get; set; }

        internal List<string> ImportPath = new List<string>();

        internal bool EnableLogging
        {
            set => ServicesContainer.Instance.Get<ILogger>().EnableLogging = value;
        }

        public abstract IBootstrapper Build();

        public static BootstrapperBuilder<T> Create<T>() where T : IBootstrapper
        {
            ServicesContainer.Instance.Bind<BootstrapperBuilder<T>>();
            return ServicesContainer.Instance.Get<BootstrapperBuilder<T>>();
        }
    }

    public class BootstrapperBuilder<T> : BootstrapperBuilder where T : IBootstrapper
    {
        public BootstrapperBuilder() : base() => ServicesContainer.Instance.Bind<IBootstrapper, T>(true);

        public override IBootstrapper Build()
        {
            var bootstrapper = ServicesContainer.Instance.Get<T>();
            (bootstrapper as Bootstrapper).Bind(this);
            return bootstrapper;
        }
    }
}
