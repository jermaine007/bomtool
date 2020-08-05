using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using System;

namespace NooneUI.Framework
{
    /// <summary>
    /// Base class for Application, providers logger and IoC container
    /// </summary>
    public abstract class LightApplicationBase : Application, ILoggerProvider, IContainerProvider
    {
        /// <summary>
        /// MainWindow
        /// </summary>
        public static Window MainWindow => (Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; } 

        /// <summary>
        /// Ioc Container 
        /// </summary>
        public IContainer Container { get; }

        protected LightApplicationBase()
        {
            Container = ContainerLocator.Current;
            Logger = Container.Get<ILogger>().Configure(this);

            this.Styles.Add(new StyleInclude(new Uri("avares://NooneUI/Styles"))
            {
                Source = new Uri("avares://NooneUI/Theme/LightStyles.xaml")
            });
            this.DataTemplates.Add(Container.Get<ViewLocator>());
            Logger.Debug("Application has been created.");
        }

        public override void Initialize()
        {
            // register view and view model
            Container.Get<IMvvmRelationships>().Register();
            // register services which are used by application
            RegisterServices(Container);
            // Setup other settings.
            Setup();
        }

        /// <summary>
        /// Register other services which are used by application
        /// </summary>
        /// <param name="container"></param>
        protected virtual void RegisterServices(IContainer container) => Logger.Debug("Register services.");

        /// <summary>
        /// Setup other settings for applicaiton.
        /// </summary>
        protected virtual void Setup() => Logger.Debug("Setup Application.");

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
        private void InitializeMainWindow(IClassicDesktopStyleApplicationLifetime desktop) => desktop.MainWindow = LookupMainEntry();

        /// <summary>
        /// Lookup the entry window.
        /// </summary>
        /// <returns></returns>
        protected abstract Window LookupMainEntry();
    }

    /// <summary>
    /// Base class for Application which support generic type
    /// </summary>
    /// <typeparam name="TView">The main entry window</typeparam>
    public abstract class LightApplicationBase<TView> : LightApplicationBase
        where TView : LightWindowBase
    {
        protected override Window LookupMainEntry()
        {
            TView view = Container.Get<TView>();
            if (view == null)
            {
                throw new InvalidOperationException("MainWindow must implement IView");
            }
            Logger.Debug($"Main entry window is {typeof(TView)}, set datacontext auto wired.");

            // set AutoWired true, so that auto bind a related view model.
            view.SetValue(Locator.AutoWiredProperty, true);
            return view;
        }
    }
}