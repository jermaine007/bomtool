using System;
using Avalonia;
using NooneUI.Framework;

namespace NooneUI
{
    /// <summary>
    /// <para>Bootstrapper for application.</para>
    /// <para>First create a App class like</para>
    /// <code>
    /// public class App : LightApplicationBaseMainWindow&gt;
    /// {
    ///     public App()
    ///     {
    ///         AvaloniaXamlLoader.Load(this);
    ///     }
    ///         .
    ///         .
    ///         .
    ///         do other things
    /// }
    /// </code>
    /// <code>Bootstrapper.Create&lt;App&gt;()</code>
    /// Otherwise, if you have no special settings for App, just Use
    /// <code>Bootstrapper.Create&lt;LightApplicationBase&lt;MainWindow&gt;&gt;()</code>
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
        internal Func<Type> LoggingBinding { get; set; }

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
