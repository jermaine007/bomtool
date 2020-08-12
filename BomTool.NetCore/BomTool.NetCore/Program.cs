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
        public static void Main(string[] args)
            => Bootstrapper.Create<LightApplicationBase<MainWindow>>()
                   .Use((IContainer container) => { })
                   .Use(builder => builder
                       .UsePlatformDetect()
                       .LogToDebug()
                       .UseReactiveUI())
                   .Launch(args);
    }
}
