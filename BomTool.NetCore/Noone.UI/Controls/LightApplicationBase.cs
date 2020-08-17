using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Noone.UI.Core;
using System;

namespace Noone.UI.Controls
{
    /// <summary>
    /// Base Application extends <see cref="Application"/>, provides <see cref="ILogger"/> and IoC container <see cref="IContainer"/>
    /// Implement <see cref="ILoggerProvider"/> and <see cref="IContainerProvider"/>
    /// </summary>
    public abstract class LightApplicationBase : Application,
        IContainerProvider,
        ILoggerProvider
    {
        // Base uri use to load styles
        private static readonly string BASE_URI = "avares://Noone.UI/Styles";

        // self-defined styles
        private static readonly string STYLES_URI = "avares://Noone.UI/Theme/LightStyles.xaml";

        /// <summary>
        /// Indicate the Main window of current Application
        /// </summary>
        public static Window MainWindow => (Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Ioc Container
        /// </summary>
        protected readonly IContainer container;

        protected LightApplicationBase()
        {
            container = ((IContainerProvider)this).Container;
            logger = ((ILoggerProvider)this).Logger;

            // load styles
            this.Styles.Add(new StyleInclude(new Uri(BASE_URI))
            {
                Source = new Uri(STYLES_URI)
            });

            // add view locator
            this.DataTemplates.Add(container.Get<ViewLocator>());

            logger.Debug("Application has been created.");
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                InitializeMainWindow(desktop);
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Initilize main window
        /// </summary>
        /// <param name="desktop"></param>
        private void InitializeMainWindow(IClassicDesktopStyleApplicationLifetime desktop) => desktop.MainWindow = LookupMainWindow();

        /// <summary>
        /// Lookup the entry window.
        /// </summary>
        /// <returns></returns>
        protected virtual Window LookupMainWindow() { return null; }
    }

    /// <summary>
    /// Base class for Application which support generic type
    /// </summary>
    /// <typeparam name="TView">Which should be a derived class from <see cref="LightWindowBase"/></typeparam>
    public class LightApplicationBase<TView> : LightApplicationBase
        where TView : LightWindowBase, new()
    {
        protected override Window LookupMainWindow() => container.Get<TView>().Setup(view =>
        {
            if (view == null)
            {
                throw new InvalidOperationException("MainWindow must implement IView");
            }
            logger.Debug($"Main entry window is {typeof(TView)}, set datacontext auto wired.");

            // set AutoWired true, so that auto bind a related view model.
            view.SetValue(Locator.AutoWiredProperty, true);
        });

    }
}
