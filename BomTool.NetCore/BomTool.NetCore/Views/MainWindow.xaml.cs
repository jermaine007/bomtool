using Avalonia;
using Avalonia.Controls;
using BomTool.NetCore.Framework;
using Avalonia.Markup.Xaml;
using System;

namespace BomTool.NetCore.Views
{
    public class MainWindow : LightWindowBase
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}