using System;
using Avalonia;
using NooneUI.Framework;

namespace NooneUI
{

    /// <summary>
    /// Bootstrapper for application.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// AppBuilder Instance factory
        /// </summary>
        internal Func<AppBuilder> AppBuilderFactory { get; private set; }

        /// <summary>
        /// IoC Container Factory
        /// </summary>
        internal Func<IContainer> ContainerFactory { get; set; }

        /// <summary>
        /// Configure AppBuilder action
        /// </summary>
        internal Action<AppBuilder> ConfigureAppBuilder { get; set; }

        /// <summary>
        /// Configure container action
        /// </summary>
        internal Action<IContainer> ConfigureContainer { get; set; }

        /// <summary>
        /// Enable logging or not
        /// </summary>
        internal Func<bool> EnableLogging { get; set; }

        /// <summary>
        /// Create a bootstrapper instance
        /// </summary>
        /// <typeparam name="TApp"></typeparam>
        /// <returns>A bootstrapper instance</returns>
        public static Bootstrapper Create<TApp>() where TApp : LightApplicationBase, new() => new Bootstrapper
        {
            AppBuilderFactory = () => AppBuilder.Configure<TApp>()
        };

        private Bootstrapper() { }
    }

}
