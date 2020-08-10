using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using BomTool.NetCore.Views;
using NooneUI;
using NooneUI.Framework;

namespace BomTool.NetCore
{
    class Program
    {
        /*

        First create a App class like
            public class App : LightApplicationBase<MainWindow>
            {
                public App() {
                    AvaloniaXamlLoader.Load(this);
                }
                .
                .
                .
                do other things
            }
         Then Bootstrapper.Create<App>() ...

         Or if you have no special settings for App,
         Just Use Bootstrapper.Create<LightApplicationBase<MainWindow>>() ...

         */
        public static void Main(string[] args)
            => Bootstrapper.Create<LightApplicationBase<MainWindow>>()
                // enable logger
                .Use(() => true)
                // Register some services
                .Use((IContainer container) => { })
                // Configure apps
                .Use(builder => builder
                        .UsePlatformDetect()
                        .LogToDebug()
                        .UseReactiveUI())
                .Launch(args);
    }
}
