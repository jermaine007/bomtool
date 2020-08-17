using Avalonia.Markup.Xaml;
using Noone.UI.Controls;
using Noone.UI.Core;

namespace BomTool.NetCore.Views
{
    public class AboutWindow : LightWindowBase
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
