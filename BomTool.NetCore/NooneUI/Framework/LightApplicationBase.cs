using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;


namespace NooneUI.Framework
{
    public class LightApplicationBase : Application, IContainerProvider, ILoggerProvider
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

        public override void Initialize() => RegisterServices((this as IContainerProvider).Container);

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
            var mainEntry = this.GetType().GetCustomAttribute<MainEntryAttribute>();
            if (mainEntry == null)
            {
                throw new InvalidOperationException("No main window has been registered");
            }
            var container = (this as IContainerProvider).Container;
            var window = (Window)container.Get(mainEntry.ViewType);
            window.DataContext = container.Get(mainEntry.ViewModelType);
            desktop.MainWindow = window;
        }
    }
}