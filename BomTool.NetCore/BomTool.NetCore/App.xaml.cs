using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BomTool.NetCore.Framework;
using BomTool.NetCore.ViewModels;
using BomTool.NetCore.Views;

namespace BomTool.NetCore
{
    [MainEntry(typeof(MainWindow), typeof(MainWindowViewModel))]
    public class App : LightApplicationBase
    {
        public override void Initialize()
        {
            base.Initialize();
            AvaloniaXamlLoader.Load(this);
        }

        // protected override void RegisterServices(Container container)
        // {
        //     container.Bind<MainWindowViewModel>(true);
        // }

        // public override void OnFrameworkInitializationCompleted()
        // {
        //     if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //     {
        //         InitializeMainWindow(desktop);
        //     }

        //     base.OnFrameworkInitializationCompleted();
        // }

        // private void InitializeMainWindow(IClassicDesktopStyleApplicationLifetime desktop)
        // {
        //     var mainWindowAttribute = this.GetType().GetCustomAttribute<MainWindowAttribute>();
        //     if (mainWindowAttribute == null)
        //     {
        //         throw new InvalidOperationException("No main window has been registered");
        //     }
        //     var window = (Window)Activator.CreateInstance(mainWindowAttribute?.Type);
        //     window.DataContext = (this as IContainerProvider).Container.Get<MainWindowViewModel>();
        //     desktop.MainWindow = window;
        // }
    }
}