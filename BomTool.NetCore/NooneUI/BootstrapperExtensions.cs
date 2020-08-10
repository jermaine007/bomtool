using System;
using Avalonia;
using NooneUI.Framework;

namespace NooneUI
{
    /// <summary>
    /// Bootstrapper extensions
    /// </summary>
    public static class BootstrapperExtensions
    {
        /// <summary>
        /// Use a new ioc container replace with built-in,
        /// If not set, would use built-in ioc container which based on Ninject
        /// </summary>
        /// <param name="bootstrapper">bootstrapper instance</param>
        /// <param name="containerFactory">ioc container factory</param>
        /// <returns>bootstrapper instance</returns>
        public static Bootstrapper Use(this Bootstrapper bootstrapper, Func<IContainer> containerFactory)
        {
            bootstrapper.ContainerFactory = containerFactory;
            return bootstrapper;
        }

        /// <summary>
        /// Use an action to register services into ioc container
        /// </summary>
        /// <param name="bootstrapper">bootstrapper instance</param>
        /// <param name="configureContainer">Action for registering service</param>
        /// <returns>bootstrapper instance</returns>
        public static Bootstrapper Use(this Bootstrapper bootstrapper, Action<IContainer> configureContainer)
        {
            bootstrapper.ConfigureContainer = configureContainer;
            return bootstrapper;
        }

        /// <summary>
        /// Use an action to configure AppBuilder
        /// </summary>
        /// <param name="bootstrapper">bootstrapper instance</param>
        /// <param name="configureAppBuilder">Action for configuring AppBuilder</param>
        /// <returns>bootstrapper instance</returns>
        public static Bootstrapper Use(this Bootstrapper bootstrapper, Action<AppBuilder> configureAppBuilder)
        {
            bootstrapper.ConfigureAppBuilder = configureAppBuilder;
            return bootstrapper;
        }

        /// <summary>
        /// Use a factory to indicate enable or disable logging
        /// </summary>
        /// <param name="bootstrapper">bootstrapper instance</param>
        /// <param name="enableLogging">factory enable or disable logging</param>
        /// <returns>bootstrapper instance</returns>
        public static Bootstrapper Use(this Bootstrapper bootstrapper, Func<bool> enableLogging)
        {
            bootstrapper.EnableLogging = enableLogging;
            return bootstrapper;
        }

        /// <summary>
        /// Launch app with args
        /// </summary>
        /// <param name="bootstrapper"></param>
        /// <param name="args"></param>
        public static void Launch(this Bootstrapper bootstrapper, params string[] args)
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
