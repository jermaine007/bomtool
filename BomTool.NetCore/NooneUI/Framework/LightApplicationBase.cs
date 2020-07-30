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
        protected readonly ILogger logger;

        protected LightApplicationBase()
        {
            logger = (this as ILoggerProvider).Logger;
            this.Styles.AddRange(new[] {
                new StyleInclude(new Uri("avares://NooneUI/Styles")){
                    Source = new Uri("avares://Avalonia.Themes.Default/DefaultTheme.xaml")
                },
                new StyleInclude(new Uri("avares://NooneUI/Styles")){
                    Source = new Uri("avares://Avalonia.Themes.Default/Accents/BaseDark.xaml")
                },
                new StyleInclude(new Uri("avares://NooneUI/Styles")){
                    Source = new Uri("avares://NooneUI/Framework/LightWindowBaseStyle.xaml")
                }
            });
            this.DataTemplates.Add(new ViewLocator());
        }

        public override void Initialize()
        {
            // register view and view model
            Container container = (this as IContainerProvider).Container;
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic && assembly != typeof(LightApplicationBase).Assembly)
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => typeof(IView).IsAssignableFrom(type) || typeof(IViewModel).IsAssignableFrom(type))
                .ToList();
            Relationship.Current.AddRegistration(types);
            types.ForEach(type => container.Bind(type));
            RegisterServices(container);
        }

        protected virtual void RegisterServices(Container container) { }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                InitializeMainWindow(desktop);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitializeMainWindow(IClassicDesktopStyleApplicationLifetime desktop)
        {
            // var mainEntry = this.GetType().GetCustomAttribute<MainEntryAttribute>();
            // if (mainEntry == null)
            // {
            //     throw new InvalidOperationException("No main window has been registered");
            // }
            //var container = (this as IContainerProvider).Container;
            desktop.MainWindow = LookupMainEntry();

        }

        protected abstract Window LookupMainEntry();
    }

    public abstract class LightApplicationBase<TView> : LightApplicationBase
        where TView : Window
    {
        protected override Window LookupMainEntry()
        {
            TView view = (this as IContainerProvider).Container.Get<TView>();
            if (view == null)
            {
                throw new InvalidOperationException("MainWindow must implement IView");
            }
            return view;
        }
    }
}