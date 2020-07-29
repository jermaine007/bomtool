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
    }
}