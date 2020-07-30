using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BomTool.NetCore.ViewModels;
using BomTool.NetCore.Views;
using NooneUI.Framework;

namespace BomTool.NetCore
{
    public class App : LightApplicationBase<MainWindow>
    {
        public override void Initialize()
        {
            base.Initialize();
            AvaloniaXamlLoader.Load(this);
        }
    }
}