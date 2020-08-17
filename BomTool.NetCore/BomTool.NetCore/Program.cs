using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using BomTool.NetCore.Views;
using Noone.UI;
using Noone.UI.Controls;
using Noone.UI.Core;

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
