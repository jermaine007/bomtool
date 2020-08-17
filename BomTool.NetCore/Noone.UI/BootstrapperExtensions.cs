using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia;
using Ninject.Infrastructure.Language;
using Noone.UI.Core;

namespace Noone.UI
{
    /// <summary>
    /// Bootstrapper extensions
    /// </summary>
    public static class BootstrapperExtensions
    {
        /// <summary>
        /// Build in logger type full name;
        /// </summary>
        private static readonly string BuiltInLogger = "Noone.UI.Core.LightLogger";

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
        ///
        /// </summary>
        /// <typeparam name="TLoggerImpl"></typeparam>
        /// <param name="bootstrapper"></param>
        /// <returns></returns>
        public static Bootstrapper Use<TLoggerImpl>(this Bootstrapper bootstrapper) where TLoggerImpl : ILogger
        {
            bootstrapper.LoggingBinding = () => typeof(TLoggerImpl);
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

            // Do auto register
            AutoRegister(ContainerLocator.Current, bootstrapper);

            // register service
            bootstrapper.ConfigureContainer?.Invoke(ContainerLocator.Current);

            // get app builder
            AppBuilder builder = bootstrapper.AppBuilderFactory();

            // Configure App
            bootstrapper.ConfigureAppBuilder?.Invoke(builder);

            // start desktop App
            builder.StartWithClassicDesktopLifetime(args);
        }

        /// <summary>
        /// Auto register
        /// </summary>
        /// <param name="container"></param>
        /// <param name="bootstrapper"></param>
        private static void AutoRegister(IContainer container, Bootstrapper bootstrapper)
        {
            // resolve logger binding
            ResolveLoggerBinding(container, bootstrapper);

            ILogger logger = container.Get<ILogger>().Configure(typeof(Bootstrapper));

            logger.Debug($"Auto registering all services types from {AppDomain.CurrentDomain.BaseDirectory}");

            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
               .Where(assembly => !assembly.IsDynamic)
               .SelectMany(assembly => assembly.GetTypes())
               .Where(type => !type.IsAbstract
                      && type.HasAttribute<AutoRegisterAttribute>()).ToList();

            // auto register built in mode
            RegisterTypesToContainer(container, logger, types);

            // resolve mvvm relationships
            container.Get<IMvvmRelationships>().AddRegistration(types);

            // Resolve logger bindings
            static void ResolveLoggerBinding(IContainer ioc, Bootstrapper starter)
            {
                // configure logger
                if (starter.LoggingBinding != null)
                {
                    Type loggerType = starter.LoggingBinding();
                    ioc.Register(typeof(ILogger), loggerType);
                }
                else
                {
                    Type loggerType = Type.GetType(BuiltInLogger);
                    ioc.Register(typeof(ILogger), loggerType);
                }
            }

            // register types to container
            static void RegisterTypesToContainer(IContainer ioc, ILogger log, IEnumerable<Type> services)
            {
                foreach (Type type in services)
                {
                    var attribute = type.GetCustomAttribute<AutoRegisterAttribute>();

                    bool singleton = attribute.Singleton;
                    Type interfaceType = attribute.InterfaceType;

                    if (interfaceType != null)
                    {
                        log.Debug($"Auto register type -> [{interfaceType}, {type}], singleton -> {singleton}");
                        ioc.Register(interfaceType, type, singleton);
                    }
                    else
                    {
                        log.Debug($"Auto register type -> {type}, singleton -> {singleton}");
                        ioc.Register(type, singleton);
                    }

                }
            }


        }
    }
}
