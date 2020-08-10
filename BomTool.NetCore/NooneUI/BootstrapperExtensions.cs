using System;
using Avalonia;
using NooneUI.Framework;

namespace NooneUI
{
    public static class BootstrapperExtensions
    {
        public static Bootstrapper Use(this Bootstrapper bootstrapper, Func<IContainer> containerFactory)
        {
            bootstrapper.ContainerFactory = containerFactory;
            return bootstrapper;
        }

        public static Bootstrapper Use(this Bootstrapper bootstrapper, Action<IContainer> configureContainer)
        {
            bootstrapper.ConfigureContainer = configureContainer;
            return bootstrapper;
        }

        public static Bootstrapper Use(this Bootstrapper bootstrapper, Action<AppBuilder> configureAppBuilder)
        {
            bootstrapper.ConfigureAppBuilder = configureAppBuilder;
            return bootstrapper;
        }

        public static Bootstrapper Use(this Bootstrapper bootstrapper, Func<bool> enableLogging)
        {
            bootstrapper.EnableLogging = enableLogging;
            return bootstrapper;
        }

        public static void Launch(this Bootstrapper bootstrapper, string[] args)
        {
            // setup service container
            ContainerLocator.Configure(bootstrapper.ContainerFactory);

            // register service
            bootstrapper.ConfigureContainer?.Invoke(ContainerLocator.Current);

            // configure logger
            LoggerExtensions.Enable(bootstrapper.EnableLogging?.Invoke() ?? true);

            // get app builder
            AppBuilder builder = bootstrapper.AppBuilderFactory();

            // Configure App
            bootstrapper.ConfigureAppBuilder?.Invoke(builder);

            // start desktop App
            builder.StartWithClassicDesktopLifetime(args);
        }
    }
}
