using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;


namespace NooneUI.Framework
{
    public abstract class LightApplicationBase : Application, IContainerProvider, ILoggerProvider
    {
        public static Window MainWindow => (Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

        protected readonly ILogger logger;
        protected readonly LightContainer container;

        protected LightApplicationBase()
        {
            logger = ((ILoggerProvider)this).Logger.Configure(this);
            container = ((IContainerProvider)this).Container;

            this.Styles.Add(new StyleInclude(new Uri("avares://NooneUI/Styles"))
            {
                Source = new Uri("avares://NooneUI/Theme/LightStyles.xaml")
            });
            this.DataTemplates.Add(container.Get<ViewLocator>());
            logger.Debug("Application has been created.");
        }

        public override void Initialize()
        {
            // register view and view model
            container.Get<MvvmRelationships>().Register();
            RegisterServices(container);
            Setup();
        }

        protected virtual void RegisterServices(LightContainer container) => logger.Debug("Register services.");

        protected virtual void Setup() => logger.Debug("Setup Application.");

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                InitializeMainWindow(desktop);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitializeMainWindow(IClassicDesktopStyleApplicationLifetime desktop) => desktop.MainWindow = LookupMainEntry();

        protected abstract Window LookupMainEntry();
    }

    public abstract class LightApplicationBase<TView> : LightApplicationBase
        where TView : LightWindowBase
    {
        protected override Window LookupMainEntry()
        {
            TView view = container.Get<TView>();
            if (view == null)
            {
                throw new InvalidOperationException("MainWindow must implement IView");
            }
            logger.Debug($"Main entry window is {typeof(TView)}, set datacontext auto wired.");
            view.SetValue(Locator.AutoWiredProperty, true);
            return view;
        }
    }
}