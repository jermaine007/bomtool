using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NooneUI.Framework;
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