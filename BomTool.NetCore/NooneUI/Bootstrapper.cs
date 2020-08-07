using System;
using Avalonia;
using NooneUI.Framework;

namespace NooneUI
{

    public class Bootstrapper
    {

        internal Func<AppBuilder> AppBuilderFactory { get; private set; }

        internal Func<IContainer> ContainerFactory { get; set; }

        internal Action<AppBuilder> ConfigureAppBuilder { get; set; }

        internal Action<IContainer> ConfigureContainer { get; set; }

        public static Bootstrapper Create<TApp>() where TApp : Application, new() => new Bootstrapper
        {
            AppBuilderFactory = () => AppBuilder.Configure<TApp>()
        };

        private Bootstrapper() { }
    }

}
