using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Noone.UI.Controls;
using Noone.UI.Core;
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
