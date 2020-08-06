using Avalonia.Markup.Xaml;
using NooneUI.Framework;

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
