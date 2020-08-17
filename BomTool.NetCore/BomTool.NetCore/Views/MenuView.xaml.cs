using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Noone.UI.Controls;
using Noone.UI.Core;

namespace BomTool.NetCore.Views
{
    public class MenuView : LightUserControlBase
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
