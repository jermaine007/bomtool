using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NooneUI.Framework;

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